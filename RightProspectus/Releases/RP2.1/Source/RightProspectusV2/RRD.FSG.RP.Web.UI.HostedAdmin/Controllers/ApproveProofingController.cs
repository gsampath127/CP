// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By :
// Last Modified On : 11-17-2015
// ***********************************************************************

using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Utilities;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// To Hold the preview related actions
    /// </summary>
    public class ApproveProofingController : BaseController
    {
        #region Constants
        ///<summary>
        ///Constants 
        ///</summary>
        private const string isInternalTAID = "?isInternalTAID=true";
        private const string forwardSlash = "/";

        #endregion

        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;
        /// <summary>
        /// The template page cache factory
        /// </summary>
        private IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey> templatePageCacheFactory;
        /// <summary>
        /// The approve proofing factory
        /// </summary>
        private IFactory<ApproveProofingObjectModel, int> approveProofingFactory;
        /// <summary>
        /// The taxonamy association factory
        /// </summary>
        private IFactory<TaxonomyAssociationObjectModel, int> taxonomyAssociationFactory;
        /// <summary>
        /// The document type association factory
        /// </summary>
        private IFactory<DocumentTypeAssociationObjectModel, int> documentTypeAssociationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApproveProofingController" /> class.
        /// </summary>
        public ApproveProofingController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
                siteCacheFactory.ClientName = SessionClientName;
                siteCacheFactory.Mode = FactoryCacheMode.All;

                templatePageCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>>("TemplatePage");
                templatePageCacheFactory.Mode = FactoryCacheMode.All;

                approveProofingFactory = RPV2Resolver.Resolve<IFactory<ApproveProofingObjectModel, int>>("ApproveProofingFactory");
                approveProofingFactory.ClientName = SessionClientName;

                taxonomyAssociationFactory = RPV2Resolver.Resolve<IFactory<TaxonomyAssociationObjectModel, int>>("TaxonomyAssociationFactory");
                taxonomyAssociationFactory.ClientName = SessionClientName;

                documentTypeAssociationFactory = RPV2Resolver.Resolve<IFactory<DocumentTypeAssociationObjectModel, int>>("DocumentTypeAssociationFactory");
                documentTypeAssociationFactory.ClientName = SessionClientName;
            }
        }

        #region Test_Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ApproveProofingController"/> class.
        /// </summary>
        /// <param name="SitefactoryCache">Site cache factory</param>
        /// <param name="TemplatePageFactoryCache">Template page cache factory</param>
        /// <param name="ApproveProofingFactory">Approve proofing factory</param>
        /// <param name="TaxonomyAssociationFactory">Taxonamy association factory</param>
        /// <param name="DocumentTypeAssociationFactory">Document type asscociation factory</param>
        public ApproveProofingController(IFactoryCache<SiteFactory, SiteObjectModel, int> SitefactoryCache,
            IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey> TemplatePageFactoryCache,
            IFactory<ApproveProofingObjectModel, int> ApproveProofingFactory,
            IFactory<TaxonomyAssociationObjectModel, int> TaxonomyAssociationFactory,
            IFactory<DocumentTypeAssociationObjectModel, int> DocumentTypeAssociationFactory)
        {
            siteCacheFactory = SitefactoryCache;
            siteCacheFactory.ClientName = SessionClientName;
            siteCacheFactory.Mode = FactoryCacheMode.All;

            templatePageCacheFactory = TemplatePageFactoryCache;
            templatePageCacheFactory.Mode = FactoryCacheMode.All;

            approveProofingFactory = ApproveProofingFactory;
            approveProofingFactory.ClientName = SessionClientName;

            taxonomyAssociationFactory = TaxonomyAssociationFactory;
            taxonomyAssociationFactory.ClientName = SessionClientName;

            documentTypeAssociationFactory = DocumentTypeAssociationFactory;
            documentTypeAssociationFactory.ClientName = SessionClientName;
        }
        #endregion

        /// <summary>
        /// Display Approve proofing.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult List()
        {
            ViewData["SelectedSite"] = SessionSiteName;
            ViewData["SelectedCustomer"] = SessionClientName;

            return View("ApproveProofing");
        }

        /// <summary>
        /// Gets the hosted site proofing URL.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetHostedSiteProofingURL()
        {
            StringBuilder hostedSiteProofingURL = new StringBuilder();

            string defaultPageName = siteCacheFactory.GetEntityByKey(SessionSiteID).DefaultPageName;
            hostedSiteProofingURL
                .Append(ConfigurationManager.AppSettings["HostedEngineURL"])
                .Append(SessionClientName);

            switch (defaultPageName)
            {
                case "TAL":
                    break;
                case "TAHD":
                    break;
                case "TADF":
                    {
                        var taxonomyAssociation = taxonomyAssociationFactory.GetAllEntities().FirstOrDefault(p => p.SiteId.HasValue);
                        var documentTypeAssociation = documentTypeAssociationFactory.GetAllEntities().FirstOrDefault(p => p.SiteId.HasValue);

                        hostedSiteProofingURL
                            .Append(forwardSlash)
                            .Append(defaultPageName)
                            .Append(forwardSlash)
                            .Append(taxonomyAssociation == null ? 0 : taxonomyAssociation.TaxonomyAssociationId)
                            .Append(forwardSlash)
                            .Append(documentTypeAssociation == null ? 0 : documentTypeAssociation.DocumentTypeId)
                            .Append(isInternalTAID);
                    } break;
                case "TAD":
                    break;
                case "XBRL":
                    {
                        var taxonomyAssociation = taxonomyAssociationFactory.GetAllEntities().FirstOrDefault(p => p.SiteId.HasValue);
                        hostedSiteProofingURL
                            .Append(forwardSlash)
                            .Append(taxonomyAssociation == null ? 0 : taxonomyAssociation.TaxonomyAssociationId)
                            .Append(isInternalTAID);
                    } break;
                default:
                    break;
            }

            hostedSiteProofingURL
                .Append((hostedSiteProofingURL.ToString().Contains("?") ? "&site=" : "?site="))
                .Append(SessionSiteName);

            return Json(hostedSiteProofingURL.ToString(), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Approve Proofing Changes
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ApproveProofingChanges()
        {
            approveProofingFactory.SaveEntity(new ApproveProofingObjectModel(), SessionUserID);
            return true;
        }
    }
}