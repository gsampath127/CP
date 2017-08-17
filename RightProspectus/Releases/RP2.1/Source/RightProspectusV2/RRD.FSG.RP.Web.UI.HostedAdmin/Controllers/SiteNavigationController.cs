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
using System.Collections;
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
    /// Class SiteNavigationController.
    /// </summary>
    public class SiteNavigationController : BaseController
    {
        #region Constant
        ///<summary>
        ///Constants values
        ///</summary>
        private const string defaultvalue = "-1";
        private const string defaultpage = "--Please select Page --";
        private const string defaultNavigation = "--Please select NavigationKey--";

        #endregion
        /// <summary>
        /// userCacheFactory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        /// <summary>
        /// SiteNavigation cache factory
        /// </summary>
        private IFactoryCache<SiteNavigationFactory, SiteNavigationObjectModel, SiteNavigationKey> siteNavigationCacheFactory;
        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;
        /// <summary>
        /// The template page cache factory
        /// </summary>
        private IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey> templatePageCacheFactory;
        /// <summary>
        /// The template Navigation cache factory
        /// </summary>
        private IFactoryCache<TemplateNavigationFactory, TemplateNavigationObjectModel, TemplateNavigationKey> templateNavigationCacheFactory;
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteNavigationController" /> class.
        /// </summary>
        public SiteNavigationController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;

                this.siteNavigationCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteNavigationFactory, SiteNavigationObjectModel, SiteNavigationKey>>("SiteNavigation");
                this.siteNavigationCacheFactory.ClientName = SessionClientName;
                this.siteNavigationCacheFactory.Mode = FactoryCacheMode.All;

                siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
                siteCacheFactory.ClientName = SessionClientName;
                siteCacheFactory.Mode = FactoryCacheMode.All;

                this.templatePageCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>>("TemplatePage");
                this.templatePageCacheFactory.Mode = FactoryCacheMode.All;

                this.templateNavigationCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplateNavigationFactory, TemplateNavigationObjectModel, TemplateNavigationKey>>("TemplateNavigation");
                this.templateNavigationCacheFactory.Mode = FactoryCacheMode.All;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteNavigationController" /> class.
        /// </summary>
        /// <param name="SiteNavigationFactoryCache">The site navigation factory cache.</param>
        /// <param name="UserFactoryCache">The user factory cache.</param>
        /// <param name="SiteFactoryCache">The site factory cache.</param>
        /// <param name="TemplatePageFactoryCache">The template page factory cache.</param>
        /// <param name="TemplateNavigationFactoryCache">The template navigation factory cache.</param>
        public SiteNavigationController(IFactoryCache<SiteNavigationFactory, SiteNavigationObjectModel, SiteNavigationKey> SiteNavigationFactoryCache, IFactoryCache<UserFactory, UserObjectModel, int> UserFactoryCache, IFactoryCache<SiteFactory, SiteObjectModel, int> SiteFactoryCache, IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey> TemplatePageFactoryCache, IFactoryCache<TemplateNavigationFactory, TemplateNavigationObjectModel, TemplateNavigationKey> TemplateNavigationFactoryCache)
        {
            userCacheFactory = UserFactoryCache;
            userCacheFactory.Mode = FactoryCacheMode.All;

            this.siteNavigationCacheFactory = SiteNavigationFactoryCache;
            this.siteNavigationCacheFactory.ClientName = SessionClientName;
            this.siteNavigationCacheFactory.Mode = FactoryCacheMode.All;

            siteCacheFactory = SiteFactoryCache;
            siteCacheFactory.ClientName = SessionClientName;
            siteCacheFactory.Mode = FactoryCacheMode.All;

            this.templatePageCacheFactory = TemplatePageFactoryCache;
            this.templatePageCacheFactory.Mode = FactoryCacheMode.All;

            this.templateNavigationCacheFactory = TemplateNavigationFactoryCache;
            this.templateNavigationCacheFactory.Mode = FactoryCacheMode.All;
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
        /// Edits the SiteNavigation
        /// </summary>
        /// <param name="SiteNavigationId">The site navigation identifier.</param>
        /// <param name="versionID">The version identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult Edit(int SiteNavigationId, int versionID)
        {
            EditSiteNavigationViewModel viewModel = new EditSiteNavigationViewModel();
            viewModel.PageDescriptions = new List<DisplayValuePair>();
            viewModel.PageDescriptions.Add(new DisplayValuePair { Value = null, Display = defaultpage });

            viewModel.NavigationKeys = new List<DisplayValuePair>();

            var templateId = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId;

            var pageNameDetails = (from dbo in templatePageCacheFactory.GetEntitiesBySearch(new TemplatePageSearchDetail { TemplateID = templateId })
                                   select new { dbo.PageDescription, dbo.PageID }).Distinct();

            var navigationKeyDetails = templateNavigationCacheFactory.GetEntitiesBySearch(new TemplateNavigationSearchDetail { TemplateID = templateId })
                .Select(x => x.NavigationKey);

            var siteNavigationDetails = siteNavigationCacheFactory.GetEntitiesBySearch(new SiteNavigationSearchDetail() { SiteId = SessionSiteID });

            if (SiteNavigationId > 0)
            {
                SiteNavigationObjectModel SiteNavigationObjectModel = (from dbo in siteNavigationDetails
                                                                       where dbo.SiteNavigationId == SiteNavigationId && dbo.Version == versionID
                                                                       select dbo).FirstOrDefault();

                foreach (var item in navigationKeyDetails)
                {
                    viewModel.NavigationKeys.Add(new DisplayValuePair { Display = item, Value = item });
                }

                viewModel.SelectedNavigationKey = SiteNavigationObjectModel.NavigationKey;
                viewModel.SiteNavigationId = SiteNavigationId;
                viewModel.NavigationXML = SiteNavigationObjectModel.NavigationXML;
                viewModel.VersionID = SiteNavigationObjectModel.Version;
                viewModel.IsProofing = SiteNavigationObjectModel.IsProofing;
                viewModel.IsProofingAvailableForSiteNavigationId = SiteNavigationObjectModel.IsProofingAvailableForSiteNavigationId;           
                viewModel.UTCLastModifiedDate = SiteNavigationObjectModel.LastModified;
                viewModel.NavigationXML = SiteNavigationObjectModel.NavigationXML;

                if (SiteNavigationObjectModel.ModifiedBy != 0)
                {
                    viewModel.ModifiedByName = SiteNavigationObjectModel.ModifiedBy.ToString();
                    if (userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = SiteNavigationObjectModel.ModifiedBy }).Count() > 0)
                    {
                        viewModel.ModifiedByName = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = SiteNavigationObjectModel.ModifiedBy }).FirstOrDefault().UserName;
                    }
                }
                else
                {
                    viewModel.ModifiedByName = string.Empty;
                }

                foreach (var item in pageNameDetails)
                {
                    viewModel.PageDescriptions.Add(new DisplayValuePair { Display = item.PageDescription, Value = item.PageID.ToString() });
                }
                viewModel.SelectedPageID = SiteNavigationObjectModel.PageId;
            }
            else
            {
                viewModel.NavigationKeys.Add(new DisplayValuePair { Value = defaultvalue, Display = defaultNavigation });
                foreach (var item in navigationKeyDetails)
                {
                    var siteNavigationKeys = from dbo in siteNavigationDetails
                                             where dbo.NavigationKey == item
                                             select new { dbo.NavigationKey, dbo.PageId };
                    if (siteNavigationKeys.Count() <= pageNameDetails.Count())
                    {
                        viewModel.NavigationKeys.Add(new DisplayValuePair { Display = item, Value = item });
                    }
                }
            }
            ViewData["Success"] = "In Progress";
            return View(viewModel);
        }

        /// <summary>
        /// Edits the SiteNavigation
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost, ValidateInput(false)]
        [IsPopUp]
        public ActionResult Edit(EditSiteNavigationViewModel viewModel)
        {
            try
            {
                SiteNavigationObjectModel siteNavigationObjectModel = new SiteNavigationObjectModel
                {
                    SiteNavigationId = viewModel.SiteNavigationId,
                    Version = viewModel.VersionID,
                    NavigationKey = viewModel.SelectedNavigationKey,
                    NavigationXML = viewModel.NavigationXML,
                    PageId = viewModel.SelectedPageID,
                    IsProofing = viewModel.IsProofing,
                    SiteId = SessionSiteID
                };
                siteNavigationCacheFactory.SaveEntity(siteNavigationObjectModel, SessionUserID);

                viewModel.PageDescriptions = new List<DisplayValuePair>();
                //Setting the default value as null because we have the option to add sitenavigation entry without selecting a page
                viewModel.PageDescriptions.Add(new DisplayValuePair { Value = null, Display = defaultpage }); 

                foreach (var item in (from dbo in templatePageCacheFactory.GetAllEntities()
                                      select new { dbo.PageDescription, dbo.PageID }).Distinct())
                {
                    viewModel.PageDescriptions.Add(new DisplayValuePair { Display = item.PageDescription, Value = item.PageID.ToString() });
                }

                viewModel.NavigationKeys = new List<DisplayValuePair>();

                foreach (var item in (from dbo in templateNavigationCacheFactory.GetEntitiesBySearch(
                                         new TemplateNavigationSearchDetail
                                         {
                                             TemplateID = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId,
                                             NavigationKey = viewModel.SelectedNavigationKey
                                         })
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

        /// <summary>
        /// Get NavigationKeys
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetNavigationKey()
        {
            return Json((from dbo in siteNavigationCacheFactory.GetEntitiesBySearch(new SiteNavigationSearchDetail { SiteId = SessionSiteID })
                         select new { Display = dbo.NavigationKey, Value = dbo.NavigationKey }).Distinct(), 
                         JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get PageNames
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetPageNames()
        {
            return Json((from dbo in siteNavigationCacheFactory.GetEntitiesBySearch(new SiteNavigationSearchDetail { SiteId = SessionSiteID })
                         where dbo.PageId != null && dbo.PageId != 0
                         select new { Display = dbo.PageDescription, Value = dbo.PageId }).Distinct(),
                         JsonRequestBehavior.AllowGet);
        }

        #region GetAllSiteNavigationtDetails
        /// <summary>
        /// Gets all SiteNavigation details.
        /// </summary>
        /// <param name="NavigationKey">The navigation key.</param>
        /// <param name="PageId">The page identifier.</param>
        /// <param name="Version">The version.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllSiteNavigationDetails(string NavigationKey, string PageId, string Version)
        {
            bool? isProofing = null;
            bool isVersionInvalid = false;
            if (!string.IsNullOrEmpty(Version))  //For invalid Search in Version Drop Down
            {
                if (Version == "Production")
                {
                    isProofing = false;
                }
                else if (Version == "Proofing")
                {
                    isProofing = true;
                }
                else
                {
                    isVersionInvalid = true;
                }
            }
            else
            {
                isVersionInvalid = false;
            }

            int id;
            SiteNavigationSearchDetail objSearchDetail = new SiteNavigationSearchDetail()
            {
                SiteId = SessionSiteID,
                NavigationKey = string.IsNullOrEmpty(NavigationKey) ? null : NavigationKey,
                IsProofing = isProofing,
                PageId = !(string.IsNullOrEmpty(PageId)) ? (int.TryParse(PageId, out id) ? id : 0) : (int?)null
            };

            if (isVersionInvalid)
            {
                return Json(new { total = 0, data = Enumerable.Empty<SiteNavigationObjectModel>() });
            }
            else
            {
                KendoGridPost kendoGridPost = new KendoGridPost();
                int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

                SiteNavigationSortColumn SortColumn = SiteNavigationSortColumn.NavigationKey;
                switch (kendoGridPost.SortColumn)
                {
                    case "PageName":
                        SortColumn = SiteNavigationSortColumn.PageName;
                        break;
                    case "PageDescription":
                        SortColumn = SiteNavigationSortColumn.PageDescription;
                        break;
                    case "Version":
                        SortColumn = SiteNavigationSortColumn.IsProofing;
                        break;
                    case "Text":
                        SortColumn = SiteNavigationSortColumn.Text;
                        break;
                }

                SortOrder sortOrder = SortOrder.Ascending;
                if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
                {
                    sortOrder = SortOrder.Descending;
                }

                return Json(new
                {
                    total = siteNavigationCacheFactory.GetEntitiesBySearch(objSearchDetail).Select(p => p.SiteNavigationId).Count(),
                    data = from obj in siteNavigationCacheFactory.GetEntitiesBySearch(
                               startRowIndex,
                               kendoGridPost.PageSize,
                               objSearchDetail,
                               new SiteNavigationSortDetail() { Column = SortColumn, Order = sortOrder })
                           select new 
                           {
                               SiteNavigationId = obj.SiteNavigationId,
                               PageName = obj.PageName,
                               NavigationKey = obj.NavigationKey,
                               IsProofing = obj.IsProofing,
                               VersionID = obj.Version,
                               PageDescription = obj.PageDescription,
                               Version = obj.IsProofing ? "Proofing" : "Production"
                           }
                });
            }
        }
        #endregion

        #region DisableSiteNavigation
        /// <summary>
        /// Disables the Site Navigation.
        /// </summary>
        /// <param name="siteNavigationId">site Navigation Id.</param>
        /// <param name="versionID">The version identifier.</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DisableSiteNavigation(int siteNavigationId, int versionID, bool isProofing)
        {
            siteNavigationCacheFactory.DeleteEntity(new SiteNavigationObjectModel()
            {
                SiteNavigationId = siteNavigationId,
                Version = versionID,
                IsProofing = isProofing
            }, SessionUserID);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /// <summary>
        /// Load Pages for NavigationKey
        /// </summary>
        /// <param name="navigationkey">The navigationkey.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult LoadPagesForNavigationKey(string navigationkey)
        {
            var PageDescriptions = new List<DisplayValuePair>();
            //Setting the default value as null because we have the option to add sitenavigation entry without selecting a page
            PageDescriptions.Add(new DisplayValuePair { Value = null, Display = defaultpage });

            var pageDetails = siteNavigationCacheFactory.GetEntitiesBySearch(
                new SiteNavigationSearchDetail()
                {
                    SiteId = SessionSiteID,
                    NavigationKey = navigationkey
                }).Distinct().Select(x => x.PageId);

            var templateId = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId;
            foreach (var item in (from dbo in templatePageCacheFactory.GetEntitiesBySearch(new TemplatePageSearchDetail { TemplateID = templateId })
                                  select new { dbo.PageDescription, dbo.PageID }).Distinct().Where(p => !pageDetails.Contains(p.PageID)))
            {
                PageDescriptions.Add(new DisplayValuePair { Display = item.PageDescription, Value = item.PageID.ToString() });
            }
            return Json(PageDescriptions, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Loads the default NavigationXML for NavigationKey.
        /// </summary>
        /// <param name="navigationkey">The navigation key.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult LoadDefaultNavigationXMLForNavigationKey(string navigationkey)
        {
            return Json(templateNavigationCacheFactory.GetEntitiesBySearch(
                new TemplateNavigationSearchDetail
                {
                    TemplateID = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId,
                    NavigationKey = navigationkey
                }).Select(p => p.DefaultNavigationXml).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

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

                    if (string.IsNullOrWhiteSpace(namespaceName) || namespaceName != "http://rightprospectus.com/hostedSchema")
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

        /// <summary>
        /// Check SiteNavigation Already Exists
        /// </summary>
        /// <param name="navigationKey">The navigation key.</param>
        /// <param name="pageID">The page identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckSiteNavigationAlreadyExists(string navigationKey, int? pageID)
        {
            bool isSiteNavigationAlreadyExists = false;
            
            var siteNavigationKeys = from dbo in siteNavigationCacheFactory.GetEntitiesBySearch(new SiteNavigationSearchDetail() { SiteId = SessionSiteID })
                                     where dbo.NavigationKey == navigationKey && dbo.PageId == pageID
                                     select dbo;
            if (siteNavigationKeys != null && siteNavigationKeys.Count() > 0)
            {
                isSiteNavigationAlreadyExists = true;
            }
            return Json(isSiteNavigationAlreadyExists, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Load Version to ComboBox
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetVersions()
        {
            return Json((from dbo in siteNavigationCacheFactory.GetEntitiesBySearch(new SiteNavigationSearchDetail() { SiteId = SessionSiteID })
                         select new
                         {
                             Display = dbo.IsProofing ? "Proofing" : "Production",
                             Value = dbo.IsProofing ? "Proofing" : "Production"
                         }).Distinct().OrderBy(obj => obj.Display)
                         , JsonRequestBehavior.AllowGet);
        }
    }
}
