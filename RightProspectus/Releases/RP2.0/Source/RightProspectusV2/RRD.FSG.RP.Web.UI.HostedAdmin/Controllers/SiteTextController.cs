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
using System.Linq;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// To Hold the Site Text related actions
    /// </summary>
    public class SiteTextController : BaseController
    {
        #region Constants
        ///<summary>
        ///Constants
        ///</summary>
        private const string defaultvalue = "-1";
        private const string defaultResource = "--Please select Resource Key--";
        private const string DefaultProofing = "Proofing";
        private const string DefaultProduction = "Production";
        #endregion
        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        /// <summary>
        /// The site text cache factory
        /// </summary>
        private IFactoryCache<SiteTextFactory, SiteTextObjectModel, SiteTextKey> siteTextCacheFactory;
        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;
        /// <summary>
        /// The template text cache factory
        /// </summary>
        private IFactoryCache<TemplateTextFactory, TemplateTextObjectModel, TemplateTextKey> templateTextCacheFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteTextController" /> class.
        /// </summary>
        public SiteTextController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                siteTextCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteTextFactory, SiteTextObjectModel, SiteTextKey>>("SiteText");
                siteTextCacheFactory.ClientName = SessionClientName;
                siteTextCacheFactory.Mode = FactoryCacheMode.All;

                siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
                siteCacheFactory.ClientName = SessionClientName;
                siteCacheFactory.Mode = FactoryCacheMode.All;

                templateTextCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplateTextFactory, TemplateTextObjectModel, TemplateTextKey>>("TemplateText");
                templateTextCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteTextController"/> class.
        /// </summary>
        /// <param name="SiteTextFactoryCache">The site text factory cache.</param>
        /// <param name="SiteFactoryCache">The site factory cache.</param>
        /// <param name="TemplateTextFactoryCache">The template text factory cache.</param>
        /// <param name="UserFactoryCache">The user factory cache.</param>
        public SiteTextController(IFactoryCache<SiteTextFactory, SiteTextObjectModel, SiteTextKey> SiteTextFactoryCache,
            IFactoryCache<SiteFactory, SiteObjectModel, int> SiteFactoryCache,
            IFactoryCache<TemplateTextFactory, TemplateTextObjectModel, TemplateTextKey> TemplateTextFactoryCache,
            IFactoryCache<UserFactory, UserObjectModel, int> UserFactoryCache)
        {
            siteTextCacheFactory = SiteTextFactoryCache;
            siteTextCacheFactory.ClientName = SessionClientName;
            siteTextCacheFactory.Mode = FactoryCacheMode.All;

            siteCacheFactory = SiteFactoryCache;
            siteCacheFactory.ClientName = SessionClientName;
            siteCacheFactory.Mode = FactoryCacheMode.All;

            templateTextCacheFactory = TemplateTextFactoryCache;
            templateTextCacheFactory.Mode = FactoryCacheMode.All;

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

            return View();
        }

        /// <summary>
        /// Gets the resource keys.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetResourceKeys()
        {
            return Json((from key in
                             (from dbo in siteTextCacheFactory.GetEntitiesBySearch(new SiteTextSearchDetail() { SiteID = SessionSiteID })
                              select dbo.ResourceKey).Distinct().OrderBy(ResourceKey => ResourceKey)
                         select new { Display = key, Value = key }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the page names.
        /// </summary>
        /// <returns>JsonResult.</returns>
        public JsonResult GetVersions()
        {
            return Json((from dbo in siteTextCacheFactory.GetEntitiesBySearch(new SiteTextSearchDetail() { SiteID = SessionSiteID })
                         select new
                         {
                             Display = dbo.IsProofing ? DefaultProofing : DefaultProduction,
                             Value = dbo.IsProofing ? DefaultProofing : DefaultProduction
                         }).Distinct().OrderBy(obj => obj.Display), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Searches the specified resource key.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="version">The version.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult Search(string resourceKey, string version)
        {
            if (string.IsNullOrWhiteSpace(resourceKey))
                resourceKey = null;

            bool? isProofing = null;
            bool? isVersionInvalid = null; // Flag to handle invalid search in Version Drop down

            if (!string.IsNullOrEmpty(version))
            {
                if (version == DefaultProduction)
                    isProofing = false;
                else if (version == DefaultProofing)
                    isProofing = true;
                else
                    isVersionInvalid = true;
            }
            else
            {
                isVersionInvalid = false;
            }

            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            SiteTextSortColumn SortColumn = SiteTextSortColumn.ResourceKey;
            switch (kendoGridPost.SortColumn)
            {
                case "Version":
                    SortColumn = SiteTextSortColumn.IsProofing;
                    break;
                case "ResourceKey":
                    SortColumn = SiteTextSortColumn.ResourceKey;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            if (isVersionInvalid == true) // if it is invalid search for Version drop down show No records found.
            {
                return Json(new { total = 0, data = Enumerable.Empty<SiteTextObjectModel>() });
            }
            else
            {
                return Json(new
                {
                    total = siteTextCacheFactory.GetEntitiesBySearch(new SiteTextSearchDetail() { SiteID = SessionSiteID, ResourceKey = resourceKey, IsProofing = isProofing }).Select(p => p.SiteTextID).Count(),
                    data = (from siteTextObjectModel in siteTextCacheFactory.GetEntitiesBySearch(
                                startRowIndex,
                                kendoGridPost.PageSize,
                                new SiteTextSearchDetail() { SiteID = SessionSiteID, ResourceKey = resourceKey, IsProofing = isProofing },
                                new SiteTextSortDetail() { Column = SortColumn, Order = sortOrder })
                            select new
                            {
                                SiteTextID = siteTextObjectModel.SiteTextID,
                                SiteID = siteTextObjectModel.SiteID,
                                ResourceKey = siteTextObjectModel.ResourceKey,
                                Text = siteTextObjectModel.Text,
                                IsProofing = siteTextObjectModel.IsProofing,
                                Version = siteTextObjectModel.IsProofing ? DefaultProofing : DefaultProduction,
                                VersionID = siteTextObjectModel.Version
                            })
                });
            }
        }

        /// <summary>
        /// Disables the specified site text identifier.
        /// </summary>
        /// <param name="siteTextID">The site text identifier.</param>
        /// <param name="versionID">The version identifier.</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult Disable(int siteTextID, int versionID, bool isProofing)
        {
            siteTextCacheFactory.DeleteEntity(new SiteTextObjectModel { SiteTextID = siteTextID, Version = versionID, IsProofing = isProofing }, SessionUserID);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Loads the default text for resource key.
        /// </summary>
        /// <param name="resourcekey">The resourcekey.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult LoadDefaultTextForResourceKey(string resourcekey)
        {
            string defaultTextForResourceKey = templateTextCacheFactory.GetEntitiesBySearch(
                new TemplateTextSearchDetail 
                { 
                    TemplateID = siteCacheFactory.GetEntityByKey(SessionSiteID).TemplateId, 
                    ResourceKey = resourcekey 
                }).Select(p => p.DefaultText).FirstOrDefault();

            if (resourcekey.Contains("css"))
            {
                defaultTextForResourceKey = CSSMinifyUnminifyHelper.UnMinifyCSSText(defaultTextForResourceKey);
            }
            else
            {
                defaultTextForResourceKey = System.Web.HttpUtility.HtmlDecode(defaultTextForResourceKey);
            }

            return Json(defaultTextForResourceKey, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Edits the specified site text identifier.
        /// </summary>
        /// <param name="siteTextID">The site text identifier.</param>
        /// <param name="versionID">The version identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult Edit(int siteTextID, int versionID)
        {
            EditSiteTextViewModel viewModel = new EditSiteTextViewModel();
            viewModel.ResourceKeys = new List<DisplayValuePair>();
            viewModel.ResourceKeys.Add(new DisplayValuePair { Value = defaultvalue, Display = defaultResource });

            IEnumerable<TemplateTextObjectModel> templateSiteTextDetails = templateTextCacheFactory.GetEntitiesBySearch(
                new TemplateTextSearchDetail { TemplateID = siteCacheFactory.GetEntityByKey(SessionSiteID).TemplateId });
            
            var ResourceKeyDetails = (from dbo in templateSiteTextDetails
                                      select dbo.ResourceKey);
            if (siteTextID > 0)
            {
                foreach (var item in ResourceKeyDetails)
                {
                    viewModel.ResourceKeys.Add(new DisplayValuePair { Display = item, Value = item });
                }

                SiteTextObjectModel siteTextObjectModel = siteTextCacheFactory.GetEntityByKey(new SiteTextKey(siteTextID, versionID));

                viewModel.SelectedResourceKey = siteTextObjectModel.ResourceKey;                
                if (templateSiteTextDetails.FirstOrDefault(p => p.ResourceKey == siteTextObjectModel.ResourceKey).IsHTML)
                {
                    viewModel.HtmlText = System.Web.HttpUtility.HtmlDecode(siteTextObjectModel.Text);
                }
                else 
                {
                    viewModel.PlainText = siteTextObjectModel.ResourceKey.Contains("css") ? CSSMinifyUnminifyHelper.UnMinifyCSSText(siteTextObjectModel.Text) : siteTextObjectModel.Text;
                }
                viewModel.SiteTextID = siteTextObjectModel.SiteTextID;
                viewModel.VersionID = siteTextObjectModel.Version;
                viewModel.IsProofing = siteTextObjectModel.IsProofing;
                viewModel.IsProofingAvailableForSiteTextId = siteTextObjectModel.IsProofingAvailableForSiteTextId;
                viewModel.UTCLastModifiedDate = siteTextObjectModel.LastModified;
                if (siteTextObjectModel.ModifiedBy != 0)
                {
                    viewModel.ModifiedByName = siteTextObjectModel.ModifiedBy.ToString();
                    if (userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = siteTextObjectModel.ModifiedBy }).Count() > 0)
                    {
                        viewModel.ModifiedByName = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = siteTextObjectModel.ModifiedBy }).FirstOrDefault().UserName;
                    }
                }
                else
                {
                    viewModel.ModifiedByName = string.Empty;
                }
            }
            else
            {
                IEnumerable<SiteTextObjectModel> siteTextDetails = siteTextCacheFactory.GetEntitiesBySearch(new SiteTextSearchDetail() { SiteID = SessionSiteID });
                var siteTextResourceKeys = siteTextDetails.Select(x => x.ResourceKey);

                ResourceKeyDetails = ResourceKeyDetails.Where(p => !siteTextResourceKeys.Contains(p));

                foreach (var item in ResourceKeyDetails)
                {
                    viewModel.ResourceKeys.Add(new DisplayValuePair { Display = item, Value = item });
                }

            }
            ViewData["Success"] = "In Progress";
            return View(viewModel);
        }

        /// <summary>
        /// Edits the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult Edit(EditSiteTextViewModel viewModel)
        {
            try
            {
                SiteTextObjectModel siteTextObjectModel = new SiteTextObjectModel
                {
                    SiteTextID = viewModel.SiteTextID,
                    Version = viewModel.VersionID,
                    ResourceKey = viewModel.SelectedResourceKey,
                    IsProofing = viewModel.IsProofing,
                    SiteID = SessionSiteID
                };

                if (siteTextObjectModel.ResourceKey.Contains("css"))
                {
                    siteTextObjectModel.Text = CSSMinifyUnminifyHelper.MinifyCSSText(viewModel.PlainText);
                }
                else
                {
                    siteTextObjectModel.Text = viewModel.HtmlText;
                }
                
                siteTextCacheFactory.SaveEntity(siteTextObjectModel, SessionUserID);

                viewModel.ResourceKeys = new List<DisplayValuePair>();
                viewModel.ResourceKeys.Add(new DisplayValuePair { Value = defaultvalue, Display = defaultResource });
                foreach (var item in (from dbo in  templateTextCacheFactory.GetEntitiesBySearch(
                                          new TemplateTextSearchDetail 
                                          {
                                              TemplateID = siteCacheFactory.GetEntityByKey(SessionSiteID).TemplateId 
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

        /// <summary>
        /// Checks whether resource key text is html or plain.
        /// </summary>
        /// <param name="resourceKey">Name of the resourceKey.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckIsHtmlTextForResourceKey(string resourceKey)
        {
            return Json(resourceKey == defaultvalue ? 
                true : templateTextCacheFactory.GetEntitiesBySearch(new TemplateTextSearchDetail { ResourceKey = resourceKey }).FirstOrDefault().IsHTML, 
                JsonRequestBehavior.AllowGet);
        }
    }
}