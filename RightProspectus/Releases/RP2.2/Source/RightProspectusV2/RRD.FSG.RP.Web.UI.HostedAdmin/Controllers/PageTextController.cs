// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-16-2015
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
using System.Text;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// To Hold the Page Text related actions
    /// </summary>
    public class PageTextController : BaseController
    {
        #region Constants
        ///<summary>
        ///Constant values
        ///</summary>
        private const string defalutvalue = "-1";
        private const string defaultPage = "--Please select Page --";
        private const string defaultResource = "--Please select Resource Key--";
        #endregion
        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        /// <summary>
        /// The page text cache factory
        /// </summary>
        private IFactoryCache<PageTextFactory, PageTextObjectModel, PageTextKey> pageTextCacheFactory;
        /// <summary>
        /// The template page cache factory
        /// </summary>
        private IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey> templatePageCacheFactory;
        /// <summary>
        /// The template page text cache factory
        /// </summary>
        private IFactoryCache<TemplatePageTextFactory, TemplatePageTextObjectModel, TemplatePageTextKey> templatePageTextCacheFactory;
        
        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageTextController" /> class.
        /// </summary>
        public PageTextController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                pageTextCacheFactory = RPV2Resolver.Resolve<IFactoryCache<PageTextFactory, PageTextObjectModel, PageTextKey>>("PageText");
                pageTextCacheFactory.ClientName = SessionClientName;
                pageTextCacheFactory.Mode = FactoryCacheMode.All;

                templatePageCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>>("TemplatePage");
                templatePageCacheFactory.Mode = FactoryCacheMode.All;

                templatePageTextCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplatePageTextFactory, TemplatePageTextObjectModel, TemplatePageTextKey>>("TemplatePageText");
                templatePageTextCacheFactory.Mode = FactoryCacheMode.All;

                siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
                siteCacheFactory.ClientName = SessionClientName;
                siteCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageTextController"/> class.
        /// </summary>
        /// <param name="PageTextFactoryCache">The page text factory cache.</param>
        /// <param name="TemplatePageFactoryCache">The template page factory cache.</param>
        /// <param name="TemplatePageTextFactoryCache">The template page text factory cache.</param>
        /// <param name="SiteFactoryCache">The site factory cache.</param>
        /// <param name="UserFactoryCache">The user factory cache.</param>
        public PageTextController(IFactoryCache<PageTextFactory, PageTextObjectModel, PageTextKey> PageTextFactoryCache,
           IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey> TemplatePageFactoryCache,
            IFactoryCache<TemplatePageTextFactory, TemplatePageTextObjectModel, TemplatePageTextKey> TemplatePageTextFactoryCache,
            IFactoryCache<SiteFactory, SiteObjectModel, int> SiteFactoryCache,
            IFactoryCache<UserFactory, UserObjectModel, int> UserFactoryCache)
        {
            pageTextCacheFactory = PageTextFactoryCache;
            pageTextCacheFactory.ClientName = SessionClientName;
            pageTextCacheFactory.Mode = FactoryCacheMode.All;

            templatePageCacheFactory = TemplatePageFactoryCache;
            templatePageCacheFactory.Mode = FactoryCacheMode.All;

            templatePageTextCacheFactory = TemplatePageTextFactoryCache;
            templatePageTextCacheFactory.Mode = FactoryCacheMode.All;

            siteCacheFactory = SiteFactoryCache;
            siteCacheFactory.ClientName = SessionClientName;
            siteCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = UserFactoryCache;
            userCacheFactory.Mode = FactoryCacheMode.All;
        }

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult List()
        {
            ViewData["SelectedSite"] = SessionSiteName;
            ViewData["SelectedCustomer"] = SessionClientName;

            return View("PageText");
        }

        #region GetAllPageTextDetails
        /// <summary>
        /// Gets all page text details.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="pageID">The page identifier.</param>
        /// <param name="version">The version.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllPageTextDetails(string resourceKey, string pageID, string version)
        {
            if (string.IsNullOrWhiteSpace(resourceKey))
            {
                resourceKey = null;
            }

            bool? isProofing = null;
            bool isValidVersion = true;
            if (!string.IsNullOrEmpty(version))
            {
                if (version == "Production")
                {
                    isProofing = false;
                }
                else if (version == "Proofing")
                {
                    isProofing = true;
                }
                else
                {
                    isValidVersion = false;
                }
            }

            int id;
            PageTextSearchDetail objPageTextSearchDetail = new PageTextSearchDetail()
            {
                SiteID = SessionSiteID,
                ResourceKey = string.IsNullOrEmpty(resourceKey) ? null : resourceKey,
                IsProofing = isProofing,
                PageID = !(string.IsNullOrEmpty(pageID)) ? (int.TryParse(pageID, out id) ? id : 0) : (int?)null
            };
           
            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            PageTextSortColumn SortColumn = PageTextSortColumn.ResourceKey;
            switch (kendoGridPost.SortColumn)
            {
                case "PageName":
                    SortColumn = PageTextSortColumn.PageName;
                    break;
                case "PageDescription":
                    SortColumn = PageTextSortColumn.PageDescription;
                    break;
                case "Version":
                    SortColumn = PageTextSortColumn.IsProofing;
                    break;
                case "Text":
                    SortColumn = PageTextSortColumn.Text;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            if (!isValidVersion)
            {
                return Json(new { total = 0, data = Enumerable.Empty<PageTextViewModel>() });
            }
            else
            {
                return Json(new
                {
                    total = pageTextCacheFactory.GetEntitiesBySearch(objPageTextSearchDetail).Select(p => p.PageTextID).Count(),
                    data = (from pageTextObjectModel in pageTextCacheFactory.GetEntitiesBySearch(
                                startRowIndex,
                                kendoGridPost.PageSize,
                                objPageTextSearchDetail,
                                new PageTextSortDetail() { Column = SortColumn, Order = sortOrder })
                            select new
                            {
                                PageTextID = pageTextObjectModel.PageTextID,
                                PageName = pageTextObjectModel.PageName,
                                VersionID = pageTextObjectModel.Version,
                                Version = pageTextObjectModel.IsProofing ? "Proofing" : "Production",
                                ResourceKey = pageTextObjectModel.ResourceKey,
                                Text = pageTextObjectModel.Text,
                                PageDescription = pageTextObjectModel.PageDescription,
                                IsProofing = pageTextObjectModel.IsProofing
                            })
                });
            }
        }
        #endregion

        #region Load Search Creteria Methods.

        /// <summary>
        /// Gets the resource keys.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetResourceKeys()
        {
            return Json((from dbo in pageTextCacheFactory.GetEntitiesBySearch(new PageTextSearchDetail() { SiteID = SessionSiteID }).Distinct().OrderBy(ResourceKey => ResourceKey)
                         select new { Display = dbo.ResourceKey, Value = dbo.ResourceKey }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the page names.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetPageNames()
        {
            return Json((from key in
                             (from dbo in pageTextCacheFactory.GetEntitiesBySearch(new PageTextSearchDetail() { SiteID = SessionSiteID })
                              select new { PageId = dbo.PageID, PageDescription = dbo.PageDescription }).Distinct()
                         select new
                         {
                             Display = key.PageDescription,
                             Value = key.PageId
                         }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the versions.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetVersions()
        {
            return Json((from key in
                             (from dbo in pageTextCacheFactory.GetEntitiesBySearch(new PageTextSearchDetail() { SiteID = SessionSiteID })
                              select dbo.IsProofing).Distinct().OrderBy(IsProofing => IsProofing)
                         select new { Display = key ? "Proofing" : "Production", Value = key ? "Proofing" : "Production" }
                         ), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Disable Page text
        /// <summary>
        /// Disables the page text.
        /// </summary>
        /// <param name="pageTextID">The page text identifier.</param>
        /// <param name="versionID">The version identifier.</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DisablePageText(int pageTextID, int versionID, bool isProofing)
        {
            pageTextCacheFactory.DeleteEntity(new PageTextObjectModel { PageTextID = pageTextID, Version = versionID, IsProofing = isProofing }, SessionUserID);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ActionMethod For Edit

        /// <summary>
        /// Edits the page text.
        /// </summary>
        /// <param name="PageTextID">The page text identifier.</param>
        /// <param name="VersionID">The version identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult EditPageText(int PageTextID, int VersionID)
        {
            EditPageTextViewModel viewModel = new EditPageTextViewModel();
            viewModel.PageDescriptions = new List<DisplayValuePair>();         

            viewModel.PageDescriptions.Add(new DisplayValuePair { Value = defalutvalue, Display = defaultPage });
            
            int templateId = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId;
            foreach (var item in (from dbo in templatePageCacheFactory.GetEntitiesBySearch(new TemplatePageSearchDetail { TemplateID = templateId })
                                  select new { dbo.PageDescription, dbo.PageID }).Distinct())
            {
                viewModel.PageDescriptions.Add(new DisplayValuePair { Display = item.PageDescription, Value = item.PageID.ToString() });
            }

            if (PageTextID > 0)
            {
                viewModel.ResourceKeys = new List<DisplayValuePair>();

                IEnumerable<TemplatePageTextObjectModel> templatePageTextDetails = templatePageTextCacheFactory.GetAllEntities();
                var ResourceKeyDetails = (from dbo in templatePageTextDetails
                                          select dbo.ResourceKey).Distinct();

                viewModel.ResourceKeys.Add(new DisplayValuePair { Value = defalutvalue, Display = defaultResource });
                foreach (var item in ResourceKeyDetails)
                {
                    viewModel.ResourceKeys.Add(new DisplayValuePair { Display = item, Value = item });
                }

                PageTextObjectModel pageTextObjectModel = pageTextCacheFactory.GetEntityByKey(new PageTextKey(PageTextID, VersionID));

                viewModel.SelectedResourceKey = pageTextObjectModel.ResourceKey;
                viewModel.SelectedPageID = pageTextObjectModel.PageID;               

                if (templatePageTextDetails.FirstOrDefault(p => p.ResourceKey == pageTextObjectModel.ResourceKey && p.PageID == pageTextObjectModel.PageID).IsHTML)
                {
                    viewModel.HtmlText = System.Web.HttpUtility.HtmlDecode(pageTextObjectModel.Text);
                }
                else
                {
                    viewModel.PlainText = pageTextObjectModel.ResourceKey.ToLower().Contains("css") ? CSSMinifyUnminifyHelper.UnMinifyCSSText(pageTextObjectModel.Text) : pageTextObjectModel.Text;
                }
                viewModel.PageTextID = pageTextObjectModel.PageTextID;
                viewModel.VersionID = pageTextObjectModel.Version;
                viewModel.IsProofing = pageTextObjectModel.IsProofing;
                viewModel.IsProofingAvailableForPageTextId = pageTextObjectModel.IsProofingAvailableForPageTextId;
                viewModel.UTCLastModifiedDate = pageTextObjectModel.LastModified;
                if (pageTextObjectModel.ModifiedBy != 0)
                {
                    viewModel.ModifiedByName = pageTextObjectModel.ModifiedBy.ToString();
                    if (userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = pageTextObjectModel.ModifiedBy }).Count() > 0)
                    {
                        viewModel.ModifiedByName = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = pageTextObjectModel.ModifiedBy }).FirstOrDefault().UserName;
                    }
                }
                else
                {
                    viewModel.ModifiedByName = string.Empty;
                }
            }
            else
            {
                viewModel.ResourceKeys = new List<DisplayValuePair>();
                viewModel.ResourceKeys.Add(new DisplayValuePair { Value = defalutvalue, Display = defaultPage });

            }
            ViewData["Success"] = "In Progress";
            return View(viewModel);
        }


        /// <summary>
        /// Edits the page text.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult EditPageText(EditPageTextViewModel viewModel)
        {
            try
            {
                PageTextObjectModel pageTextObjectModel = new PageTextObjectModel()
                {
                    PageTextID = viewModel.PageTextID,
                    Version = viewModel.VersionID,
                    ResourceKey = viewModel.SelectedResourceKey,
                    PageID = viewModel.SelectedPageID,
                    IsProofing = viewModel.IsProofing,
                    SiteID = SessionSiteID
                };

                IEnumerable<TemplatePageTextObjectModel> templatePageTextDetails = templatePageTextCacheFactory.GetAllEntities();
                if (templatePageTextDetails.FirstOrDefault(p => p.ResourceKey == pageTextObjectModel.ResourceKey && p.PageID == pageTextObjectModel.PageID).IsHTML)
                {
                    pageTextObjectModel.Text = viewModel.HtmlText;
                }
                else
                {
                    pageTextObjectModel.Text = pageTextObjectModel.ResourceKey.ToLower().Contains("css") ? CSSMinifyUnminifyHelper.MinifyCSSText(viewModel.PlainText) : viewModel.PlainText;
                }

                pageTextCacheFactory.SaveEntity(pageTextObjectModel, SessionUserID);

                viewModel.PageDescriptions = new List<DisplayValuePair>();
                viewModel.PageDescriptions.Add(new DisplayValuePair { Value = defalutvalue, Display = defaultPage });

                foreach (var item in (from dbo in templatePageCacheFactory.GetAllEntities()
                                      select new { dbo.PageDescription, dbo.PageID }).Distinct())
                {
                    viewModel.PageDescriptions.Add(new DisplayValuePair { Display = item.PageDescription, Value = item.PageID.ToString() });
                }

                viewModel.ResourceKeys = new List<DisplayValuePair>();

                viewModel.ResourceKeys.Add(new DisplayValuePair { Value = defalutvalue, Display = defaultResource });
                foreach (var item in (from dbo in templatePageTextCacheFactory.GetEntitiesBySearch(
                                          new TemplatePageTextSearchDetail
                                          {
                                              TemplateID = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId,
                                              PageID = viewModel.SelectedPageID
                                          })
                                      select dbo.ResourceKey).Distinct())
                {
                    viewModel.ResourceKeys.Add(new DisplayValuePair { Display = item, Value = item });
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

        /// <summary>
        /// Loads the resource keys.
        /// </summary>
        /// <param name="pageID">The page identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult LoadResourceKeys(int pageID)
        {
            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>(); 
            var pagetextResourceKeys = pageTextCacheFactory.GetEntitiesBySearch(new PageTextSearchDetail { SiteID = SessionSiteID, PageID = pageID }).Select(x => x.ResourceKey);
          
            lstResourceKeys.Add(new DisplayValuePair { Value = defalutvalue, Display = defaultResource });
            foreach (var item in (from dbo in templatePageTextCacheFactory.GetEntitiesBySearch(
                                      new TemplatePageTextSearchDetail 
                                      { 
                                          TemplateID = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId, 
                                          PageID = pageID 
                                      })
                                  select dbo.ResourceKey).Where(p => !pagetextResourceKeys.Contains(p)))
            {
                lstResourceKeys.Add(new DisplayValuePair { Display = item, Value = item });
            }
            return Json(lstResourceKeys, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Loads the default text for resource key.
        /// </summary>
        /// <param name="pageID">The page identifier.</param>
        /// <param name="resourcekey">The resourcekey.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult LoadDefaultTextForResourceKey(int pageID, string resourcekey)
        {           
            string defaultTextForResourceKey = templatePageTextCacheFactory.GetEntitiesBySearch(
                new TemplatePageTextSearchDetail 
                { 
                    TemplateID = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId, 
                    PageID = pageID, 
                    ResourceKey = resourcekey 
                }).Select(p => p.DefaultText).FirstOrDefault();           
            return Json(resourcekey.ToLower().Contains("css") ? CSSMinifyUnminifyHelper.UnMinifyCSSText(defaultTextForResourceKey) : System.Web.HttpUtility.HtmlDecode(defaultTextForResourceKey), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Checks whether resource key text is html or plain.
        /// </summary>
        /// <param name="resourceKey">The resourcekey.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckIsHtmlTextForResourceKey(string resourceKey, int pageId)
        {
            return Json(resourceKey == "-1" || pageId == -1 ? 
                true : templatePageTextCacheFactory.GetEntitiesBySearch(new TemplatePageTextSearchDetail { ResourceKey = resourceKey, PageID = pageId }).FirstOrDefault().IsHTML, JsonRequestBehavior.AllowGet);
        }

   

    }
}