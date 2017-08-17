using RP.Extensions;
using RP.Utilities;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.HostedPages;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces.HostedPages;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.HostedAdmin.Common;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    public class UrlGeneratorController : BaseController
    {

        #region Constants
        ///<summary>
        ///Constants
        ///</summary>


        #endregion

        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;
        /// <summary>
        /// The taxonomy cache factory
        /// </summary>
        private IFactoryCache<TaxonomyAssociationFactory, TaxonomyAssociationObjectModel, int> taxonomyAssociationCacheFactory;

        //private IFactoryCache<HostedClientPageScenariosFactory, UrlGenerationObjectModel, int> hostedClientPageScenariosFactory;

        private IHostedClientPageScenariosFactory hostedClientPageScenariosFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlGeneratorController" /> class.
        /// </summary>
        public UrlGeneratorController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
                siteCacheFactory.ClientName = SessionClientName;
                siteCacheFactory.Mode = FactoryCacheMode.All;

                taxonomyAssociationCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TaxonomyAssociationFactory, TaxonomyAssociationObjectModel, int>>("TaxonomyAssociation");
                taxonomyAssociationCacheFactory.ClientName = SessionClientName;
                taxonomyAssociationCacheFactory.Mode = FactoryCacheMode.All;

                hostedClientPageScenariosFactory = RPV2Resolver.Resolve<IHostedClientPageScenariosFactory>();

            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlGeneratorController"/> class.
        /// </summary>
        /// <param name="SiteCacheFactory">The site cache factory.</param>
        /// <param name="TaxonomyAssociationCacheFactory">The taxonomy association cache factory.</param>

        public UrlGeneratorController(IFactoryCache<SiteFactory, SiteObjectModel, int> SiteCacheFactory,
            IFactoryCache<TaxonomyAssociationFactory, TaxonomyAssociationObjectModel, int> TaxonomyAssociationCacheFactory)
        {
            siteCacheFactory = SiteCacheFactory;
            siteCacheFactory.ClientName = SessionClientName;
            siteCacheFactory.Mode = FactoryCacheMode.All;

            taxonomyAssociationCacheFactory = TaxonomyAssociationCacheFactory;
            taxonomyAssociationCacheFactory.ClientName = SessionClientName;
            taxonomyAssociationCacheFactory.Mode = FactoryCacheMode.All;

        }

        // GET: UrlGenerator
        public ActionResult List()
        {
            ViewData["SelectedSite"] = SessionSiteName;
            ViewData["SelectedCustomer"] = SessionClientName;
            return View();
        }

        /// <summary>
        /// Sites the configuration.
        /// </summary>
        /// <param name="SiteID">The site identifier.</param>
        /// <param name="SiteName">Name of the site.</param>
        /// <returns>ActionResult.</returns>
        [IsPopUp]
        public ActionResult SiteConfiguration(int? SiteID, string SiteName)
        {
            if (SiteID != null)
            {
                SessionSiteID = SiteID.Value;
                SessionSiteName = SiteName;
            }
            ViewData["SelectedSite"] = SessionSiteName;
            ViewData["SelectedCustomer"] = SessionClientName;
            return View();
        }



        #region Load Search Creteria Methods.

        /// <summary>
        /// Gets the site names.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetSiteNames()
        {
            return Json((from dbo in siteCacheFactory.GetAllEntities()
                         select new { Display = dbo.Name, Value = dbo.Name }).Distinct(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the Market Ids.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetMarketId()
        {
            return Json((from dbo in taxonomyAssociationCacheFactory.GetAllEntities()
                         select new { Display = dbo.MarketId, Value = dbo.MarketId }).Distinct(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GetAllSiteDetails_Returns_JsonResult

        /// <summary>
        /// Gets all Site Detailsls.
        /// </summary>

        /// <param name="userId">User Id.</param>
        /// <returns>JsonResult.</returns>

        public void GetAllUrlDetails(string siteName, string marketID)
        {

            List<UrlGenerationObjectModel> lstUrl = hostedClientPageScenariosFactory.GetTaxonomyAssociationforUrlGeneration(SessionClientName, marketID, siteName);
            DataTable urlGenerationTable = new DataTable();
            urlGenerationTable.Columns.Add("Site Name", typeof(string));
            urlGenerationTable.Columns.Add("Fund Name", typeof(string));
            urlGenerationTable.Columns.Add("Market ID", typeof(string));
            urlGenerationTable.Columns.Add("Document Type", typeof(string));
            urlGenerationTable.Columns.Add("HTTP Public Url", typeof(string));
            urlGenerationTable.Columns.Add("HTTP Private Url", typeof(string));
            urlGenerationTable.Columns.Add("HTTPS Public Url", typeof(string));
            urlGenerationTable.Columns.Add("HTTPS Private Url", typeof(string));

            foreach (UrlGenerationObjectModel obj in lstUrl)
            {
                urlGenerationTable.Rows.Add(obj.SiteName, obj.FundName, obj.TLEExternalID, obj.DocumentType, obj.PublicUrl, obj.PrivateUrl,
                                             obj.PublicUrl.Replace(ConfigValues.HostedEngineURL, ConfigValues.HTTPSHostedEngineURL),
                                             obj.PrivateUrl.Replace(ConfigValues.HostedEngineURL, ConfigValues.HTTPSHostedEngineURL));
            }
            CreateExcelFile.CreateExcelDocument(urlGenerationTable, "UrlReport.xlsx", System.Web.HttpContext.Current.Response, true);

        }
        #endregion
    }
}