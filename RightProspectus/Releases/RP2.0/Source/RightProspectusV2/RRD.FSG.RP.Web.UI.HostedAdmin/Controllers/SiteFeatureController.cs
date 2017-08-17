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
    /// Class SiteFeatureController.
    /// </summary>
    public class SiteFeatureController : BaseController
    {
        #region Constants
        /// <summary>
        /// Constants
        /// </summary>
        private const string DefaultSiteFeature ="--Please select Site Feature Key--" ;
        private const string defaultvalue = "-1";
        #endregion

        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        
        /// <summary>
        /// The Site Feature cache factory
        /// </summary>
        private IFactoryCache<SiteFeatureFactory, SiteFeatureObjectModel, SiteFeatureKey> siteFeatureCacheFactory;

        /// <summary>
        /// The template feature  cache factory
        /// </summary>
        private IFactoryCache<TemplateFeatureFactory, TemplateFeatureObjectModel, TemplateFeatureKey> templateFeatureCacheFactory;

        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteFeatureController" /> class.
        /// </summary>
        public SiteFeatureController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                siteFeatureCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFeatureFactory, SiteFeatureObjectModel, SiteFeatureKey>>("SiteFeature");
                siteFeatureCacheFactory.ClientName = SessionClientName;
                siteFeatureCacheFactory.Mode = FactoryCacheMode.All;

                templateFeatureCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TemplateFeatureFactory, TemplateFeatureObjectModel, TemplateFeatureKey>>("TemplateFeature");
                templateFeatureCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;

                siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
                siteCacheFactory.ClientName = SessionClientName;
                siteCacheFactory.Mode = FactoryCacheMode.All;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteFeatureController" /> class.
        /// </summary>
        /// <param name="SiteFeatureFactoryCache">The site feature factory cache.</param>
        /// <param name="TemplateFeatureFactoryCache">The template feature factory cache.</param>
        /// <param name="UserFactoryCache">The user factory cache.</param>
        /// <param name="SiteFactoryCache">The site factory cache.</param>
        public SiteFeatureController(IFactoryCache<SiteFeatureFactory, SiteFeatureObjectModel,SiteFeatureKey> SiteFeatureFactoryCache, 
            IFactoryCache<TemplateFeatureFactory, TemplateFeatureObjectModel, TemplateFeatureKey> TemplateFeatureFactoryCache, 
            IFactoryCache<UserFactory, UserObjectModel, int> UserFactoryCache,
            IFactoryCache<SiteFactory, SiteObjectModel, int> SiteFactoryCache)
        {
            siteFeatureCacheFactory = SiteFeatureFactoryCache;
            siteFeatureCacheFactory.ClientName = SessionClientName;
            siteFeatureCacheFactory.Mode = FactoryCacheMode.All;

            templateFeatureCacheFactory = TemplateFeatureFactoryCache;
            templateFeatureCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = UserFactoryCache;
            userCacheFactory.Mode = FactoryCacheMode.All;

            siteCacheFactory = SiteFactoryCache;
            siteCacheFactory.ClientName = SessionClientName;
            siteCacheFactory.Mode = FactoryCacheMode.All;
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

        #region Action Methods for GET
        /// <summary>
        /// Edits the specified site feature key.
        /// </summary>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="siteFeatureKey">The site feature key.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult Edit(int siteId, string siteFeatureKey)
        {
            EditSiteFeatureViewModel viewModel = new EditSiteFeatureViewModel();
            viewModel.FeatureKeys = new List<DisplayValuePair>();
            viewModel.FeatureKeys.Add(new DisplayValuePair { Value = defaultvalue, Display = DefaultSiteFeature });

            var FeatureKeyDetails = templateFeatureCacheFactory.GetEntitiesBySearch(
                new TemplateFeatureSearchDetail 
            {
                    TemplateId = siteCacheFactory.GetEntitiesBySearch(new SiteSearchDetail { SiteID = SessionSiteID }).FirstOrDefault().TemplateId 
                }).Select(x => x.FeatureKey);

            if (siteId != 0)
            {
                foreach (var item in FeatureKeyDetails)
                {
                    viewModel.FeatureKeys.Add(new DisplayValuePair { Display = item, Value = item });
                }

                SiteFeatureObjectModel siteFeatureObjectModel =  siteFeatureCacheFactory.GetEntityByKey(new SiteFeatureKey(siteId, siteFeatureKey));

                viewModel.SiteId = siteFeatureObjectModel.SiteId;
                viewModel.SelectedFeatureKey = siteFeatureObjectModel.SiteKey;
                viewModel.FeatureMode = siteFeatureObjectModel.FeatureMode;
                viewModel.UTCLastModifiedDate = siteFeatureObjectModel.LastModified;
                viewModel.ModifiedBy = siteFeatureObjectModel.ModifiedBy;

                if (siteFeatureObjectModel.ModifiedBy != 0)
                {
                    viewModel.ModifiedByName = siteFeatureObjectModel.ModifiedBy.ToString();
                    if (userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = siteFeatureObjectModel.ModifiedBy }).Count() > 0)
                    {
                        viewModel.ModifiedByName = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = siteFeatureObjectModel.ModifiedBy }).FirstOrDefault().UserName;
                    }
                }
                else
                {
                    viewModel.ModifiedByName = string.Empty;
                }
            }
            else
            {
                var siteKeyCollection = siteFeatureCacheFactory.GetEntitiesBySearch(new SiteFeatureSearchDetail() { SiteId = SessionSiteID }).Select(x => x.SiteKey);
                foreach (var item in FeatureKeyDetails.Where(p => !siteKeyCollection.Contains(p)))
                {
                    viewModel.FeatureKeys.Add(new DisplayValuePair { Display = item, Value = item });
                }
                viewModel.SelectedFeatureKey = defaultvalue;
            }

            ViewData["Success"] = "In Progress";
            return View(viewModel);
        }

        /// <summary>
        /// Edits the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="selectedFeatureModes">The selected feature modes.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult Edit(EditSiteFeatureViewModel viewModel, string selectedFeatureModes)
        {
            try
            {
                viewModel.FeatureKeys = new List<DisplayValuePair>();

                SiteFeatureObjectModel siteFeatureObjectModel = new SiteFeatureObjectModel()
                {
                    SiteId = SessionSiteID,
                    SiteKey = viewModel.SelectedFeatureKey,
                    FeatureMode = CalculateFeatureMode(selectedFeatureModes)
                };

                siteFeatureCacheFactory.SaveEntity(siteFeatureObjectModel, SessionUserID);

                viewModel.FeatureKeys.Add(new DisplayValuePair { Value = defaultvalue, Display = DefaultSiteFeature });
                foreach (var item in templateFeatureCacheFactory.GetAllEntities().Select(x => x.FeatureKey))
                {
                    viewModel.FeatureKeys.Add(new DisplayValuePair { Display = item, Value = item });
                }

                viewModel.SelectedFeatureKey = viewModel.SelectedFeatureKey;

                ViewData["Success"] = "OK";
            }
            catch
            {

            }
            return View(viewModel);
        }

        #endregion

        #region GetAllFeatures

        /// <summary>
        /// Get All records
        /// </summary>
        /// <param name="siteFeatureKey">The site feature key.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult Search(string siteFeatureKey)
        {
            if (string.IsNullOrWhiteSpace(siteFeatureKey))
                siteFeatureKey = null;

            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;
            SiteFeatureSortColumn SortColumn = SiteFeatureSortColumn.SiteKey;

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            return Json(new
            {
                total = siteFeatureCacheFactory.GetEntitiesBySearch(
                    new SiteFeatureSearchDetail()
                    {
                        SiteId = SessionSiteID,
                        SiteKey = siteFeatureKey
                    }).Select(p => p.SiteKey).Count(),
                data = (from siteFeatureObjectModel in siteFeatureCacheFactory.GetEntitiesBySearch(
                            startRowIndex,
                            kendoGridPost.PageSize,
                                                                                                       new SiteFeatureSearchDetail() { SiteId = SessionSiteID, SiteKey = siteFeatureKey },
                            new SiteFeatureSortDetail() { Column = SortColumn, Order = sortOrder })
                        select new 
            {
                            SiteId = siteFeatureObjectModel.SiteId,
                            SiteKey = siteFeatureObjectModel.SiteKey 
                        })
            });
        }
        #endregion

        #region Load Search Criteria
        /// <summary>
        /// Gets the template feature keys.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetTemplateFeatureKeys()
        {
            return Json((from key in
                             (from dbo in siteFeatureCacheFactory.GetEntitiesBySearch(new SiteFeatureSearchDetail() { SiteId = SessionSiteID, })
                              select dbo.SiteKey).Distinct().OrderBy(SiteKey => SiteKey)
                         select new { Display = key, Value = key }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Feature Modes By Key
        /// </summary>
        /// <param name="siteFeatureKey">The site feature key.</param>
        /// <returns>JsonResult.</returns>
        public JsonResult GetFeatureModesByKey(string siteFeatureKey)
        {
            //Using sessionSiteId as we should be showing only the features of the site we are in.
            int featureMode = siteFeatureCacheFactory.GetEntityByKey(new SiteFeatureKey(SessionSiteID, siteFeatureKey)).FeatureMode;

            List<MultiSelectDropDownViewModel> vmMultiSelect = new List<MultiSelectDropDownViewModel>();
            if (siteFeatureKey == "XBRL")
            {
                vmMultiSelect = GetFeatureModesXBRL(featureMode);
            }
            else if (siteFeatureKey == "RequestMaterial")
            {
                vmMultiSelect = GetFeatureModesRequestMaterial(featureMode);
            }
            return Json(vmMultiSelect, JsonRequestBehavior.AllowGet); ;

        }
        /// <summary>
        /// Get All Feature Modes
        /// </summary>
        /// <param name="siteFeatureKey">The site feature key.</param>
        /// <returns>JsonResult.</returns>
        public JsonResult GetFeatureModes(string siteFeatureKey)
        {
            List<MultiSelectDropDownViewModel> lstFeatureModesVM = new List<MultiSelectDropDownViewModel>();

            if (siteFeatureKey == "XBRL")
            {
                FeatureEnums.XBRL[] enums = (FeatureEnums.XBRL[])Enum.GetValues(typeof(FeatureEnums.XBRL));

                foreach (var item in enums)
                {
                    MultiSelectDropDownViewModel vmMultiSelect = new MultiSelectDropDownViewModel()
                    {
                        label = item.ToString(),
                        value = Convert.ToInt32(item).ToString(),
                        title = item.ToString()
                    };
                    lstFeatureModesVM.Add(vmMultiSelect);
                }
            }
            else if (siteFeatureKey == "RequestMaterial")
            {
                FeatureEnums.RequestMaterial[] enums = (FeatureEnums.RequestMaterial[])Enum.GetValues(typeof(FeatureEnums.RequestMaterial));

                foreach (var item in enums)
                {
                    MultiSelectDropDownViewModel vmMultiSelect = new MultiSelectDropDownViewModel()
                    {
                        label = item.ToString(),
                        value = Convert.ToInt32(item).ToString(),
                        title = item.ToString()
                    };
                    lstFeatureModesVM.Add(vmMultiSelect);
                }
            }

            return Json(lstFeatureModesVM, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region FaeatureModes
        /// <summary>
        /// Calculates FeatureMode by selected Feature Modes
        /// </summary>
        /// <param name="selectedFeatureModes">The selected feature modes.</param>
        /// <returns>System.Int32.</returns>
        public int CalculateFeatureMode(string selectedFeatureModes)
        {
            string[] featureMode = selectedFeatureModes.Split(',');
            int mode = 0;

            mode = Convert.ToInt32(featureMode[0]);
            if (featureMode.Length > 1)
            { 
                for (int i = 1; i < featureMode.Length; i++)
                {
                    mode = mode | Convert.ToInt32(featureMode[i]);
                }
            }
            return mode;
        }
        
        /// <summary>
        /// Get Feature Modes for XBRL
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;MultiSelectDropDownViewModel&gt;.</returns>
        public List<MultiSelectDropDownViewModel> GetFeatureModesXBRL(int mode)
        {
            FeatureEnums.XBRL featureMode = (FeatureEnums.XBRL)mode;
            List<MultiSelectDropDownViewModel> featureModes = new List<MultiSelectDropDownViewModel>();

            if (featureMode.HasFlag(FeatureEnums.XBRL.Disabled))
            {
                featureModes.Add(new MultiSelectDropDownViewModel { value = Convert.ToInt32(FeatureEnums.XBRL.Disabled).ToString() });
            }
            if (featureMode.HasFlag(FeatureEnums.XBRL.Enabled))
            {
                featureModes.Add(new MultiSelectDropDownViewModel { value = Convert.ToInt32(FeatureEnums.XBRL.Enabled).ToString() });
            }
            if (featureMode.HasFlag(FeatureEnums.XBRL.ShowXBRLInNewTab))
            {
                featureModes.Add(new MultiSelectDropDownViewModel { value = Convert.ToInt32(FeatureEnums.XBRL.ShowXBRLInNewTab).ToString() });
            }
            if (featureMode.HasFlag(FeatureEnums.XBRL.ShowXBRLInTabbedView))
            {
                featureModes.Add(new MultiSelectDropDownViewModel { value = Convert.ToInt32(FeatureEnums.XBRL.ShowXBRLInTabbedView).ToString() });
            }
            if (featureMode.HasFlag(FeatureEnums.XBRL.ShowXBRLInLandingPage))
            {
                featureModes.Add(new MultiSelectDropDownViewModel { value = Convert.ToInt32(FeatureEnums.XBRL.ShowXBRLInLandingPage).ToString() });
            }

            return featureModes;
        }
        /// <summary>
        /// Get All Feature Modes for RequestMaterial
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;MultiSelectDropDownViewModel&gt;.</returns>
        public List<MultiSelectDropDownViewModel> GetFeatureModesRequestMaterial(int mode)
        {
            FeatureEnums.RequestMaterial featureMode = (FeatureEnums.RequestMaterial)mode;
            List<MultiSelectDropDownViewModel> featureModes = new List<MultiSelectDropDownViewModel>();

            if (featureMode.HasFlag(FeatureEnums.RequestMaterial.Disabled))
            {
                featureModes.Add(new MultiSelectDropDownViewModel { value = Convert.ToInt32(FeatureEnums.RequestMaterial.Disabled).ToString() });
            }
            if (featureMode.HasFlag(FeatureEnums.RequestMaterial.Enabled))
            {
                featureModes.Add(new MultiSelectDropDownViewModel { value = Convert.ToInt32(FeatureEnums.RequestMaterial.Enabled).ToString() });
            }

            return featureModes;
        }

        #endregion

        #region DisableSiteFeature
        /// <summary>
        /// Disables the Site Feature.
        /// </summary>
        /// <param name="SiteFeatureKey">Key of the Site.</param>       
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DisableSiteFeature(string SiteFeatureKey)
        {
            siteFeatureCacheFactory.DeleteEntity(new SiteFeatureObjectModel()
            {
                SiteId = SessionSiteID,
                SiteKey = SiteFeatureKey
            }, SessionUserID);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
