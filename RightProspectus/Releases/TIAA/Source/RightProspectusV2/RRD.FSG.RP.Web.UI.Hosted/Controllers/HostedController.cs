// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************


using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.Interfaces.HostedPages;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.Hosted.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.Services;
using System.Xml.Linq;


/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class HostedController.
    /// </summary>
    public class HostedController : BaseController
    {
        /// <summary>
        /// The hosted client page scenarios
        /// </summary>
        private IHostedClientPageScenariosFactory hostedClientPageScenarios;
        /// <summary>
        /// The hosted request material details
        /// </summary>
        private IHostedClientRequestMaterialFactory hostedRequestMaterialDetails;
        /// <summary>
        /// The site activity factory
        /// </summary>
        private IFactory<SiteActivityObjectModel, int> siteActivityFactory;
        /// <summary>
        /// The client cache factory
        /// </summary>
        private IFactoryCache<ClientFactory, ClientObjectModel, int> clientCacheFactory;
        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;


        /// <summary>
        /// Initializes a new instance of the <see cref="HostedController" /> class.
        /// </summary>
        public HostedController()
        {
            this.hostedClientPageScenarios = RPV2Resolver.Resolve<IHostedClientPageScenariosFactory>();
            this.hostedRequestMaterialDetails = RPV2Resolver.Resolve<IHostedClientRequestMaterialFactory>();

            siteActivityFactory = RPV2Resolver.Resolve<IFactory<SiteActivityObjectModel, int>>("SiteActivity");

            clientCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientFactory, ClientObjectModel, int>>("Client");
            clientCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;

            siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
            siteCacheFactory.Mode = FactoryCacheMode.All;
        }

        /// <summary>
        /// Welcomes this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Welcome()
        {
            return "Welcome";
        }

        /// <summary>
        /// Indexes the specified customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="site">The site.</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <returns>ActionResult.</returns>
        [Route("{customer}")]
        public ActionResult Index(string customer, string site, bool? isProofing = false)
        {
            if (!ValidateURL(customer, site))
            {
                return RedirectToAction("Index", "Error", new { ErrorCode = "600" });
            }

            base.SetContext(customer);
            string pageName = string.Empty;

            try
            {
                pageName = hostedClientPageScenarios.GetCurrentPageName(customer, site);
            }
            catch { }
            //Stored Procedure to get default page id and site id for the customer..
            switch (pageName)
            {
                case "TAL": return TaxonomyAssociationLinks(customer, site, isProofing);

                case "TAD": return TaxonomyAssociationDocuments(customer, site, isProofing);

                case "TAHD": return TaxonomyAssociationHierarchyDocuments(customer, site, null, false, isProofing);

                case "TAGD": return TaxonomyAssociationGroupDocuments(customer, site, isProofing);

                default: return RedirectToAction("Index", "Error", new { ErrorCode = "404" });

            }
        }

        /// <summary>
        /// Taxonomies the association links.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="site">The site.</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <returns>ActionResult.</returns>
        [Route("{customer}/TAL/")]
        public ActionResult TaxonomyAssociationLinks(string customer, string site, bool? isProofing = false)
        {
            if (!ValidateURL(customer, site))
            {
                return RedirectToAction("Index", "Error", new { ErrorCode = "600" });
            }
            base.SetContext(customer);

            TaxonomyAssociationLinkViewModel viewModel = new TaxonomyAssociationLinkViewModel();
            viewModel.ClientName = customer;
            viewModel.SiteName = site;
            viewModel.IsProofing = isProofing.Value ? 1 : 0;
            viewModel.BaseURL = GetBaseUrl();
            viewModel.PageId = hostedClientPageScenarios.GetPageIDForPageName("TAL");
            viewModel.PageCSSResourceKey = "TAL_CSSFile";

            bool getProductionVersion = !isProofing.Value;
            List<HostedSiteText> cachedSiteText = hostedClientPageScenarios.GetSiteTextFromCache(customer, site).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            if (cachedSiteText.Exists(p => p.ResourceKey == "LogoText"))
            {
                viewModel.LogoText = cachedSiteText.Find(p => p.ResourceKey == "LogoText").Text;
            }
            List<HostedPageText> cachedPageText = hostedClientPageScenarios.GetPageTextFromCache(customer, site, viewModel.PageId).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            if (cachedPageText.Exists(p => p.ResourceKey == "TAL_ProductHeaderText"))
            {
                viewModel.ProductHeaderText = cachedPageText.Find(p => p.ResourceKey == "TAL_ProductHeaderText").Text;
            }

            viewModel.TaxonomyAssociationLinkModelData = hostedClientPageScenarios.GetTaxonomyAssociationLinks(customer, site);
            if (viewModel.TaxonomyAssociationLinkModelData == null)
            {
                if (cachedSiteText.Exists(p => p.ResourceKey == "TaxonomyNotAvailableText"))
                {
                    viewModel.TaxonomyNotAvailableText = cachedSiteText.Find(p => p.ResourceKey == "TaxonomyNotAvailableText").Text;
                }

                return View("TaxonomyDataNotAvailable", viewModel);
            }
            string returnViewName = "";

            switch (hostedClientPageScenarios.GetTemplateName(customer, site))
            {
                case "Default":
                    returnViewName = "TaxonomyAssociationLinksT1";
                    break;
            }
            return View(returnViewName, viewModel);
        }

        /// <summary>
        /// Taxonomies the association hierarchy documents.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="site">The site.</param>
        /// <param name="externalID">The external identifier.</param>
        /// <param name="isInternalTAID">if set to <c>true</c> [is internal ta id].</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <returns>ActionResult.</returns>
        [Route("{customer}/TAHD/{externalID}")]
        public ActionResult TaxonomyAssociationHierarchyDocuments(string customer, string site, string externalID, bool? isInternalTAID = false, bool? isProofing = false)
        {
            if (!ValidateURL(customer, site))
            {
                return RedirectToAction("Index", "Error", new { ErrorCode = "600" });
            }
            base.SetContext(customer);

            TaxonomyAssociationHierarchyViewModel viewModel = new TaxonomyAssociationHierarchyViewModel();
            viewModel.ClientName = customer;
            viewModel.SiteName = site;
            viewModel.IsProofing = isProofing.Value ? 1 : 0;
            viewModel.BaseURL = GetBaseUrl();
            viewModel.PageId = hostedClientPageScenarios.GetPageIDForPageName("TAHD");
            viewModel.PageCSSResourceKey = "TAHD_CSSFile";

            bool getProductionVersion = !isProofing.Value;

            List<HostedSiteText> cachedSiteText = hostedClientPageScenarios.GetSiteTextFromCache(customer, site).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            if (cachedSiteText.Exists(p => p.ResourceKey == "LogoText"))
            {
                viewModel.LogoText = cachedSiteText.Find(p => p.ResourceKey == "LogoText").Text;
            }
            List<HostedPageText> cachedPageText = hostedClientPageScenarios.GetPageTextFromCache(customer, site, viewModel.PageId).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            if (cachedPageText.Exists(p => p.ResourceKey == "TAHD_ProductHeaderText"))
            {
                viewModel.ProductHeaderText = cachedPageText.Find(p => p.ResourceKey == "TAHD_ProductHeaderText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAHD_ProductGridProductNameColumnText"))
            {
                viewModel.ProductGridProductNameColumnText = cachedPageText.Find(p => p.ResourceKey == "TAHD_ProductGridProductNameColumnText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAHD_UnderlyingFundHeaderText"))
            {
                viewModel.UnderlayingFundHeaderText = cachedPageText.Find(p => p.ResourceKey == "TAHD_UnderlyingFundHeaderText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAHD_UnderlyingFundGridFundNameColumnText"))
            {
                viewModel.UnderlayingFundGridFundNameColumnText = cachedPageText.Find(p => p.ResourceKey == "TAHD_UnderlyingFundGridFundNameColumnText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAHD_DocumentNotAvailableText"))
            {
                viewModel.DocumentNotAvailableText = cachedPageText.Find(p => p.ResourceKey == "TAHD_DocumentNotAvailableText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAHD_ProductDocumentNotAvailableText"))
            {
                viewModel.ProductDocumentNotAvailableText = cachedPageText.Find(p => p.ResourceKey == "TAHD_ProductDocumentNotAvailableText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAHD_FootnotesHeaderText"))
            {
                viewModel.FootnotesHeaderText = cachedPageText.Find(p => p.ResourceKey == "TAHD_FootnotesHeaderText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAHD_Glossary"))
            {
                viewModel.Glossary = cachedPageText.Find(p => p.ResourceKey == "TAHD_Glossary").Text;
            }

            int xbrlFeatureMode = hostedClientPageScenarios.GetSiteFeatureModeFromCache(customer, site, "XBRL");
            List<FeatureEnums.XBRL> featureModes = FeatureHelper.GetFeatureModesXBRL(xbrlFeatureMode);

            if (featureModes.Contains(FeatureEnums.XBRL.Enabled) && featureModes.Contains(FeatureEnums.XBRL.ShowXBRLInLandingPage))
            {
                viewModel.ShowXBRLInLandingPage = true;
                if (featureModes.Contains(FeatureEnums.XBRL.ShowXBRLInNewTab))
                {
                    viewModel.DisplayXBRLInNewTAB = true;
                }
            }

            TaxonomyAssociationHierarchyModel objTaxonomyAssociationHierarchyModel = null;
            if (isInternalTAID.Value)
            {
                objTaxonomyAssociationHierarchyModel = hostedClientPageScenarios.GetTaxonomyAssociationHierarchyDocuments(customer, site, null, int.Parse(externalID));
            }
            else
            {
                objTaxonomyAssociationHierarchyModel = hostedClientPageScenarios.GetTaxonomyAssociationHierarchyDocuments(customer, site, externalID, null);
            }
            viewModel.TaxonomyAssociationHierarchyModelData = objTaxonomyAssociationHierarchyModel;

            if (viewModel.TaxonomyAssociationHierarchyModelData == null)
            {
                if (cachedSiteText.Exists(p => p.ResourceKey == "TaxonomyNotAvailableText"))
                {
                    viewModel.TaxonomyNotAvailableText = cachedSiteText.Find(p => p.ResourceKey == "TaxonomyNotAvailableText").Text;
                }

                return View("TaxonomyDataNotAvailable", viewModel);
            }

            string returnViewName = "";

            switch (hostedClientPageScenarios.GetTemplateName(customer, site))
            {
                case "Default":
                    returnViewName = "TaxonomyAssociationHieararchyDocumentsT1";
                    break;
            }
            return View(returnViewName, viewModel);
        }

        /// <summary>
        /// Taxonomies the specific document frame.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="site">The site.</param>
        /// <param name="externalID1">The external i d1.</param>
        /// <param name="externalID2">The external i d2.</param>
        /// <param name="isInternalTAID">if set to <c>true</c> [is internal ta id].</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <returns>ActionResult.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), Route("{customer}/TADF/{externalID1}/{externalID2}")]
        public ActionResult TaxonomySpecificDocumentFrame(string customer, string site, string externalID1, string externalID2, bool? isInternalTAID = false, bool? isProofing = false, bool? spv = false)
        {
            if (!ValidateURL(customer, site))
            {
                return RedirectToAction("Index", "Error", new { ErrorCode = "600" });
            }
            base.SetContext(customer);

            bool getProductionVersion = !isProofing.Value;

            TaxonomySpecificDocumentFrameViewModel viewModel = new TaxonomySpecificDocumentFrameViewModel
            {
                ClientName = customer,
                SiteName = site,
                IsProofing = isProofing.Value ? 1 : 0,
                IsInternalTAID = isInternalTAID.Value ? 1 : 0,
                ExternalID1 = externalID1,
                ExternalID2 = externalID2,
                BaseURL = GetBaseUrl(),
                RequestBatchId = Guid.NewGuid(),
                PageId = hostedClientPageScenarios.GetPageIDForPageName("TADF"),
                PageCSSResourceKey = "TADF_CSSFile"
            };

            //Get Site Text
            List<HostedSiteText> cachedSiteText = hostedClientPageScenarios.GetSiteTextFromCache(customer, site).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            TextHelper.GetSiteText(viewModel, cachedSiteText);
            //Get Page Text 
            List<HostedPageText> cachedPageText = hostedClientPageScenarios.GetPageTextFromCache(customer, site, viewModel.PageId).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            TextHelper.GetPageText(viewModel, cachedPageText, "TADF");

            int browserAlertFeatureMode = hostedClientPageScenarios.GetSiteFeatureModeFromCache(customer, site, "BrowserAlert");
            List<FeatureEnums.BrowserAlert> featureModesBrowserAlert = FeatureHelper.GetFeatureModesBrowserAlert(browserAlertFeatureMode);
            if (featureModesBrowserAlert.Contains(FeatureEnums.BrowserAlert.Enabled))
            {
                BrowserVersionObjectModel browserVersionObjectModel = hostedClientPageScenarios.GetBrowserVersion(customer, site, Request.Browser.Browser);
                if (browserVersionObjectModel != null)
                {
                    viewModel.DisplayBrowserAlert = true;
                    viewModel.BrowserDetails = FeatureHelper.ValidateLatestBrowser(browserVersionObjectModel, Request.Browser);
                }
            }

            if (isInternalTAID.Value)
            {
                viewModel.TaxonomyAssociationData = hostedClientPageScenarios.GetTaxonomySpecificDocuments(customer, site, null, int.Parse(externalID1));
            }
            else
            {
                viewModel.TaxonomyAssociationData = hostedClientPageScenarios.GetTaxonomySpecificDocuments(customer, site, externalID1, null);
            }
            //Document Not available scenario
            bool checkIfDocumentTypeExist = true;
            if (viewModel.TaxonomyAssociationData != null)
            {
                if (isInternalTAID.Value)
                {
                    checkIfDocumentTypeExist = viewModel.TaxonomyAssociationData.DocumentTypes.
                                                                                  Exists(d => d.DocumentTypeId == int.Parse(externalID2));
                }
                else
                {
                    checkIfDocumentTypeExist = viewModel.TaxonomyAssociationData.DocumentTypes.
                                                              Exists(d => d.DocumentTypeExternalID.Contains(externalID2, StringComparer.OrdinalIgnoreCase));
                }
            }
            else
            {
                BadRequestType badRequestIssue = BadRequestType.InvalidDocTypeExternalID;
                string badRequestParameterName = "DocumentType";
                string badRequestParameterValue = externalID2;
                if (viewModel.TaxonomyAssociationData == null)
                {
                    badRequestIssue = BadRequestType.InvalidTaxonomyLevelExternalID;
                    badRequestParameterName = "TaxonomyExternalId";
                    badRequestParameterValue = externalID1;
                }

                
                TrackSiteActivity(new SiteActivityViewModel
                {
                    Customer = customer,
                    Site = site,
                    ExternalID1 = externalID1,
                    ExternalID2 = externalID2,
                    IsInternalTAID = isInternalTAID.Value,
                    RequestBatchId = viewModel.RequestBatchId,
                    InitDoc = true,
                    BadRequestIssue = (int)badRequestIssue,
                    BadRequestParameterName = badRequestParameterName,
                    BadRequestParameterValue = badRequestParameterValue
                });
                viewModel.LogoText = viewModel.TADFLogoText;
                return View("DocumentNotAvailable", viewModel);
            }

            viewModel.FundName = viewModel.TaxonomyAssociationData.TaxonomyName;

            bool showXBRLInTabbedView = false;
            int xbrlFeatureMode = hostedClientPageScenarios.GetSiteFeatureModeFromCache(customer, site, "XBRL");
            List<FeatureEnums.XBRL> featureModes = FeatureHelper.GetFeatureModesXBRL(xbrlFeatureMode);

            showXBRLInTabbedView = featureModes.Contains(FeatureEnums.XBRL.Enabled) && featureModes.Contains(FeatureEnums.XBRL.ShowXBRLInTabbedView);

            int requestMaterialFeatureMode = hostedClientPageScenarios.GetPageFeatureModeFromCache(customer, site, viewModel.PageId, "RequestMaterial");
            List<FeatureEnums.RequestMaterial> featureModesReaquestMaterial = FeatureHelper.GetFeatureModesRequestMaterial(requestMaterialFeatureMode);
            if (featureModesReaquestMaterial.Contains(FeatureEnums.RequestMaterial.Enabled))
            {
                viewModel.DisplayRequestMaterial = true;
            }

            int singlePdfViewFeatureMode = hostedClientPageScenarios.GetPageFeatureModeFromCache(customer, site, viewModel.PageId, "SinglePdfView");
            List<FeatureEnums.SinglePdfView> featureModesSinglePdfView = FeatureHelper.GetFeatureModesSinglePdfView(singlePdfViewFeatureMode);

            if (spv.Value && featureModesSinglePdfView.Contains(FeatureEnums.SinglePdfView.Enabled))
            {
                viewModel.IsSinglePdfView = true;
                if (featureModesSinglePdfView.Contains(FeatureEnums.SinglePdfView.ShowClientLogoFrame))
                {
                    viewModel.SinglePdfViewShowClientFrame = true;
                }
            }

            int sarValidationFeatureMode = hostedClientPageScenarios.GetSiteFeatureModeFromCache(customer, site, "SARValidation");
            List<FeatureEnums.SARValidation> featureModesSARValidation = FeatureHelper.GetFeatureModesSARValidation(sarValidationFeatureMode);
            if (featureModesSARValidation.Contains(FeatureEnums.SARValidation.Enabled))
            {
                viewModel.IsSARValidationEnabled = true;
            }

            Tuple<string, string> cachedSiteNavigation = hostedClientPageScenarios.GetPageNavigationFromCache(customer, site, "TADF_DocumentTypeMenu", isProofing.Value, viewModel.PageId);
            viewModel.PageNavigationXML = cachedSiteNavigation.Item1;
            viewModel.PageNavigationXSLT = cachedSiteNavigation.Item2;
            XDocument xDocumentNavigationXMLFinal = PageNavigationHelper.GetFinalNavigationXML(viewModel.PageNavigationXML, customer, site, externalID1, isInternalTAID, viewModel.TaxonomyAssociationData, showXBRLInTabbedView, viewModel.IsSARValidationEnabled);
            viewModel.PageNavigationXML = xDocumentNavigationXMLFinal.ToString();


            SiteActivityViewModel siteActivityViewModel = new SiteActivityViewModel
            {
                Customer = customer,
                Site = site,
                ExternalID1 = externalID1,
                ExternalID2 = externalID2,
                IsInternalTAID = isInternalTAID.Value,
                RequestBatchId = viewModel.RequestBatchId,
                InitDoc = true
            };

            if (checkIfDocumentTypeExist)
            {
                if (isInternalTAID.Value)
                {
                    viewModel.PageLoadPDFURL = viewModel.TaxonomyAssociationData.DocumentTypes.Find(p => p.DocumentTypeId == int.Parse(externalID2)).ContentURI;
                    viewModel.PageLoadMenuID = "MenuTdID" + viewModel.TaxonomyAssociationData.DocumentTypes.Find(p => p.DocumentTypeId == int.Parse(externalID2)).VerticalMarketID;
                }
                else
                {
                    viewModel.PageLoadPDFURL = viewModel.TaxonomyAssociationData.DocumentTypes.Find(p => p.DocumentTypeExternalID.Contains(externalID2, StringComparer.OrdinalIgnoreCase)).ContentURI;
                    viewModel.PageLoadMenuID = "MenuTdID" + viewModel.TaxonomyAssociationData.DocumentTypes.Find(p => p.DocumentTypeExternalID.Contains(externalID2, StringComparer.OrdinalIgnoreCase)).VerticalMarketID;
                }
            }
            else
            {
                

                if (cachedPageText.Exists(p => p.ResourceKey == "TADF_DocumentNotAvailableText"))
                {
                    viewModel.DocumentNotAvailableText = cachedPageText.Find(p => p.ResourceKey == "TADF_DocumentNotAvailableText").Text;
                }

                siteActivityViewModel.BadRequestIssue = (int)BadRequestType.InvalidDocTypeExternalID;
                siteActivityViewModel.BadRequestParameterName = "DocumentType";
                siteActivityViewModel.BadRequestParameterValue = externalID2;
            }

            string returnViewName = "";
            switch (hostedClientPageScenarios.GetTemplateName(customer, site))
            {
                case "Default":
                    returnViewName = "TaxonomySpecificDocumentFrameT1";
                    break;
            }

            TrackSiteActivity(siteActivityViewModel);

            return View(returnViewName, viewModel);
        }





        /// <summary>
        /// TaxonomyAssociation ClientDocument Frame.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="site">The site.</param>
        /// <param name="externalID1">The external id1.</param>    
        /// <param name="externalID2">The external id2.</param>    
        /// <param name="isITAID">if set to <c>true</c> [is internal ta id].</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <param name="SPV">if set to <c>true</c> [Single PDF View].</param>
        /// <returns>ActionResult.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), Route("{customer}/TACDF/{externalID1}/{externalID2}")]
        public ActionResult TaxonomyAssociationClientDocumentFrame(string customer, string site, string externalID1, string externalID2, bool? isITAID = false, bool? isProofing = false, bool SPV = false)
        {
            if (!ValidateURL(customer, site))
            {
                return RedirectToAction("Index", "Error", new { ErrorCode = "600" });
            }

            switch (externalID2)
            {
                case "DMMD":
                case "NCR":
                    if (!SPV)
                    {
                        return RedirectToAction("Index", "Error", new { ErrorCode = "600" });
                    }
                    break;
            }
            base.SetContext(customer);

            bool getProductionVersion = !isProofing.Value;

            TaxonomyAssociationClientDocumentViewModel viewModel = new TaxonomyAssociationClientDocumentViewModel
            {
                ClientName = customer,
                SiteName = site,
                IsProofing = isProofing.Value ? 1 : 0,
                IsInternalTAID = isITAID.Value ? 1 : 0,
                ExternalID1 = externalID1,
                ExternalID2 = externalID2,
                BaseURL = GetBaseUrl(),
                RequestBatchId = Guid.NewGuid(),
                PageId = hostedClientPageScenarios.GetPageIDForPageName("TACDF"),
                PageCSSResourceKey = "TACDF_CSSFile",
                IsSinglePdfView = SPV
            };

            if (isITAID.Value)
            {
                viewModel.TaxonomyAssociationData = hostedClientPageScenarios.GetTaxonomyAssociationClientDocuments(customer, null, externalID2, int.Parse(externalID1));
            }
            else
            {
                viewModel.TaxonomyAssociationData = hostedClientPageScenarios.GetTaxonomyAssociationClientDocuments(customer, externalID1, externalID2, null);
            }

            List<HostedSiteText> cachedSiteText = hostedClientPageScenarios.GetSiteTextFromCache(customer, site).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            if (cachedSiteText.Exists(p => p.ResourceKey == "LogoText"))
            {
                viewModel.LogoText = cachedSiteText.Find(p => p.ResourceKey == "LogoText").Text;
            }

            if (viewModel.TaxonomyAssociationData == null)
            {
                BadRequestType badRequestIssue = BadRequestType.InvalidTaxonomyLevelExternalID;
                string badRequestParameterName = "TaxonomyExternalId";
                string badRequestParameterValue = externalID1;

                if (cachedSiteText.Exists(p => p.ResourceKey == "DocumentNotAvailableText"))
                {
                    viewModel.DocumentNotAvailableText = cachedSiteText.Find(p => p.ResourceKey == "DocumentNotAvailableText").Text;
                }
                TrackSiteActivity(new SiteActivityViewModel
                {
                    Customer = customer,
                    Site = site,
                    ExternalID1 = externalID1,
                    IsInternalTAID = isITAID.Value,
                    RequestBatchId = viewModel.RequestBatchId,
                    InitDoc = true,
                    BadRequestIssue = (int)badRequestIssue,
                    BadRequestParameterName = badRequestParameterName,
                    BadRequestParameterValue = badRequestParameterValue
                });
                return View("DocumentNotAvailable", viewModel);
            }

            List<HostedPageText> cachedPageText = hostedClientPageScenarios.GetPageTextFromCache(customer, site, viewModel.PageId).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            if (cachedPageText.Exists(p => p.ResourceKey == "TACDF_FormNMFPText"))
            {
                viewModel.FormNMFPText = cachedPageText.Find(p => p.ResourceKey == "TACDF_FormNMFPText").Text.
                    Replace("Form_NMFP_Link_URL", ConfigurationManager.AppSettings["FormN-MFP_SECUrl"].Replace("CIKVALUE", viewModel.TaxonomyAssociationData.SeriesID));
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TACDF_DMMDLinkText"))
            {
                viewModel.DMMDLinkText = cachedPageText.Find(p => p.ResourceKey == "TACDF_DMMDLinkText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TACDF_NCRLinkText"))
            {
                viewModel.NCRLinkText = cachedPageText.Find(p => p.ResourceKey == "TACDF_NCRLinkText").Text;
            }

            viewModel.TaxonomyAssociationData.ClientDocuments.ForEach(p =>
            {
                p.ContentUri = GetBaseUrl() + "ClientDocument/" + p.ClientDocumentId + "?client=" + customer;
            });

            viewModel.FundName = viewModel.TaxonomyAssociationData.TaxonomyName;

            //TODO : it will be enabled/disabled using navigation XML.
            //int formN_MFPFeatureMode = hostedClientPageScenarios.GetPageFeatureModeFromCache(customer, site, viewModel.PageId, "FormN-MFP");
            //List<FeatureEnums.FormN_MFP> featureModesFormN_MFP = FeatureHelper.GetFeatureModesFormN_MFP(formN_MFPFeatureMode);
            //if (featureModesFormN_MFP.Contains(FeatureEnums.FormN_MFP.Enabled))
            //{
            //    viewModel.DisplayFormN_MFP = true;
            //}

            int dailyMoneyMarketDisclosureFeatureMode = hostedClientPageScenarios.GetPageFeatureModeFromCache(customer, site, viewModel.PageId, "DailyMoneyMarketDisclosure");
            List<FeatureEnums.DailyMoneyMarketDisclosure> featureModesDailyMoneyMarketDisclosure = FeatureHelper.GetFeatureModesDailyMoneyMarketDisclosure(dailyMoneyMarketDisclosureFeatureMode);
            if (featureModesDailyMoneyMarketDisclosure.Contains(FeatureEnums.DailyMoneyMarketDisclosure.Enabled))
            {
                viewModel.DisplayDailyMoneyMarketDisclosure = true;
            }

            int NCRFeatureMode = hostedClientPageScenarios.GetPageFeatureModeFromCache(customer, site, viewModel.PageId, "N-CR");
            List<FeatureEnums.NCR> featureModesNCR = FeatureHelper.GetFeatureModesNCR(NCRFeatureMode);
            if (featureModesNCR.Contains(FeatureEnums.NCR.Enabled))
            {
                viewModel.DisplayNCR = true;
            }

            Tuple<string, string> cachedSiteNavigation = hostedClientPageScenarios.GetPageNavigationFromCache(customer, site, "TACDF_ClientDocumentTypeMenu", isProofing.Value, viewModel.PageId);
            viewModel.PageNavigationXML = cachedSiteNavigation.Item1;
            viewModel.PageNavigationXSLT = cachedSiteNavigation.Item2;
            XDocument xDocumentNavigationXMLFinal = PageNavigationHelper.GetFinalNavigationXML(viewModel.PageNavigationXML, customer, site, externalID1, isITAID, viewModel.TaxonomyAssociationData, false, false);
            viewModel.PageNavigationXML = xDocumentNavigationXMLFinal.ToString();


            SiteActivityViewModel siteActivityViewModel = new SiteActivityViewModel
            {
                Customer = customer,
                Site = site,
                ExternalID1 = externalID1,
                ClientDocumentId = viewModel.TaxonomyAssociationData.ClientDocuments.FirstOrDefault().ClientDocumentId,
                IsInternalTAID = isITAID.Value,
                RequestBatchId = viewModel.RequestBatchId,
                InitDoc = true
            };


            viewModel.PageLoadPDFURL = GetBaseUrl() + "ClientDocument/" + viewModel.TaxonomyAssociationData.ClientDocuments.FirstOrDefault().ClientDocumentId + "?client=" + customer;
            viewModel.PageLoadMenuID = "MenuTdID" + viewModel.TaxonomyAssociationData.ClientDocuments.FirstOrDefault().ClientDocumentId;


            string returnViewName = "";
            switch (hostedClientPageScenarios.GetTemplateName(customer, site))
            {
                case "Default":
                    returnViewName = "TaxonomyAssociationClientDocumentFrameT1";
                    break;
            }

            TrackSiteActivity(siteActivityViewModel);

            return View(returnViewName, viewModel);
        }

        /// <summary>
        /// Taxonomies the association documents.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="site">The site.</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <returns>ActionResult.</returns>
        [Route("{customer}/TAD")]
        public ActionResult TaxonomyAssociationDocuments(string customer, string site, bool? isProofing = false)
        {
            if (!ValidateURL(customer, site))
            {
                return RedirectToAction("Index", "Error", new { ErrorCode = "600" });
            }
            base.SetContext(customer);

            TaxonomyAssociationDocumentsViewModel viewModel = new TaxonomyAssociationDocumentsViewModel
            {
                ClientName = customer,
                SiteName = site,
                IsProofing = isProofing.Value ? 1 : 0,
                BaseURL = GetBaseUrl(),
                PageId = hostedClientPageScenarios.GetPageIDForPageName("TAD"),
                PageCSSResourceKey = "TAD_CSSFile"
            };

            bool getProductionVersion = !isProofing.Value;

            List<HostedSiteText> cachedSiteText = hostedClientPageScenarios.GetSiteTextFromCache(customer, site).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            if (cachedSiteText.Exists(p => p.ResourceKey == "LogoText"))
            {
                viewModel.LogoText = cachedSiteText.Find(p => p.ResourceKey == "LogoText").Text;
            }

            List<HostedPageText> cachedPageText = hostedClientPageScenarios.GetPageTextFromCache(customer, site, viewModel.PageId).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);

            if (cachedPageText.Exists(p => p.ResourceKey == "TAD_UnderlyingFundDocumentsHelpText"))
            {
                viewModel.UnderlyingFundDocumentsHelpText = cachedPageText.Find(p => p.ResourceKey == "TAD_UnderlyingFundDocumentsHelpText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAD_UnderlayingFundGridFundNameColumnText"))
            {
                viewModel.UnderlayingFundGridFundNameColumnText = cachedPageText.Find(p => p.ResourceKey == "TAD_UnderlayingFundGridFundNameColumnText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAD_NAText"))
            {
                viewModel.UnderlayingFundGridNAText = cachedPageText.Find(p => p.ResourceKey == "TAD_NAText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAD_FootnotesHeaderText"))
            {
                viewModel.FootnotesHeaderText = cachedPageText.Find(p => p.ResourceKey == "TAD_FootnotesHeaderText").Text;
            }

            int xbrlFeatureMode = hostedClientPageScenarios.GetSiteFeatureModeFromCache(customer, site, "XBRL");
            List<FeatureEnums.XBRL> featureModes = FeatureHelper.GetFeatureModesXBRL(xbrlFeatureMode);

            if (featureModes.Contains(FeatureEnums.XBRL.Enabled) && featureModes.Contains(FeatureEnums.XBRL.ShowXBRLInLandingPage))
            {
                viewModel.ShowXBRLInLandingPage = true;
                if (featureModes.Contains(FeatureEnums.XBRL.ShowXBRLInNewTab))
                {
                    viewModel.DisplayXBRLInNewTAB = true;
                }
            }


            Tuple<string, string> cachedSiteNavigation = hostedClientPageScenarios.GetPageNavigationFromCache(customer, site, "TAD_HeaderMenu", isProofing.Value, viewModel.PageId);
            viewModel.PageNavigationXML = cachedSiteNavigation.Item1;
            viewModel.PageNavigationXSLT = cachedSiteNavigation.Item2;
            XDocument xDocumentNavigationXMLFinal = PageNavigationHelper.GetFinalNavigationXML(viewModel.PageNavigationXML);
            viewModel.PageNavigationXML = xDocumentNavigationXMLFinal.ToString();

            viewModel.TaxonomyAssociationDocumentsModel = hostedClientPageScenarios.GetTaxonomyAssociationDocuments(customer, site);
            if (viewModel.TaxonomyAssociationDocumentsModel == null)
            {
                if (cachedSiteText.Exists(p => p.ResourceKey == "TaxonomyNotAvailableText"))
                {
                    viewModel.TaxonomyNotAvailableText = cachedSiteText.Find(p => p.ResourceKey == "TaxonomyNotAvailableText").Text;
                }

                return View("TaxonomyDataNotAvailable", viewModel);
            }

            string returnViewName = "";

            switch (hostedClientPageScenarios.GetTemplateName(customer, site))
            {
                case "Default":
                    returnViewName = "TaxonomyAssociationDocumentsT1";
                    break;
            }
            return View(returnViewName, viewModel);
        }

        /// <summary>
        /// XBRLs the specified customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="site">The site.</param>
        /// <param name="externalID">The external identifier.</param>
        /// <param name="isInternalTAID">if set to <c>true</c> [is internal ta id].</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <param name="isPID">if set to <c>true</c> [is pid].</param>
        /// <param name="requestBatchId">The request batch identifier.</param>
        /// <returns>ActionResult.</returns>
        [Route("{customer}/XBRL/{externalID}")]
        public ActionResult XBRL(string customer, string site, string externalID, bool? isInternalTAID = false, bool? isProofing = false, bool isPID = false, Guid? requestBatchId = null)
        {
            if (!ValidateURL(customer, site))
            {
                return RedirectToAction("Index", "Error", new { ErrorCode = "600" });
            }
            base.SetContext(customer);

            bool getProductionVersion = !isProofing.Value;

            XBRLViewModel viewModel = new XBRLViewModel();
            viewModel.ClientName = customer;
            viewModel.SiteName = site;
            viewModel.IsProofing = isProofing.Value ? 1 : 0;
            viewModel.BaseURL = GetBaseUrl();
            viewModel.ExternalID = externalID;
            viewModel.IsInternalTAID = isInternalTAID.Value;
            viewModel.RequestBatchId = requestBatchId.HasValue ? requestBatchId.Value : Guid.NewGuid();
            viewModel.PageId = hostedClientPageScenarios.GetPageIDForPageName("XBRL");
            viewModel.PageCSSResourceKey = "XBRL_CSSFile";
            List<HostedSiteText> cachedSiteText = hostedClientPageScenarios.GetSiteTextFromCache(customer, site).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            if (cachedSiteText.Exists(p => p.ResourceKey == "LogoText"))
            {
                viewModel.LogoText = cachedSiteText.Find(p => p.ResourceKey == "LogoText").Text;
            }

            int xbrlFeatureMode = hostedClientPageScenarios.GetSiteFeatureModeFromCache(customer, site, "XBRL");
            List<FeatureEnums.XBRL> featureModes = FeatureHelper.GetFeatureModesXBRL(xbrlFeatureMode);

            List<HostedPageText> cachedPageText = hostedClientPageScenarios.GetPageTextFromCache(customer, site, viewModel.PageId).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);

            if (cachedPageText.Exists(p => p.ResourceKey == "XBRL_DateFormat"))
            {
                viewModel.XBRLDateFormat = cachedPageText.Find(p => p.ResourceKey == "XBRL_DateFormat").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "XBRL_DatedText"))
            {
                viewModel.XBRLViewerDatedText = cachedPageText.Find(p => p.ResourceKey == "XBRL_DatedText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "XBRL_FiledText"))
            {
                viewModel.XBRLViewerFiledText = cachedPageText.Find(p => p.ResourceKey == "XBRL_FiledText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "XBRL_ViewerHeaderText"))
            {
                viewModel.XBRLViewerHeaderText = cachedPageText.Find(p => p.ResourceKey == "XBRL_ViewerHeaderText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "XBRL_ViewerNotReadyMessage"))
            {
                viewModel.XBRLViewerNotReadyMessage = cachedPageText.Find(p => p.ResourceKey == "XBRL_ViewerNotReadyMessage").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "XBRL_ZipFilesHeaderText"))
            {
                viewModel.XBRLZipFilesHeaderText = cachedPageText.Find(p => p.ResourceKey == "XBRL_ZipFilesHeaderText").Text;
            }

            if (isInternalTAID.Value)
            {
                viewModel.XBRLData = hostedClientPageScenarios.GetXBRLByTaxonomyAssociationIDOrExternalID(customer, int.Parse(externalID), null, isPID);
            }
            else
            {
                viewModel.XBRLData = hostedClientPageScenarios.GetXBRLByTaxonomyAssociationIDOrExternalID(customer, null, externalID, isPID);
            }

            //Document Not available scenario
            if (viewModel.XBRLData == null || viewModel.XBRLData.Count == 0)
            {
                if (cachedSiteText.Exists(p => p.ResourceKey == "DocumentNotAvailableText"))
                {
                    viewModel.DocumentNotAvailableText = cachedSiteText.Find(p => p.ResourceKey == "DocumentNotAvailableText").Text;
                }

                TrackSiteActivity(new SiteActivityViewModel
                {
                    Customer = customer,
                    Site = site,
                    ExternalID1 = externalID,
                    IsInternalTAID = isInternalTAID.Value,
                    RequestBatchId = viewModel.RequestBatchId,
                    BadRequestIssue = (int)BadRequestType.InvalidXBRLTaxonomyLevelExternalID,
                    BadRequestParameterName = "TaxonomyExternalId",
                    BadRequestParameterValue = externalID
                });

                return View("DocumentNotAvailable", viewModel);
            }

            viewModel.FundName = viewModel.XBRLData[0].TaxonomyName;


            string returnViewName = "";
            switch (hostedClientPageScenarios.GetTemplateName(customer, site))
            {
                case "Default":
                    if (featureModes.Contains(FeatureEnums.XBRL.ShowXBRLInNewTab))
                    {
                        returnViewName = "XBRLT1";
                    }
                    else if (featureModes.Contains(FeatureEnums.XBRL.ShowXBRLInTabbedView))
                    {
                        returnViewName = "XBRLContentT1";
                    }
                    break;
            }
            return View(returnViewName, viewModel);
        }

        /// <summary>
        /// Tracks the site activity.
        /// </summary>
        /// <param name="siteActivityViewModel">The site activity view model.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [WebMethod]
        public bool TrackSiteActivity(SiteActivityViewModel siteActivityViewModel)
        {
            siteActivityFactory.ClientName = siteActivityViewModel.Customer;

            SiteActivityObjectModel saobject = new SiteActivityObjectModel();

            saobject.SiteName = siteActivityViewModel.Site;
            saobject.InitDoc = siteActivityViewModel.InitDoc;
            saobject.RequestBatchId = siteActivityViewModel.RequestBatchId;

            if (siteActivityViewModel.IsInternalTAID)
            {
                saobject.TaxonomyAssociationId = int.Parse(siteActivityViewModel.ExternalID1);
            }
            else
            {
                saobject.TaxonomyExternalId = siteActivityViewModel.ExternalID1;
            }

            saobject.ClientDocumentId = siteActivityViewModel.ClientDocumentId;

            saobject.Level = 1;

            if (siteActivityViewModel.IsInternalDTID || siteActivityViewModel.IsInternalTAID)
            {
                if (!string.IsNullOrWhiteSpace(siteActivityViewModel.ExternalID2)) // siteActivityViewModel.ExternalID2 will be null when we click on zip file.
                {
                    saobject.DocumentTypeId = int.Parse(siteActivityViewModel.ExternalID2);
                }
            }
            else
            {
                saobject.DocumentTypeExternalID = siteActivityViewModel.ExternalID2;
            }

            saobject.ClientIPAddress = Request.ServerVariables["REMOTE_ADDR"];

            saobject.UserAgentString = Request.ServerVariables["HTTP_USER_AGENT"];

            saobject.HTTPMethod = "GET";

            saobject.RequestUriString = Request.Url.ToString();

            if (!string.IsNullOrWhiteSpace(siteActivityViewModel.XBRLRequestURL))
            {
                saobject.RequestUriString = siteActivityViewModel.XBRLRequestURL;
            }

            saobject.ParsedRequestUriString = Request.Url.ToString();
            try
            {
                saobject.ServerName = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
            }
            catch
            {
                saobject.ServerName = "";
            }

            saobject.ReferrerUriString = string.IsNullOrWhiteSpace(Request.ServerVariables["HTTP_REFERER"]) ? "" : Request.ServerVariables["HTTP_REFERER"];

            saobject.XBRLDocumentName = siteActivityViewModel.XBRLDocumentName;

            saobject.XBRLItemType = siteActivityViewModel.XBRLItemType;

            saobject.BadRequestIssue = siteActivityViewModel.BadRequestIssue;

            saobject.BadRequestParameterName = siteActivityViewModel.BadRequestParameterName;

            saobject.BadRequestParameterValue = siteActivityViewModel.BadRequestParameterValue;

            siteActivityFactory.SaveEntity(saobject);

            return true;
        }

        /// <summary>
        /// ValidateURL.
        /// </summary>
        /// <param name="clientName">clientName.</param>
        /// <param name="siteName">siteName.</param>
        /// <returns>isValidRequest</returns>
        private bool ValidateURL(string clientName, string siteName)
        {
            bool isValidRequest = false;
            string url = Request.Url.ToString().ToLower();
            if (!url.Contains(ConfigurationManager.AppSettings["hostedBaseUrl"].ToString().ToLower()))
            {
                ClientObjectModel clientDetails = clientCacheFactory.GetAllEntities()
                                                    .Where(c => c.ClientDnsList.Where(d => url.Contains(d.Dns.ToLower())).Count() > 0).FirstOrDefault();
                if (clientDetails.ClientName.ToLower() == clientName.ToLower())
                {
                    siteCacheFactory.ClientName = clientName;
                    if (string.IsNullOrWhiteSpace(siteName))
                    {
                        isValidRequest = true;
                    }
                    else
                    {
                        SiteObjectModel siteDetails = siteCacheFactory.GetEntitiesBySearch(
                                                        new SiteSearchDetail
                                                        {
                                                            SiteID = clientDetails.ClientDnsList.Where(d => url.Contains(d.Dns.ToLower())).FirstOrDefault().ClientDnsSiteId
                                                        }).FirstOrDefault();
                        if (siteDetails != null && siteDetails.Name.ToLower() == siteName.ToLower())
                        {
                            isValidRequest = true;
                        }
                    }
                }
            }
            else
            {
                isValidRequest = true;
            }

            return isValidRequest;
        }


        /// <summary>
        /// Requests the material.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="site">The site.</param>
        /// <param name="externalID">The external identifier.</param>
        /// <param name="isInternalTAID">if set to <c>true</c> [is internal ta id].</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <param name="requestBatchId">The request batch identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult RequestMaterial(string customer, string site, string externalID, bool? isInternalTAID = false, bool? isProofing = false, Guid? requestBatchId = null)
        {
            RequestMaterialViewModel viewModel = new RequestMaterialViewModel();
            viewModel.ClientName = customer;
            viewModel.SiteName = site;
            viewModel.RequestBatchId = requestBatchId;

            if (isInternalTAID.Value)
            {
                viewModel.TaxonomyAssociationData = hostedClientPageScenarios.GetTaxonomySpecificDocuments(customer, site, null, int.Parse(externalID));
            }
            else
            {
                viewModel.TaxonomyAssociationData = hostedClientPageScenarios.GetTaxonomySpecificDocuments(customer, site, externalID, null);
            }

            viewModel.TaxonomyAssociationData.DocumentTypes.ForEach(x =>
            {
                x.DocumentTypeLinkText = x.DocumentTypeLinkText.Replace("newline", " ");
            });

            return View("RequestMaterial", viewModel);
        }

        /// <summary>
        /// Adds the email details.
        /// </summary>
        /// <param name="siteEmailData">The site email data.</param>
        [HttpPost]
        public void AddEmailDetails(RequestMaterialViewModel siteEmailData)
        {

            string[] documentTypeIds = siteEmailData.SelectedDocTypes.Split(',');
            RequestMaterialEmailHistory emailDetails = new RequestMaterialEmailHistory();


            Guid uniqueId = new Guid();
            uniqueId = Guid.NewGuid();

            emailDetails.RecipEmail = siteEmailData.Email;

            emailDetails.IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            emailDetails.UserAgent = Request.ServerVariables["HTTP_USER_AGENT"];
            emailDetails.RequestUriString = Request.Url.ToString();
            emailDetails.Referer = string.IsNullOrWhiteSpace(Request.ServerVariables["HTTP_REFERER"]) ? "" : Request.ServerVariables["HTTP_REFERER"];

            emailDetails.UniqueID = uniqueId;
            emailDetails.RequestBatchId = Guid.Parse(siteEmailData.RequestBatchId.ToString());

            TaxonomyAssociationData taxonomyData = new TaxonomyAssociationData();

            taxonomyData.TaxonomyAssociationID = Convert.ToInt32(siteEmailData.TaxanomyAssociationId);
            taxonomyData.DocumentTypes = new List<HostedDocumentType>();
            foreach (var item in documentTypeIds)
            {

                taxonomyData.DocumentTypes.Add(new HostedDocumentType { DocumentTypeId = int.Parse(item.Split(':')[0].Trim()), DocumentTypeLinkText = item.Split(':')[1].Trim() });
            }


            emailDetails.TaxonomyAssociationData = taxonomyData;

            emailDetails.Sent = SendRequestMaterialMail(siteEmailData.ClientName, siteEmailData.SiteName, taxonomyData.DocumentTypes, uniqueId, siteEmailData.Email);
            hostedRequestMaterialDetails.SaveEmailDetails(siteEmailData.ClientName, siteEmailData.SiteName, emailDetails);

        }
        /// <summary>
        /// Saves the print details.
        /// </summary>
        /// <param name="sitePrintData">The site print data.</param>
        [HttpPost]
        public void SavePrintDetails(RequestMaterialViewModel sitePrintData)
        {

            string[] documentTypeIds = sitePrintData.SelectedDocTypes.Split(',');


            RequestMaterialPrintHistory printDetails = new RequestMaterialPrintHistory();


            Guid uniqueId = new Guid();
            uniqueId = Guid.NewGuid();
            printDetails.ClientCompanyName = sitePrintData.CompanyName;
            printDetails.ClientFirstName = sitePrintData.FirstName;
            printDetails.ClientLastName = sitePrintData.LastName;
            printDetails.Address1 = sitePrintData.Address1;
            printDetails.Address2 = sitePrintData.Address2;
            printDetails.City = sitePrintData.City;
            printDetails.StateOrProvince = sitePrintData.StateOrProvince;
            printDetails.IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            printDetails.UserAgent = Request.ServerVariables["HTTP_USER_AGENT"];
            printDetails.Quantity = 1;
            printDetails.UniqueID = uniqueId;
            printDetails.PostalCode = sitePrintData.PostalCode;
            printDetails.RequestBatchId = Guid.Parse(sitePrintData.RequestBatchId.ToString());
            printDetails.RequestUriString = Request.Url.ToString();
            printDetails.Referer = string.IsNullOrWhiteSpace(Request.ServerVariables["HTTP_REFERER"]) ? "" : Request.ServerVariables["HTTP_REFERER"];

            TaxonomyAssociationData taxonomyData = new TaxonomyAssociationData();

            taxonomyData.TaxonomyAssociationID = Convert.ToInt32(sitePrintData.TaxanomyAssociationId);
            taxonomyData.DocumentTypes = new List<HostedDocumentType>();
            foreach (var item in documentTypeIds)
            {
                taxonomyData.DocumentTypes.Add(new HostedDocumentType { DocumentTypeId = int.Parse(item.Split(':')[0].Trim()), DocumentTypeLinkText = item.Split(':')[1].Trim() });
            }

            printDetails.TaxonomyAssociationData = taxonomyData;
            hostedRequestMaterialDetails.SavePrintDetails(sitePrintData.ClientName, sitePrintData.SiteName, printDetails);



        }
        /// <summary>
        /// Sends the request material mail.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="site">The site.</param>
        /// <param name="documentTypes">The document types.</param>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <param name="receipEmail">The receip email.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool SendRequestMaterialMail(string clientName, string site, List<HostedDocumentType> documentTypes, Guid uniqueId, string receipEmail)
        {

            try
            {
                StringBuilder emailContent = new StringBuilder();
                emailContent.Append("If you wish to retain a permanent copy, you may access this link and download the materials to your personal computer.");
                emailContent.Append("<ul>");
                foreach (var item in documentTypes)
                {
                    string url = GetBaseUrl() + clientName + "/DRMDocs/" + uniqueId + "/" + item.DocumentTypeId;
                    if (!string.IsNullOrWhiteSpace(site))
                    {
                        url = url + "?site=" + site;
                    }
                    emailContent.Append("<li><a href='" + url + "' target='_blank'>" + item.DocumentTypeLinkText + "</a></li>");
                }

                emailContent.Append("</ul>");
                EmailHelper.SendEmail(ConfigValues.RequestMaterialEmailFrom, receipEmail, "Test Email", emailContent.ToString());

                return true;
            }

            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Displays the request material docs.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="site">The site.</param>
        /// <param name="externalID1">The external i d1.</param>
        /// <param name="externalID2">The external i d2.</param>
        /// <returns>ActionResult.</returns>
        [Route("{customer}/DRMDocs/{externalID1}/{externalID2}")]
        public ActionResult DisplayRequestMaterialDocs(string customer, string site, string externalID1, string externalID2)
        {
            Guid uniqueId;
            int documentTypeId;
            if (Guid.TryParse(externalID1, out uniqueId) && int.TryParse(externalID2, out documentTypeId))
            {
                int taxonomyAsociationId = hostedRequestMaterialDetails.UpdateEmailClickDate(customer, uniqueId, documentTypeId);
                if (taxonomyAsociationId > 0)
                {
                    return TaxonomySpecificDocumentFrame(customer, site, taxonomyAsociationId.ToString(), externalID2, true, false);
                }
            }

            return RedirectToAction("Index", "Error", new { ErrorCode = "404" });

        }

        /// <summary>
        /// Taxonomy Association Group Documents
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="site"></param>
        /// <param name="isProofing"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), Route("{customer}/TAGD")]
        public ActionResult TaxonomyAssociationGroupDocuments(string customer, string site, bool? isProofing = false, int? Id = null)
        {
            if (!ValidateURL(customer, site))
            {
                return RedirectToAction("Index", "Error", new { ErrorCode = "600" });
            }
            base.SetContext(customer);
            TaxonomyAssociationGroupViewModel viewModel = new TaxonomyAssociationGroupViewModel();
            viewModel.ClientName = customer;
            viewModel.SiteName = site;
            viewModel.IsProofing = isProofing.Value ? 1 : 0;
            viewModel.BaseURL = GetBaseUrl();
            viewModel.PageId = hostedClientPageScenarios.GetPageIDForPageName("TAGD");
            viewModel.PageCSSResourceKey = "TAGD_CSSFile";

            bool getProductionVersion = !isProofing.Value;
            //Get Site Text
            List<HostedSiteText> cachedSiteText = hostedClientPageScenarios.GetSiteTextFromCache(customer, site).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            TextHelper.GetSiteText(viewModel, cachedSiteText);
            //Get Page Text 
            List<HostedPageText> cachedPageText = hostedClientPageScenarios.GetPageTextFromCache(customer, site, viewModel.PageId).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            TextHelper.GetPageText(viewModel, cachedPageText, "TAGD");

            viewModel.DisplayTAGDFooterTemplate = hostedClientPageScenarios.GetStaticResourcesFromCache(customer).Exists(p => p.FileName == "TAGDFooterTemplate");

            //Get All necessary Features
            FeatureModeHelper<TaxonomyAssociationGroupViewModel> featureMode = new FeatureModeHelper<TaxonomyAssociationGroupViewModel>();
            List<Type> featureTypes = new List<Type>();
            featureTypes.Add(typeof(FeatureEnums.AllCategories));
            featureTypes.Add(typeof(FeatureEnums.SARValidation));
            featureTypes.Add(typeof(FeatureEnums.XBRL));
            featureTypes.Add(typeof(FeatureEnums.ShowDocumentDate));
            featureMode.GetFeatureModes(ref viewModel, featureTypes);          
           

            int browserAlertFeatureMode = hostedClientPageScenarios.GetSiteFeatureModeFromCache(customer, site, "BrowserAlert");

            List<FeatureEnums.BrowserAlert> featureModesBrowserAlert = FeatureHelper.GetFeatureModesBrowserAlert(browserAlertFeatureMode);
            if (featureModesBrowserAlert.Contains(FeatureEnums.BrowserAlert.Enabled))
            {
                BrowserVersionObjectModel browserVersionObjectModel = hostedClientPageScenarios.GetBrowserVersion(customer, site, Request.Browser.Browser);
                if (browserVersionObjectModel != null)
                {
                    viewModel.DisplayBrowserAlert = true;
                    viewModel.BrowserDetails = FeatureHelper.ValidateLatestBrowser(browserVersionObjectModel, Request.Browser);
                }
            }

            viewModel.TaxonomyAssociationGroupModelData = hostedClientPageScenarios.GetTaxonomyAssociationGroupDocuments(customer, string.IsNullOrEmpty(site) ? null : site, Id == 0 ? null : Id);
            viewModel.TAGDDetails = viewModel.TaxonomyAssociationGroupModelData.TAGDetails;

                if(viewModel.IsSARValidationEnabled)
                {
                    viewModel.TaxonomyAssociationGroupModelData.TAGTAData.
                                       ForEach(x => x.TaxonomyAssociationData.
                                       ForEach(y => y.DocumentTypes.Remove(y.DocumentTypes.Find(z => z.VerticalMarketID == "SAR" && z.DocumentDate <= y.DocumentTypes.Find(t => t.VerticalMarketID == "AR").DocumentDate))));

                }
                

            
            viewModel.TAGDDetails.Add(new { Id = 0, Name = "All Categories" });
            viewModel.SelectedTAGDId = Id == null ? 0 : Id;

            if (viewModel.TaxonomyAssociationGroupModelData.TAGDetails.Count == 0)
            {
                if (cachedSiteText.Exists(p => p.ResourceKey == "TaxonomyNotAvailableText"))
                {
                    viewModel.TaxonomyNotAvailableText = cachedSiteText.Find(p => p.ResourceKey == "TaxonomyNotAvailableText").Text;
                }

                return View("TaxonomyDataNotAvailable", viewModel);
            }

            string returnViewName = "";

            switch (hostedClientPageScenarios.GetTemplateName(customer, site))
            {
                case "Default":
                    returnViewName = "TaxonomyAssociationGroupDocumentsT1";
                    break;
            }
            return View(returnViewName, viewModel);
        }
        /// <summary>
        /// Partial View to display GRID for Taxonomy Association Group Documents
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="site"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public PartialViewResult TaxonomyAssoicationGroupTaxonomyAssoicationGrid(string customer, string site, bool? isProofing = false, int? Id = null)
        {
            TaxonomyAssociationGroupViewModel viewModel = new TaxonomyAssociationGroupViewModel();
            viewModel.ClientName = customer;
            viewModel.SiteName = site;
            bool getProductionVersion = !isProofing.Value;
            viewModel.PageId = hostedClientPageScenarios.GetPageIDForPageName("TAGD");
           
            //Get Page Text 
            List<HostedPageText> cachedPageText = hostedClientPageScenarios.GetPageTextFromCache(customer, site, viewModel.PageId).FindAll(p => p.IsCurrentProductionVersion == getProductionVersion);
            TextHelper.GetPageText(viewModel, cachedPageText, "TAGD");

            //Get All necessary Features
            FeatureModeHelper<TaxonomyAssociationGroupViewModel> featureMode = new FeatureModeHelper<TaxonomyAssociationGroupViewModel>();
            List<Type> featureTypes = new List<Type>();            
            featureTypes.Add(typeof(FeatureEnums.ShowDocumentDate));
            featureMode.GetFeatureModes(ref viewModel, featureTypes);
            
            viewModel.TaxonomyAssociationGroupModelData = hostedClientPageScenarios.GetTaxonomyAssociationGroupDocuments(customer, string.IsNullOrEmpty(site) ? null : site, Id == 0 ? null : Id);
            return PartialView("TaxonomyAssociationGroupPartialT1", viewModel);
        }
    }
}