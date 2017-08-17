using Newtonsoft.Json.Linq;
using RP.Utilities;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.Keys;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    public class VerticalDataImportController : BaseController
    {

        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;
        /// <summary>
        /// The VerticalDataImportFactory
        /// </summary>
        private IVerticalDataImportFactory verticalDataImportFactory;
        /// <summary>
        /// The taxonamy association factory
        /// </summary>
        private IFactory<TaxonomyAssociationObjectModel, int> taxonomyAssociationFactory;
        /// <summary>
        /// The document type association factory
        /// </summary>
        private IFactory<DocumentTypeAssociationObjectModel, int> documentTypeAssociationFactory;

        /// <summary>
        /// The Site Feature cache factory
        /// </summary>
        private IFactoryCache<SiteFeatureFactory, SiteFeatureObjectModel, SiteFeatureKey> siteFeatureCacheFactory;

        private const string forwardSlash = "/";
        private const string isInternalTAID = "?isInternalTAID=true";
        /// <summary>
        /// Constructor to intialize factory
        /// </summary>
        public VerticalDataImportController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
                siteCacheFactory.ClientName = SessionClientName;
                siteCacheFactory.Mode = FactoryCacheMode.All;

                verticalDataImportFactory = RPV2Resolver.Resolve<IVerticalDataImportFactory>();
                verticalDataImportFactory.ClientName = SessionClientName;
                verticalDataImportFactory.UserId = SessionUserID;

                taxonomyAssociationFactory = RPV2Resolver.Resolve<IFactory<TaxonomyAssociationObjectModel, int>>("TaxonomyAssociationFactory");
                taxonomyAssociationFactory.ClientName = SessionClientName;

                documentTypeAssociationFactory = RPV2Resolver.Resolve<IFactory<DocumentTypeAssociationObjectModel, int>>("DocumentTypeAssociationFactory");
                documentTypeAssociationFactory.ClientName = SessionClientName;

                siteFeatureCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFeatureFactory, SiteFeatureObjectModel, SiteFeatureKey>>("SiteFeature");
                siteFeatureCacheFactory.ClientName = SessionClientName;
                siteFeatureCacheFactory.Mode = FactoryCacheMode.All;
            }


        }
        // GET: VerticalDataImport
        public ActionResult Index()
        {
            ViewData["SelectedCustomer"] = SessionClientName;
            return View();
        }
      
        //For Approving the Proofing Version
        [HttpPost]
        public string ApproveProofing(int siteId)
        {
            return verticalDataImportFactory.ApproveProofing(siteId);
        }
        /// <summary>
        /// Gets the site names.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetSiteNames()
        {

           
            return Json((from dbo in siteCacheFactory.GetAllEntities()
                         select new { Name = dbo.Name, 
                             Id = dbo.SiteID, 
                             DefaultPage = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = dbo.SiteID }).FirstOrDefault().DefaultPageName ,
                             FundOrder = GetFeatureForFundOrder(dbo.SiteID)
                         }),
                             JsonRequestBehavior.AllowGet);
        }

    

        /// <summary>
        /// GetHostedSiteProofingURL.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetHostedSiteProofingURL(int siteId)
        {
            StringBuilder hostedSiteURL = new StringBuilder();

            string defaultPageName = siteCacheFactory.GetEntityByKey(siteId).DefaultPageName;
            hostedSiteURL
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

                        hostedSiteURL
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
                case "TAGD":
                    break;
                case "XBRL":
                    {
                        var taxonomyAssociation = taxonomyAssociationFactory.GetAllEntities().FirstOrDefault(p => p.SiteId.HasValue);
                        hostedSiteURL
                            .Append(forwardSlash)
                            .Append(taxonomyAssociation == null ? 0 : taxonomyAssociation.TaxonomyAssociationId)
                            .Append(isInternalTAID);
                    } break;
                default:
                    break;
            }

            hostedSiteURL
                .Append((hostedSiteURL.ToString().Contains("?") ? "&site=" : "?site="))
                .Append(siteCacheFactory.GetEntityByKey(siteId).Name);

            
            return Json(new
            {
                production = hostedSiteURL.ToString(),
                proofing = hostedSiteURL
                    .Append((hostedSiteURL.ToString().Contains("?") ? "&isProofing=true" : "?isProofing=true")).ToString()
            }, JsonRequestBehavior.AllowGet);
      
        }

        /// <summary>
        /// Get All Feature Modes for FundOrder
        /// </summary>
        /// <param name="mode">The mode.</param>

        private string GetFeatureForFundOrder(int siteId)
        {
            int mode = 0;
            var featureData= siteFeatureCacheFactory.GetEntitiesBySearch(
                     new SiteFeatureSearchDetail()
                     {
                         SiteId = siteId,
                         SiteKey = "FundOrder"
                     });
           if( featureData.Any())
           {
               mode = featureData.FirstOrDefault().FeatureMode;
           }
            string feature = string.Empty;
            FeatureEnums.FundOrder featureMode = (FeatureEnums.FundOrder)mode;
         

            if (featureMode.HasFlag(FeatureEnums.FundOrder.AlphabeticalAsc))
            {
                feature = FeatureEnums.FundOrder.AlphabeticalAsc.ToString();
            }
            if (featureMode.HasFlag(FeatureEnums.FundOrder.AlphabeticalDesc))
            {
                feature = FeatureEnums.FundOrder.AlphabeticalDesc.ToString();
            }
            if (featureMode.HasFlag(FeatureEnums.FundOrder.CustomizeOrder))
            {
                 feature = FeatureEnums.FundOrder.CustomizeOrder.ToString();
            }

            return feature;
        }
    }
}
