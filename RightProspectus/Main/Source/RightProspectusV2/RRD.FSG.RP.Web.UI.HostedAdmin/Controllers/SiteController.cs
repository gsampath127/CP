// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************

using RP.Extensions;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
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
    /// <summary>
    /// To Hold the Site related change
    /// </summary>
    public class SiteController : BaseController
    {
        #region Constants
        ///<summary>
        ///Constants
        /// </summary>
        private const string defaultvalue = "-1";
        private const string DefaultTemplateName = "--Please select Template Name--";
        private const string DefaultPage = "--Please select Page--";
        private const string DefaultTemplatefirst = "--Please select Template First--";
        private const string DefaultPageName = "--Please select Default Page--";

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
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        /// <summary>
        /// The template text cache factory
        /// </summary>
        private IFactoryCache<TemplateTextFactory, TemplateTextObjectModel, TemplateTextKey> templateTextCacheFactory;
        /// <summary>
        /// The template page text cache factory
        /// </summary>
        private IFactoryCache<TemplatePageTextFactory, TemplatePageTextObjectModel, TemplatePageTextKey> templatePageTextCacheFactory;
        /// <summary>
        /// The template navigation cache factory
        /// </summary>
        private IFactoryCache<TemplateNavigationFactory, TemplateNavigationObjectModel, TemplateNavigationKey> templateNavigationCacheFactory;
        /// <summary>
        /// The template page navigation cache factory
        /// </summary>
        private IFactoryCache<TemplatePageNavigationFactory, TemplatePageNavigationObjectModel, TemplatePageNavigationKey> templatePageNavigationCacheFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteController" /> class.
        /// </summary>
        public SiteController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
                siteCacheFactory.ClientName = SessionClientName;
                siteCacheFactory.Mode = FactoryCacheMode.All;

                templatePageCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>>("TemplatePage");
                templatePageCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;

                templateTextCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplateTextFactory, TemplateTextObjectModel, TemplateTextKey>>("TemplateText");
                templateTextCacheFactory.Mode = FactoryCacheMode.All;

                templatePageTextCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplatePageTextFactory, TemplatePageTextObjectModel, TemplatePageTextKey>>("TemplatePageText");
                templatePageTextCacheFactory.Mode = FactoryCacheMode.All;

                templateNavigationCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplateNavigationFactory, TemplateNavigationObjectModel, TemplateNavigationKey>>("TemplateNavigation");
                templateNavigationCacheFactory.Mode = FactoryCacheMode.All;

                templatePageNavigationCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplatePageNavigationFactory, TemplatePageNavigationObjectModel, TemplatePageNavigationKey>>("TemplatePageNavigation");
                templatePageNavigationCacheFactory.Mode = FactoryCacheMode.All;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteController"/> class.
        /// </summary>
        /// <param name="SiteCacheFactory">The site cache factory.</param>
        /// <param name="TemplatePageCacheFactory">The template page cache factory.</param>
        /// <param name="UserCacheFactory">The user cache factory.</param>
        /// <param name="TemplateTextCacheFactory">The template text cache factory.</param>
        /// <param name="TemplatePageTextCacheFactory">The template page text cache factory.</param>
        /// <param name="TemplateNavigationCacheFactory">The template navigation cache factory.</param>
        /// <param name="TemplatePageNavigationCacheFactory">The template page navigation cache factory.</param>
        public SiteController(IFactoryCache<SiteFactory, SiteObjectModel, int> SiteCacheFactory,
           
            IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey> TemplatePageCacheFactory,
            IFactoryCache<UserFactory, UserObjectModel, int> UserCacheFactory,
            IFactoryCache<TemplateTextFactory, TemplateTextObjectModel, TemplateTextKey> TemplateTextCacheFactory,
            IFactoryCache<TemplatePageTextFactory, TemplatePageTextObjectModel, TemplatePageTextKey> TemplatePageTextCacheFactory,
            IFactoryCache<TemplateNavigationFactory, TemplateNavigationObjectModel, TemplateNavigationKey> TemplateNavigationCacheFactory,
            IFactoryCache<TemplatePageNavigationFactory, TemplatePageNavigationObjectModel, TemplatePageNavigationKey> TemplatePageNavigationCacheFactory)
        {
            siteCacheFactory = SiteCacheFactory;
            siteCacheFactory.ClientName = SessionClientName;
            siteCacheFactory.Mode = FactoryCacheMode.All;

            templatePageCacheFactory = TemplatePageCacheFactory;
            templatePageCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = UserCacheFactory;
            userCacheFactory.Mode = FactoryCacheMode.All;

            templateTextCacheFactory = TemplateTextCacheFactory;
            templateTextCacheFactory.Mode = FactoryCacheMode.All;

            templatePageTextCacheFactory = TemplatePageTextCacheFactory;
            templatePageTextCacheFactory.Mode = FactoryCacheMode.All;

            templateNavigationCacheFactory = TemplateNavigationCacheFactory;
            templateNavigationCacheFactory.Mode = FactoryCacheMode.All;

            templatePageNavigationCacheFactory = TemplatePageNavigationCacheFactory;
            templatePageNavigationCacheFactory.Mode = FactoryCacheMode.All;
        }

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
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

        #region GetAllPageTextDetails
        /// <summary>
        /// Gets all site details.
        /// </summary>
        /// <param name="siteName">The site name.</param>
        /// <param name="templateID">The template identifier.</param>
        /// <param name="pageID">The page identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllSiteDetails(string siteName, string templateID, string pageID)
        {
            int id;
            SiteSearchDetail objSearchDetail = new SiteSearchDetail()
            {
                Name = string.IsNullOrEmpty(siteName) ? null : siteName,
                TemplateId = !(string.IsNullOrEmpty(templateID)) ? (int.TryParse(templateID, out id) ? id : 0) : (int?)null,
                DefaultPageId = !(string.IsNullOrEmpty(pageID)) ? (int.TryParse(pageID, out id) ? id : 0) : (int?)null
            };
            
            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            SiteSortColumn SortColumn = SiteSortColumn.Name;
            switch (kendoGridPost.SortColumn)
            {
                case "TemplateName":
                    SortColumn = SiteSortColumn.TemplateName;
                    break;
                case "DefaultPageName":
                    SortColumn = SiteSortColumn.DefaultPageName;
                    break;
                case "PageDescription":
                    SortColumn = SiteSortColumn.PageDescription;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            return Json(new
            {
                total = siteCacheFactory.GetEntitiesBySearch(objSearchDetail).Select(x => x.SiteID).Count(),
                data = (from siteObjectModel in siteCacheFactory.GetEntitiesBySearch(
                            startRowIndex,
                            kendoGridPost.PageSize,
                            objSearchDetail,
                            new SiteSortDetail() { Column = SortColumn, Order = sortOrder })
                        select new
                        {
                            SiteID = siteObjectModel.SiteID,
                            SiteName = siteObjectModel.Name,
                            DefaultPageName = siteObjectModel.DefaultPageName,
                            PageDescription = siteObjectModel.PageDescription,
                            TemplateName = siteObjectModel.TemplateName,
                            DefaultPageID = siteObjectModel.DefaultPageId,
                            TemplateID = siteObjectModel.TemplateId,
                            Description = siteObjectModel.Description
                        })
            });
        }
        #endregion

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
        /// Gets the template names.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetTemplateNames()
        {
            return Json((from dbo in siteCacheFactory.GetAllEntities()
                         select new { Display = dbo.TemplateName, Value = dbo.TemplateId }).Distinct(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the default page names.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetDefaultPageNames()
        {
            return Json((from dbo in siteCacheFactory.GetAllEntities()
                         select new { Display = dbo.DefaultPageName, Value = dbo.DefaultPageId }).Distinct(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Disable Page text
        /// <summary>
        /// Deletes the site.
        /// </summary>
        /// <param name="siteID">The site identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DeleteSite(int siteID)
        {
            siteCacheFactory.DeleteEntity(siteID, SessionUserID);
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ActionMethod For Edit

        /// <summary>
        /// Edits the site.
        /// </summary>
        /// <param name="siteID">The site identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult EditSite(int siteID)
        {
            EditSiteViewModel viewModel = new EditSiteViewModel()
            {
                PageDescriptions = new List<DisplayValuePair>(),
                TemplateNames = new List<DisplayValuePair>(),
                BaseURL = GetBaseUrl()
            };

            // Using TemplatePageFactory instead of TemplateFactory.
            IEnumerable<TemplatePageObjectModel> templatePageDetails = templatePageCacheFactory.GetAllEntities();
        
            viewModel.TemplateNames.Add(new DisplayValuePair { Value = defaultvalue, Display = DefaultTemplateName });
            foreach (var item in (from dbo in templatePageDetails
                                  select new { dbo.TemplateName, dbo.TemplateID }).Distinct())
            {
                viewModel.TemplateNames.Add(new DisplayValuePair { Display = item.TemplateName, Value = item.TemplateID.ToString() });
            }

            if (siteID > 0)
            {
                SiteObjectModel siteDetails = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = siteID }).FirstOrDefault();

                viewModel.SiteID = siteDetails.SiteID;
                viewModel.SiteName = siteDetails.Name;
                viewModel.SelectedTemplateID = siteDetails.TemplateId;
                viewModel.SelectedDefaultPageNameID = siteDetails.DefaultPageId;
                viewModel.Description = siteDetails.Description;
                viewModel.IsDefaultSite = siteDetails.IsDefaultSite;
               
                viewModel.PageDescriptions.Add(new DisplayValuePair { Value = defaultvalue, Display = DefaultPage });
                foreach (var item in (from dbo in templatePageDetails
                                      where dbo.TemplateID == siteDetails.TemplateId
                                      select new { dbo.PageDescription, dbo.PageID }).Distinct())
                {
                    viewModel.PageDescriptions.Add(new DisplayValuePair { Display = item.PageDescription, Value = item.PageID.ToString() });
                }
            }
            else
            {
                if (siteCacheFactory.GetAllEntities().Count() == 0)
                {
                    viewModel.IsDefaultSite = true;
                    viewModel.DisableDefaultSiteCheckbox = true;
                }
                viewModel.PageDescriptions.Add(new DisplayValuePair { Value = defaultvalue, Display = DefaultTemplatefirst });
            }
            ViewData["Success"] = "In Progress";
            return View(viewModel);
        }

        /// <summary>
        /// Edits the site.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult EditSite(EditSiteViewModel viewModel)
        {
            try
            {
                SiteObjectModel siteObjectModel = new SiteObjectModel()
                {
                    SiteID = viewModel.SiteID,
                    Name = viewModel.SiteName,
                    TemplateId = viewModel.SelectedTemplateID,
                    DefaultPageId = viewModel.SelectedDefaultPageNameID,
                    Description = viewModel.Description,
                    IsDefaultSite = viewModel.DisableDefaultSiteCheckbox ? viewModel.DisableDefaultSiteCheckbox : viewModel.IsDefaultSite,
                    ClientId = SessionClientID
                };
                siteObjectModel.TemplateText = new DataTable();
                siteObjectModel.TemplateText.Columns.Add("ResourceKey", typeof(string));
                siteObjectModel.TemplateText.Columns.Add("DefaultText", typeof(string));

                siteObjectModel.TemplateNavigation = new DataTable();
                siteObjectModel.TemplateNavigation.Columns.Add("NavigationKey", typeof(string));
                siteObjectModel.TemplateNavigation.Columns.Add("DefaultNavigationXml", typeof(string));

                siteObjectModel.TemplatePageText = new DataTable();
                siteObjectModel.TemplatePageText.Columns.Add("PageId", typeof(int));
                siteObjectModel.TemplatePageText.Columns.Add("ResourceKey", typeof(string));
                siteObjectModel.TemplatePageText.Columns.Add("DefaultText", typeof(string));

                siteObjectModel.TemplatePageNavigation = new DataTable();
                siteObjectModel.TemplatePageNavigation.Columns.Add("PageId", typeof(int));
                siteObjectModel.TemplatePageNavigation.Columns.Add("NavigationKey", typeof(string));
                siteObjectModel.TemplatePageNavigation.Columns.Add("DefaultNavigationXml", typeof(string));

                if (viewModel.SiteID == 0)
                {
                    LoadDefaultSiteSettings(siteObjectModel);
                }
                
                siteCacheFactory.SaveEntity(siteObjectModel, SessionUserID);

                IEnumerable<TemplatePageObjectModel> templatePageDetails = templatePageCacheFactory.GetAllEntities();
               
                viewModel.PageDescriptions = new List<DisplayValuePair>();
                viewModel.PageDescriptions.Add(new DisplayValuePair { Value = defaultvalue, Display = DefaultPage });
                foreach (var item in (from dbo in templatePageDetails
                                      select new { dbo.PageDescription, dbo.PageID }).Distinct())
                {
                    viewModel.PageDescriptions.Add(new DisplayValuePair { Display = item.PageDescription, Value = item.PageID.ToString() });
                }

                viewModel.TemplateNames = new List<DisplayValuePair>();
                viewModel.TemplateNames.Add(new DisplayValuePair { Value = defaultvalue, Display = DefaultTemplateName });
                foreach (var item in (from dbo in templatePageDetails
                                      select new { dbo.TemplateName, dbo.TemplateID }).Distinct())
                {
                    viewModel.TemplateNames.Add(new DisplayValuePair { Display = item.TemplateName, Value = item.TemplateID.ToString() });
                }
                
                SessionSiteExist = true;               

                viewModel.BaseURL = GetBaseUrl();
                ViewData["Success"] = "OK";
            }
            catch (Exception exception)
            {
                viewModel.SuccessOrFailedMessage = exception.Message;
            }
            return View(viewModel);
        }

        /// <summary>
        /// Loads the default site settings.
        /// </summary>
        /// <param name="siteObjectModel">The site object model.</param>
        private void LoadDefaultSiteSettings(SiteObjectModel siteObjectModel)
        {
            //Load TemplateText from SystemDB            
            foreach (var item in templateTextCacheFactory.GetEntitiesBySearch(new TemplateTextSearchDetail { TemplateID = siteObjectModel.TemplateId }))
            {
                siteObjectModel.TemplateText.Rows.Add(item.ResourceKey, item.DefaultText);
            }

            //Load Template navigation from SystemDB            
            foreach (var item in templateNavigationCacheFactory.GetEntitiesBySearch(new TemplateNavigationSearchDetail { TemplateID = siteObjectModel.TemplateId }))
            {
                siteObjectModel.TemplateNavigation.Rows.Add(item.NavigationKey, item.DefaultNavigationXml);
            }
          
            IEnumerable<TemplatePageObjectModel> pageDetails = templatePageCacheFactory.GetEntitiesBySearch(new TemplatePageSearchDetail { TemplateID = siteObjectModel.TemplateId });

            switch (templatePageCacheFactory.GetEntitiesBySearch(new TemplatePageSearchDetail { TemplateID = siteObjectModel.TemplateId, PageID = siteObjectModel.DefaultPageId }).FirstOrDefault().PageName)
            {
                case "TAL":
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "TAL").FirstOrDefault().PageID);
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "TAHD").FirstOrDefault().PageID);
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "TADF").FirstOrDefault().PageID);
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "XBRL").FirstOrDefault().PageID);
                    break;
                case "TAHD":
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "TAHD").FirstOrDefault().PageID);
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "TADF").FirstOrDefault().PageID);
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "XBRL").FirstOrDefault().PageID);
                    break;
                case "TADF":
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "TADF").FirstOrDefault().PageID);
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "XBRL").FirstOrDefault().PageID);
                    break;
                case "TAD":
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "TAD").FirstOrDefault().PageID);
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "TADF").FirstOrDefault().PageID);
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "XBRL").FirstOrDefault().PageID);
                    break;
                case "XBRL":
                    LoadPageTextPageNavigationDefaultSiteSettings(siteObjectModel, pageDetails.Where(p => p.PageName == "XBRL").FirstOrDefault().PageID);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Loads the page text page navigation default site settings.
        /// </summary>
        /// <param name="siteObjectModel">The site object model.</param>
        /// <param name="pageID">The page identifier.</param>
        private void LoadPageTextPageNavigationDefaultSiteSettings(SiteObjectModel siteObjectModel, int pageID)
        {
            foreach (var item in templatePageTextCacheFactory.GetEntitiesBySearch(new TemplatePageTextSearchDetail { TemplateID = siteObjectModel.TemplateId, PageID = pageID }))
            {
                siteObjectModel.TemplatePageText.Rows.Add(item.PageID, item.ResourceKey, item.DefaultText);
            }

            foreach (var item in templatePageNavigationCacheFactory.GetEntitiesBySearch(new TemplatePageNavigationSearchDetail { TemplateID = siteObjectModel.TemplateId, PageId = pageID }))
            {
                siteObjectModel.TemplatePageNavigation.Rows.Add(item.PageID, item.NavigationKey, item.DefaultNavigationXml);
            }
        }
        #endregion

        /// <summary>
        /// Checks the site name already exists.
        /// </summary>
        /// <param name="siteName">Name of the site.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckSiteNameAlreadyExists(string siteName)
        {
            bool isSiteExists = false;
            var siteEntity = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { Name = siteName });

            if (siteEntity != null && siteEntity.Count() > 0)
            {
                isSiteExists = true;
            }
            return Json(isSiteExists, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Loads the default page names.
        /// </summary>
        /// <param name="selectedTemplateID">The selected template identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult LoadDefaultPageNames(int selectedTemplateID)
        {
            List<DisplayValuePair> lstDisplayValuePair = new List<DisplayValuePair>();
            lstDisplayValuePair.Add(new DisplayValuePair { Value = defaultvalue, Display = DefaultPageName });

            foreach (var key in templatePageCacheFactory.GetEntitiesBySearch(new TemplatePageSearchDetail { TemplateID = selectedTemplateID }))
            {
                lstDisplayValuePair.Add(new DisplayValuePair
                {
                    Display = key.PageDescription,
                    Value = key.PageID.ToString()
                });
            }
            return Json(lstDisplayValuePair, JsonRequestBehavior.AllowGet);
        }
    }
}