// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-17-2015
// ***********************************************************************

using RP.Extensions;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
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
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Schema;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// Class PageNavigationController.
    /// </summary>
    public class PageNavigationController : BaseController
    {
        #region Constants
        /// <summary>
        /// Constant values for the default value of dropdownlist
        /// </summary>
        private const string dropPageText = "--Please select Page--";
        private const string dropPageNavigationText = "--Please select Navigation Key--";
        private const string dropvalue = "-1";
        private const string Versionprod = "Production";
        private const string Versionproof = "Proofing";
        #endregion

        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        /// <summary>
        /// The page navigation cache factory
        /// </summary>
        private IFactoryCache<PageNavigationFactory, PageNavigationObjectModel, PageNavigationKey> pageNavigationCacheFactory;
        /// <summary>
        /// The template page cache factory
        /// </summary>
        private IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey> templatePageCacheFactory;
        /// <summary>
        /// The template page text cache factory
        /// </summary>
        private IFactoryCache<TemplatePageNavigationFactory, TemplatePageNavigationObjectModel, TemplatePageNavigationKey> templatePageNavigationCacheFactory;

        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the RRD.FSG.RP.Web.UI.HostedAdmin.Controllers.PageNavigationController class
        /// </summary>
        public PageNavigationController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;

                pageNavigationCacheFactory = RPV2Resolver.Resolve<IFactoryCache<PageNavigationFactory, PageNavigationObjectModel, PageNavigationKey>>("PageNavigation");
                pageNavigationCacheFactory.ClientName = SessionClientName;
                pageNavigationCacheFactory.Mode = FactoryCacheMode.All;

                templatePageCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>>("TemplatePage");
                templatePageCacheFactory.Mode = FactoryCacheMode.All;

                templatePageNavigationCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplatePageNavigationFactory, TemplatePageNavigationObjectModel, TemplatePageNavigationKey>>("TemplatePageNavigation");
                templatePageNavigationCacheFactory.Mode = FactoryCacheMode.All;

                siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
                siteCacheFactory.ClientName = SessionClientName;
                siteCacheFactory.Mode = FactoryCacheMode.All;
            }
        }
        #endregion

        #region Constructor_Test
        /// <summary>
        /// Initializes a new instance of the <see cref="PageNavigationController"/> class.
        /// </summary>
        /// <param name="PageNavigationCacheFactory">The page navigation cache factory.</param>
        /// <param name="UserCacheFactory">The user cache factory.</param>
        /// <param name="TemplatePageCacheFactory">The template page cache factory.</param>
        /// <param name="TemplatePageNavigationCacheFactory">The template page navigation cache factory.</param>
        /// <param name="SiteCacheFactory">The site cache factory.</param>
        public PageNavigationController(IFactoryCache<PageNavigationFactory, PageNavigationObjectModel, PageNavigationKey> PageNavigationCacheFactory,
            IFactoryCache<UserFactory, UserObjectModel, int> UserCacheFactory,
            IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey> TemplatePageCacheFactory,
            IFactoryCache<TemplatePageNavigationFactory, TemplatePageNavigationObjectModel, TemplatePageNavigationKey> TemplatePageNavigationCacheFactory,
            IFactoryCache<SiteFactory, SiteObjectModel, int> SiteCacheFactory)
        {
            pageNavigationCacheFactory = PageNavigationCacheFactory;
            pageNavigationCacheFactory.ClientName = SessionClientName;
            pageNavigationCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = UserCacheFactory;
            userCacheFactory.Mode = FactoryCacheMode.All;

            templatePageCacheFactory = TemplatePageCacheFactory;
            templatePageCacheFactory.ClientName = SessionClientName;
            templatePageCacheFactory.Mode = FactoryCacheMode.All;

            templatePageNavigationCacheFactory = TemplatePageNavigationCacheFactory;
            templatePageNavigationCacheFactory.ClientName = SessionClientName;
            templatePageNavigationCacheFactory.Mode = FactoryCacheMode.All;

            siteCacheFactory = SiteCacheFactory;
            siteCacheFactory.ClientName = SessionClientName;
            siteCacheFactory.Mode = FactoryCacheMode.All;

        }
        #endregion

        #region ActionList
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
        #endregion

        #region GetAllPageNavigationDetails
        /// <summary>
        /// Gets all page navigation details.
        /// </summary>
        /// <param name="navigationKey">The navigation key.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <param name="version">The version.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllPageNavigationDetails(string navigationKey, string pageId, string version)
        {
            int i;
            bool? isProofing = null;
            bool isValidVersion = true;
            if (!string.IsNullOrEmpty(version))
            {
                if (version == Versionprod)
                {
                    isProofing = false;
                }
                else if (version == Versionproof)
                {
                    isProofing = true;
                }
                else
                {
                    isValidVersion = false;
                }
            }

            PageNavigationSearchDetail objPageNavigationSearchDetail = new PageNavigationSearchDetail()
            {
                SiteID = SessionSiteID,
                NavigationKey = string.IsNullOrEmpty(navigationKey) ? null : navigationKey,
                IsProofing = isProofing,
                PageID = !(string.IsNullOrEmpty(pageId)) ? (int.TryParse(pageId, out i) ? i : 0) : (int?)null
            };

            if (!isValidVersion)
            {
                return Json(new { total = 0, data = Enumerable.Empty<PageNavigationObjectModel>() });
            }
            else
            {           
                KendoGridPost kendoGridPost = new KendoGridPost();
                int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

                PageNavigationSortColumn SortColumn = PageNavigationSortColumn.NavigationKey;
                switch (kendoGridPost.SortColumn)
                {
                    case "PageName":
                        SortColumn = PageNavigationSortColumn.PageName;
                        break;
                    case "PageDescription":
                        SortColumn = PageNavigationSortColumn.PageDescription;
                        break;
                    case "NavigationKey":
                        SortColumn = PageNavigationSortColumn.NavigationKey;
                        break;
                    case "Version":
                        SortColumn = PageNavigationSortColumn.IsProofing;
                        break;
                }

                SortOrder sortOrder = SortOrder.Ascending;
                if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
                {
                    sortOrder = SortOrder.Descending;
                }

                return Json(new
                {
                    total = pageNavigationCacheFactory.GetEntitiesBySearch(objPageNavigationSearchDetail).Select(p => p.PageNavigationId).Count(),
                    data = (from pageNavigationObjectModel in pageNavigationCacheFactory.GetEntitiesBySearch(
                                startRowIndex,
                                kendoGridPost.PageSize,
                                objPageNavigationSearchDetail,
                                new PageNavigationSortDetail() { Column = SortColumn, Order = sortOrder })
                            select new
                            {
                                PageNavigationId = pageNavigationObjectModel.PageNavigationId,
                                PageName = pageNavigationObjectModel.PageName,
                                VersionID = pageNavigationObjectModel.Version,
                                Version = pageNavigationObjectModel.IsProofing ? Versionproof : Versionprod,
                                NavigationKey = pageNavigationObjectModel.NavigationKey,
                                IsProofing = pageNavigationObjectModel.IsProofing,
                                PageDescription = pageNavigationObjectModel.PageDescription
                            })
                });
            }
        }
        #endregion


        #region Load Search Creteria Methods.

        /// <summary>
        /// Gets the Navigation keys.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetNavigationKey()
        {
            return Json((from dbo in pageNavigationCacheFactory.GetEntitiesBySearch(new PageNavigationSearchDetail { SiteID = SessionSiteID })
                         select new { Display = dbo.NavigationKey, Value = dbo.NavigationKey }).Distinct().OrderBy(NavigationKey => NavigationKey.Display),
                         JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetPageNames
        /// <summary>
        /// Gets the page Names.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetPageNames()
        {
            return Json((from dbo in pageNavigationCacheFactory.GetEntitiesBySearch(new PageNavigationSearchDetail { SiteID = SessionSiteID })
                         select new
                         {
                             Display = dbo.PageDescription,
                             Value = dbo.PageId.ToString()
                         }).Distinct().OrderBy(PageId => PageId.Display),
                         JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetVersions

        /// <summary>
        /// Gets the page names.
        /// </summary>
        /// <returns>JsonResult.</returns>
        public JsonResult GetVersions()
        {
            return Json((from dbo in pageNavigationCacheFactory.GetEntitiesBySearch(new PageNavigationSearchDetail { SiteID = SessionSiteID })
                         select new
                         {
                             Display = dbo.IsProofing ? Versionproof : Versionprod,
                             Value = dbo.IsProofing ? Versionproof : Versionprod
                         }).Distinct().OrderBy(IsProofing => IsProofing.Display),
                         JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ValidateXML
        /// <summary>
        /// ValidateXml
        /// </summary>
        /// <param name="navXml">The nav XML.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet, ValidateInput(false)]
        public JsonResult ValidateXml(string navXml)
        {
            List<string> ErrorList = new List<string>();
            if (!string.IsNullOrWhiteSpace(navXml))
            {
                try
                {
                    XmlSchemaSet schema = new XmlSchemaSet();
                    schema.Add(null, Path.Combine(Server.MapPath("~/XmlSchema/VerticalImport"), "menuItem.xsd"));

                    XDocument xmlDoc = XDocument.Load(new StringReader(navXml));
                    var namespaceName = xmlDoc.Root.GetDefaultNamespace().NamespaceName;

                    if (string.IsNullOrWhiteSpace(namespaceName) || !namespaceName.Equals("http://rightprospectus.com/hostedSchema"))
                    {
                        ErrorList.Add("Xml doesn't contain 'http://rightprospectus.com/hostedSchema' namespace");
                    }

                    xmlDoc.Validate(schema, (o, e) =>
                    {
                        if (e.Severity != XmlSeverityType.Warning)
                        {
                            if (!ErrorList.Contains(e.Message))
                            {
                                ErrorList.Add(e.Message);
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    ErrorList.Add(ex.Message);
                }
            }

            return Json(ErrorList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Disable Page Navigation
        /// <summary>
        /// Disables the page Navigation.
        /// </summary>
        /// <param name="pageNavigationID">The page Navigation identifier.</param>
        /// <param name="versionID">The version identifier.</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DisablePageNavigation(int pageNavigationID, int versionID, bool isProofing)
        {
            pageNavigationCacheFactory.DeleteEntity(new PageNavigationObjectModel { PageNavigationId = pageNavigationID, Version = versionID, IsProofing = isProofing }, SessionUserID);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region ActionMethod For Edit
        /// <summary>
        /// Edits the page navigation.
        /// </summary>
        /// <param name="PageNavigationID">The page navigation identifier.</param>
        /// <param name="VersionID">The version identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult EditPageNavigation(int PageNavigationID, int VersionID)
        {
            EditPageNavigationViewModel viewModel = new EditPageNavigationViewModel();
            viewModel.PageDescriptions = new List<DisplayValuePair>();

            viewModel.PageDescriptions.Add(new DisplayValuePair { Value = dropvalue, Display = dropPageText });
            
            //Filter the template pages for the template associated with the selected site.
            int templateId = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId;
            foreach (var item in (from dbo in templatePageCacheFactory.GetEntitiesBySearch(new TemplatePageSearchDetail { TemplateID = templateId })
                                  select new { dbo.PageDescription, dbo.PageID }).Distinct())
            {
                viewModel.PageDescriptions.Add(new DisplayValuePair { Display = item.PageDescription, Value = item.PageID.ToString() });
            }

            if (PageNavigationID > 0)
            {
                viewModel.NavigationKeys = new List<DisplayValuePair>();

                viewModel.NavigationKeys.Add(new DisplayValuePair { Value = dropvalue, Display = dropPageNavigationText });
                foreach (var item in (from dbo in templatePageNavigationCacheFactory.GetAllEntities()
                                      select dbo.NavigationKey).Distinct())
                {
                    viewModel.NavigationKeys.Add(new DisplayValuePair { Display = item, Value = item });
                }
                PageNavigationObjectModel pageNavigationObjectModel = pageNavigationCacheFactory.GetEntityByKey(new PageNavigationKey(PageNavigationID, VersionID));

                viewModel.SelectedNavigationKey = pageNavigationObjectModel.NavigationKey;
                viewModel.SelectedPageID = pageNavigationObjectModel.PageId;
                viewModel.Text = pageNavigationObjectModel.Text;
                viewModel.PageNavigationID = pageNavigationObjectModel.PageNavigationId;
                viewModel.VersionID = pageNavigationObjectModel.Version;
                viewModel.IsProofing = pageNavigationObjectModel.IsProofing;
                viewModel.IsProofingAvailableForPageNavigationId = pageNavigationObjectModel.IsProofingAvailableForPageNavigationID;
                if (pageNavigationObjectModel.ModifiedBy != 0)
                {
                    viewModel.ModifiedByName = pageNavigationObjectModel.ModifiedBy.ToString();
                    if (userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = pageNavigationObjectModel.ModifiedBy }).Count() > 0)
                    {
                        viewModel.ModifiedByName = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = pageNavigationObjectModel.ModifiedBy }).FirstOrDefault().UserName;
                    }
                }
                else
                {
                    viewModel.ModifiedByName = string.Empty;
                }
                viewModel.UTCLastModifiedDate = pageNavigationObjectModel.LastModified;
                viewModel.NavigationXML = pageNavigationObjectModel.NavigationXML;
            }
            else
            {
                viewModel.NavigationKeys = new List<DisplayValuePair>();
                viewModel.NavigationKeys.Add(new DisplayValuePair { Value = dropvalue, Display = dropPageNavigationText });


            }
            ViewData["Success"] = "In Progress";
            return View(viewModel);
        }


        /// <summary>
        /// Edit PageNavigation POST action method
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>Result containing PageNavigation view model</returns>
        [HttpPost, ValidateInput(false)]
        [IsPopUp]
        public ActionResult EditPageNavigation(EditPageNavigationViewModel viewModel)
        {
            try
            {
                PageNavigationObjectModel pageNavigationObjectModel = new PageNavigationObjectModel()
                {
                    PageNavigationId = viewModel.PageNavigationID,
                    Version = viewModel.VersionID,
                    NavigationKey = viewModel.SelectedNavigationKey,
                    NavigationXML = viewModel.NavigationXML,
                    PageId = viewModel.SelectedPageID,
                    IsProofing = viewModel.IsProofing,
                    SiteId = SessionSiteID
                };
                pageNavigationCacheFactory.SaveEntity(pageNavigationObjectModel, SessionUserID);

                viewModel.PageDescriptions = new List<DisplayValuePair>();
                viewModel.PageDescriptions.Add(new DisplayValuePair { Value = dropvalue, Display = dropPageText });               
            
                foreach (var item in (from dbo in templatePageCacheFactory.GetAllEntities()
                                      select new { dbo.PageDescription, dbo.PageID }).Distinct())
                {
                    viewModel.PageDescriptions.Add(new DisplayValuePair { Display = item.PageDescription, Value = item.PageID.ToString() });
                }

                viewModel.NavigationKeys = new List<DisplayValuePair>();

                IEnumerable<TemplatePageNavigationObjectModel> templatePageNavigationDetails = templatePageNavigationCacheFactory.GetEntitiesBySearch(
                    new TemplatePageNavigationSearchDetail
                    {
                        TemplateID = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId,
                        NavigationKey = viewModel.SelectedNavigationKey
                    });
                
                viewModel.NavigationKeys.Add(new DisplayValuePair { Value = dropvalue, Display = dropPageNavigationText });
                foreach (var item in (from dbo in templatePageNavigationDetails
                                            select dbo.NavigationKey).Distinct())
                {
                    viewModel.NavigationKeys.Add(new DisplayValuePair { Display = item, Value = item });
                }


                ViewData["Success"] = "OK";
            }
            catch (Exception exception)
            {
                viewModel.SuccessOrFailedMessage = exception.Message;
            }
            return View(viewModel);
        }

        #endregion


        #region LoadNavigationKeys
        /// <summary>
        /// Loads the Navigation Keys.
        /// </summary>
        /// <param name="pageID">The page identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult LoadNavigationKeys(int pageID)
        {
            List<DisplayValuePair> lstNavigationKeys = new List<DisplayValuePair>();
            lstNavigationKeys.Add(new DisplayValuePair { Value = dropvalue, Display = dropPageNavigationText });

            IEnumerable<string> pageNavigationKeys = pageNavigationCacheFactory.GetEntitiesBySearch(new PageNavigationSearchDetail { SiteID = SessionSiteID, PageID = pageID }).Select(x => x.NavigationKey);
            (from dbo in templatePageNavigationCacheFactory.GetEntitiesBySearch(
                 new TemplatePageNavigationSearchDetail
                 {
                     TemplateID = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId,
                     PageId = pageID
                 })
             select dbo.NavigationKey).Where(p => !pageNavigationKeys.Contains(p)).ToList().ForEach(
                item => lstNavigationKeys.Add(new DisplayValuePair { Display = item, Value = item })
             );

            return Json(lstNavigationKeys, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region LoadDefaultTextForNavigationKey
        /// <summary>
        /// Loads the default text for navigation key.
        /// </summary>
        /// <param name="pageID">The page identifier.</param>
        /// <param name="navigationkey">The navigationkey.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult LoadDefaultTextForNavigationKey(int pageID, string navigationkey)
        {
            return Json(
                templatePageNavigationCacheFactory.GetEntitiesBySearch(new TemplatePageNavigationSearchDetail
                {
                    TemplateID = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId,
                    PageId = pageID,
                    NavigationKey = navigationkey
                }).Select(p => p.DefaultNavigationXml).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
