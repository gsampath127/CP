// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-20-2015
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
using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Keys;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.HostedAdmin.Common;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;


/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// Class PageFeatureController.
    /// </summary>
    public class PageFeatureController : BaseController
    {
        #region Constants
        ///<summary>
        ///Constants
        ///</summary>
        private const string DefaultFeatureKey = "--Please select Page Feature Key--";
        private const string DefaultPage = "--Please select Page--";
        private const string defaultvalue = "-1";
        #endregion

        /// <summary>
        /// The page feature cache factory
        /// </summary>
        private IFactoryCache<PageFeatureFactory, PageFeatureObjectModel, PageFeatureKey> pageFeatureCacheFactory;
        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;

        /// <summary>
        /// The template page cache factory
        /// </summary>
        private IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey> templatePageCacheFactory;

        /// <summary>
        /// The page feature cache factory
        /// </summary>
        private IFactoryCache<TemplatePageFeatureFactory, TemplatePageFeatureObjectModel, TemplatePageFeatureKey> templatePageFeatureCacheFactory;

        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PageFeatureController"/> class.
        /// </summary>
        public PageFeatureController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                this.pageFeatureCacheFactory = RPV2Resolver.Resolve<IFactoryCache<PageFeatureFactory, PageFeatureObjectModel, PageFeatureKey>>("PageFeature");
                this.pageFeatureCacheFactory.ClientName = SessionClientName;
                this.pageFeatureCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;

                templatePageCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>>("TemplatePage");
                templatePageCacheFactory.Mode = FactoryCacheMode.All;

                templatePageFeatureCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplatePageFeatureFactory, TemplatePageFeatureObjectModel, TemplatePageFeatureKey>>("TemplatePageFeature");
                templatePageFeatureCacheFactory.Mode = FactoryCacheMode.All;

                siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
                siteCacheFactory.ClientName = SessionClientName;
                siteCacheFactory.Mode = FactoryCacheMode.All;
            }
        }
        #endregion

        #region Constructor_Test

        /// <summary>
        /// Initializes a new instance of the <see cref="PageFeatureController"/> class.
        /// </summary>
        /// <param name="PageFeatureFactoryCache">The page feature factory cache.</param>
        /// <param name="UserFactoryCache">The user factory cache.</param>
        /// <param name="TemplatePageFactoryCache">The template page factory cache.</param>
        /// <param name="TemplatePageFeatureFactoryCache">The template page feature factory cache.</param>
        /// <param name="SiteCacheFactory">The site cache factory.</param>
        public PageFeatureController(IFactoryCache<PageFeatureFactory, PageFeatureObjectModel, PageFeatureKey> PageFeatureFactoryCache,
           IFactoryCache<UserFactory, UserObjectModel, int> UserFactoryCache,
            IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey> TemplatePageFactoryCache,
            IFactoryCache<TemplatePageFeatureFactory, TemplatePageFeatureObjectModel, TemplatePageFeatureKey> TemplatePageFeatureFactoryCache,
            IFactoryCache<SiteFactory, SiteObjectModel, int> SiteCacheFactory)
        {
            this.pageFeatureCacheFactory = PageFeatureFactoryCache;
            this.pageFeatureCacheFactory.ClientName = SessionClientName;
            this.pageFeatureCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = UserFactoryCache;
            userCacheFactory.Mode = FactoryCacheMode.All;

            templatePageCacheFactory = TemplatePageFactoryCache;
            templatePageCacheFactory.Mode = FactoryCacheMode.All;

            templatePageFeatureCacheFactory = TemplatePageFeatureFactoryCache;
            templatePageFeatureCacheFactory.Mode = FactoryCacheMode.All;

            siteCacheFactory = SiteCacheFactory;
            siteCacheFactory.ClientName = SessionClientName;
            siteCacheFactory.Mode = FactoryCacheMode.All;
        }
        #endregion

        #region View
        /// <summary>
        /// Pages the feature.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult List()
        {
            ViewData["SelectedSite"] = SessionSiteName;
            ViewData["SelectedCustomer"] = SessionClientName;
            return View();
        }
        #endregion

        #region GetPageFeatureKey
        /// <summary>
        /// Gets the Page feature key
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetPageFeatureKey()
        {
            return Json((from dbo in pageFeatureCacheFactory.GetEntitiesBySearch(new PageFeatureSearchDetail { SiteId = SessionSiteID })
                         select new { Display = dbo.PageKey }).OrderBy(PageKey => PageKey.Display).Distinct(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetPageNames
        /// <summary>
        /// Gets the page names.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetPageNames()
        {
            return Json((from dbo in pageFeatureCacheFactory.GetEntitiesBySearch(new PageFeatureSearchDetail { SiteId = SessionSiteID })
                         select new { Display = dbo.PageDescription, Value = dbo.PageId }).OrderBy(PageDescription => PageDescription.Display).Distinct(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region LoadAllPageFeature
        /// <summary>
        /// Load All Page Feature details.
        /// </summary>
        /// <param name="Key">The Page key.</param>
        /// <param name="PageID">The page identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult LoadAllPageFeature(string Key, string PageID)
        {
            int id;
            PageFeatureSearchDetail objSearchDetail = new PageFeatureSearchDetail()
            {
                PageId = !(string.IsNullOrEmpty(PageID)) ? (int.TryParse(PageID, out id) ? id : 0) : (int?)null,
                PageKey = string.IsNullOrEmpty(Key) ? null : Key,
                SiteId = SessionSiteID
            };

            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            PageFeatureSortColumn SortColumn = PageFeatureSortColumn.PageKey;
            switch (kendoGridPost.SortColumn)
            {
                case "PageName":
                    SortColumn = PageFeatureSortColumn.PageName;
                    break;
                case "PageDescription":
                    SortColumn = PageFeatureSortColumn.PageDescription;
                    break;
                case "Key":
                    SortColumn = PageFeatureSortColumn.PageKey;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            return Json(new
            {
                total = pageFeatureCacheFactory.GetEntitiesBySearch(objSearchDetail).Count(),
                data = (from pageFeatureObjectModel in pageFeatureCacheFactory.GetEntitiesBySearch(
                            startRowIndex,
                            kendoGridPost.PageSize,
                            objSearchDetail,
                            new PageFeatureSortDetail() { Column = SortColumn, Order = sortOrder })
                        select new
                        {
                            PageKey = pageFeatureObjectModel.PageKey.ToString(),
                            PageName = pageFeatureObjectModel.PageName,
                            PageDescription = pageFeatureObjectModel.PageDescription,
                            PageId = pageFeatureObjectModel.PageId
                        }
                    )
            });
        }
        #endregion

        #region  GetPageFeatureModes
        /// <summary>
        /// Gets the page feature modes.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <returns>JsonResult.</returns>
        public JsonResult GetPageFeatureModes(string pageKey)
        {
            List<MultiSelectDropDownViewModel> lstFeatureModesVM = new List<MultiSelectDropDownViewModel>();

            if (pageKey == "XBRL")
            {
                FeatureEnums.XBRL[] enums = (FeatureEnums.XBRL[])Enum.GetValues(typeof(FeatureEnums.XBRL));

                for (int i = 0; i < enums.Length; i++)
                {
                    MultiSelectDropDownViewModel vmMultiSelect = new MultiSelectDropDownViewModel()
                    {
                        label = enums[i].ToString(),
                        value = Math.Pow(2, i).ToString(),
                        title = enums[i].ToString()
                    };
                    lstFeatureModesVM.Add(vmMultiSelect);
                }
            }
            else if (pageKey == "RequestMaterial")
            {
                FeatureEnums.RequestMaterial[] enums = (FeatureEnums.RequestMaterial[])Enum.GetValues(typeof(FeatureEnums.RequestMaterial));

                for (int i = 0; i < enums.Length; i++)
                {
                    MultiSelectDropDownViewModel vmMultiSelect = new MultiSelectDropDownViewModel()
                    {
                        label = enums[i].ToString(),
                        value = Math.Pow(2, i).ToString(),
                        title = enums[i].ToString()
                    };
                    lstFeatureModesVM.Add(vmMultiSelect);
                }
            }

            else if (pageKey == "SinglePdfView")
            {
                FeatureEnums.SinglePdfView[] enums = (FeatureEnums.SinglePdfView[])Enum.GetValues(typeof(FeatureEnums.SinglePdfView));

                for (int i = 0; i < enums.Length; i++)
                {
                    MultiSelectDropDownViewModel vmMultiSelect = new MultiSelectDropDownViewModel()
                    {
                        label = enums[i].ToString(),
                        value = Math.Pow(2, i).ToString(),
                        title = enums[i].ToString()
                    };
                    lstFeatureModesVM.Add(vmMultiSelect);
                }
            }

            return Json(lstFeatureModesVM, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetPageFeatureModesByKey
        /// <summary>
        /// Gets the page feature modes by key.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>JsonResult.</returns>
        public JsonResult GetPageFeatureModesByKey(string pageKey, string pageId)
        {         
            int featureMode = pageFeatureCacheFactory.GetEntitiesBySearch(new PageFeatureSearchDetail() { SiteId = SessionSiteID, PageKey = pageKey, PageId = Convert.ToInt32(pageId) }).SingleOrDefault().FeatureMode;

            List<MultiSelectDropDownViewModel> vmMultiSelect = new List<MultiSelectDropDownViewModel>();
            if (pageKey == "XBRL")
            {
                vmMultiSelect = GetFeatureModesXBRL(featureMode);
            }
            else if (pageKey == "RequestMaterial")
            {
                vmMultiSelect = GetFeatureModesRequestMaterial(featureMode);
            }
            else if (pageKey == "SinglePdfView")
            {
                vmMultiSelect = GetFeatureModesSinglePdfView(featureMode);
            }
            return Json(vmMultiSelect, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region GetFeatureModesXBRL

        /// <summary>
        /// Gets the feature modes XBRL.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;MultiSelectDropDownViewModel&gt;.</returns>
        public List<MultiSelectDropDownViewModel> GetFeatureModesXBRL(int mode)
        {
            FeatureEnums.XBRL featureMode = (FeatureEnums.XBRL)mode;
            List<MultiSelectDropDownViewModel> featureModes = new List<MultiSelectDropDownViewModel>();

            if (featureMode.HasFlag(FeatureEnums.XBRL.Disabled))
            {
                featureModes.Add(new MultiSelectDropDownViewModel { value = "1" }); ;
            }
            if (featureMode.HasFlag(FeatureEnums.XBRL.Enabled))
            {
                featureModes.Add(new MultiSelectDropDownViewModel { value = "2" }); ;
            }
            if (featureMode.HasFlag(FeatureEnums.XBRL.ShowXBRLInNewTab))
            {
                featureModes.Add(new MultiSelectDropDownViewModel { value = "4" }); ;
            }
            if (featureMode.HasFlag(FeatureEnums.XBRL.ShowXBRLInTabbedView))
            {
                featureModes.Add(new MultiSelectDropDownViewModel { value = "8" }); ;
            }
            if (featureMode.HasFlag(FeatureEnums.XBRL.ShowXBRLInLandingPage))
            {
                featureModes.Add(new MultiSelectDropDownViewModel { value = "16" });
            }

            return featureModes;
        }

        /// <summary>
        /// Gets the feature modes request material.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;MultiSelectDropDownViewModel&gt;.</returns>
        public List<MultiSelectDropDownViewModel> GetFeatureModesRequestMaterial(int mode)
        {
            FeatureEnums.RequestMaterial featureMode = (FeatureEnums.RequestMaterial)mode;
            List<MultiSelectDropDownViewModel> featureModes = new List<MultiSelectDropDownViewModel>();

            if (featureMode.HasFlag(FeatureEnums.RequestMaterial.Disabled))
            {

                featureModes.Add(new MultiSelectDropDownViewModel { value = "1" }); ;
            }
            if (featureMode.HasFlag(FeatureEnums.RequestMaterial.Enabled))
            {

                featureModes.Add(new MultiSelectDropDownViewModel { value = "2" }); ;
            }
            return featureModes;
        }

        public List<MultiSelectDropDownViewModel> GetFeatureModesSinglePdfView(int mode)
        {
            FeatureEnums.SinglePdfView featureMode = (FeatureEnums.SinglePdfView)mode;
            List<MultiSelectDropDownViewModel> featureModes = new List<MultiSelectDropDownViewModel>();

            if (featureMode.HasFlag(FeatureEnums.SinglePdfView.Disabled))
            {

                featureModes.Add(new MultiSelectDropDownViewModel { value = "1" }); ;
            }
            if (featureMode.HasFlag(FeatureEnums.SinglePdfView.Enabled))
            {

                featureModes.Add(new MultiSelectDropDownViewModel { value = "2" }); ;
            }
            if (featureMode.HasFlag(FeatureEnums.SinglePdfView.ShowClientLogoFrame))
            {

                featureModes.Add(new MultiSelectDropDownViewModel { value = "4" }); ;
            }
            return featureModes;
        }
        #endregion

        #region ActionMethod For Edit
        /// <summary>
        /// Edits the page feature.
        /// </summary>
        /// <param name="PageID">The page text identifier.</param>
        /// <param name="pagekey">PageKey</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult EditPageFeature(int PageID, string pagekey)
        {
            EditPageFeatureViewModel viewModelPageFeature = new EditPageFeatureViewModel();
            viewModelPageFeature.PageKeys = new List<DisplayValuePair>();
            viewModelPageFeature.PageNames = new List<DisplayValuePair>();
            viewModelPageFeature.PageKeys.Add(new DisplayValuePair { Value = defaultvalue, Display = DefaultFeatureKey });
            viewModelPageFeature.PageNames.Add(new DisplayValuePair { Value = defaultvalue, Display = DefaultPage });

            //Get the templateid for the selected site.
            int templateId = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId;
            
            // Get only the pages for the template associated with the selected site.
            foreach (var item in templatePageCacheFactory.GetEntitiesBySearch(new TemplatePageSearchDetail { TemplateID = templateId })
                .Select(x => new { x.PageID, x.PageDescription }))
            {
                viewModelPageFeature.PageNames.Add(new DisplayValuePair { Display = item.PageDescription, Value = item.PageID.ToString() });
            }
            
            // Conditional check for edit
            if (PageID != 0)
            {   
                foreach (var item in pageFeatureCacheFactory.GetAllEntities().Select(x => x.Key))
                {
                    viewModelPageFeature.PageKeys.Add(new DisplayValuePair { Display = item.PageKey, Value = item.PageKey });
                }

                PageFeatureObjectModel pageFeatureObjectModel = pageFeatureCacheFactory.GetEntityByKey(new PageFeatureKey(SessionSiteID, PageID, pagekey));

                viewModelPageFeature.SelectedPageKey = pageFeatureObjectModel.PageKey;
                viewModelPageFeature.SelectedPageId = pageFeatureObjectModel.PageId;
                viewModelPageFeature.UTCLastModifiedDate = pageFeatureObjectModel.LastModified;
                if (pageFeatureObjectModel.ModifiedBy != 0)
                {
                    viewModelPageFeature.ModifiedByName = pageFeatureObjectModel.ModifiedBy.ToString();
                    if (userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = pageFeatureObjectModel.ModifiedBy }).Count() > 0)
                    {
                        viewModelPageFeature.ModifiedByName = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = pageFeatureObjectModel.ModifiedBy }).FirstOrDefault().UserName;
                    }
                }
                else
                {
                    viewModelPageFeature.ModifiedByName = string.Empty;
                }
            }
            else
            {
                viewModelPageFeature.SelectedPageKey = defaultvalue;
                viewModelPageFeature.SelectedPageId = -1;
            }

            viewModelPageFeature.PageId = PageID;
            ViewData["Success"] = "In Progress";
            return View(viewModelPageFeature);
        }
        #endregion

        #region  GetPageKeyByPageId
        /// <summary>
        /// Gets the page key by page identifier.
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetPageKeyByPageId(string pageId)
        {    
            IEnumerable<string> templatePageKeys = (from dbo in templatePageFeatureCacheFactory.GetEntitiesBySearch(
                                                        new TemplatePageFeatureSearchDetail
                                                        {
                                                            TemplateId = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId,
                                                            PageId = Convert.ToInt32(pageId)
                                                        })
                                                    select dbo.FeatureKey);

            var pageKeyCollection = pageFeatureCacheFactory.GetEntitiesBySearch(new PageFeatureSearchDetail { SiteId = SessionSiteID, PageId = Convert.ToInt32(pageId) }).Select(x => x.PageKey);
            templatePageKeys = templatePageKeys.Where(p => !pageKeyCollection.Contains(p));

            return Json((from item in templatePageKeys
                         select new { Display = item, Value = item }), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region  EditPageFeature
        /// <summary>
        /// Edits the page feature.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="selectedFeatureModes">The selected feature modes.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult EditPageFeature(EditPageFeatureViewModel viewModel, string selectedFeatureModes)
        {
            try
            {
                viewModel.PageKeys = new List<DisplayValuePair>();
                viewModel.PageNames = new List<DisplayValuePair>();

                PageFeatureObjectModel objPageFeatureObjectModel = new PageFeatureObjectModel()
                {
                    SiteId = SessionSiteID,
                    PageId = viewModel.SelectedPageId,
                    PageKey = viewModel.SelectedPageKey,
                    FeatureMode = CalculateFeatureMode(selectedFeatureModes),
                };
                pageFeatureCacheFactory.SaveEntity(objPageFeatureObjectModel, SessionUserID);

                viewModel.PageKeys.Add(new DisplayValuePair { Value = defaultvalue, Display = DefaultFeatureKey });
                foreach (var item in pageFeatureCacheFactory.GetAllEntities().Select(x => x.Key))
                {
                    viewModel.PageKeys.Add(new DisplayValuePair { Display = item.PageKey, Value = item.PageKey });
                }

                viewModel.PageNames.Add(new DisplayValuePair { Value = defaultvalue, Display = DefaultPage });
                foreach (var item in templatePageCacheFactory.GetAllEntities().Select(x => new { x.PageID, x.PageDescription }))
                {
                    viewModel.PageNames.Add(new DisplayValuePair { Display = item.PageDescription, Value = item.PageID.ToString() });
                }
            }
            catch
            {

            }

            ViewData["Success"] = "OK";
            return View(viewModel);
        }
        #endregion

        #region CalculateFeatureMode
        /// <summary>
        /// Calculates the feature mode.
        /// </summary>
        /// <param name="selectedFeatureModes">The selected feature modes.</param>
        /// <returns>System.Int32.</returns>
        public int CalculateFeatureMode(string selectedFeatureModes)
        {
            string[] featureMode = selectedFeatureModes.Split(',');
            int mode = 0;

            mode = Convert.ToInt32(featureMode[0]);
            if (featureMode.Length > 1)
                for (int i = 1; i < featureMode.Length; i++)
                {
                    mode = mode | Convert.ToInt32(featureMode[i]);
                }
            return mode;
        }
        #endregion


        /// <summary>
        /// Deletes the static resource.
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <param name="pageKey">The page key.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DeletePageFeature(int pageId, string pageKey)
        {            
            pageFeatureCacheFactory.DeleteEntity(new PageFeatureObjectModel { PageId = pageId, PageKey = pageKey, SiteId = SessionSiteID }, SessionUserID);
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
    }
}