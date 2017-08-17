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
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
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
    /// Class TaxonomyLevelExternalIdController.
    /// </summary>
    public class TaxonomyLevelExternalIdController : BaseController
    {
        #region Constants
        /// <summary>
        /// Providing constant values for the dropdown
        /// </summary>
        private const string dropLevel = "--Please select Level--";
        private const string dropTaxonomy = "--Please select Taxonomy--";
        private const string dropvalue = "-1";
        #endregion

        #region Variables
        /// <summary>
        /// The taxonomy level external identifier cache factory
        /// </summary>
        private IFactoryCache<TaxonomyLevelExternalIdFactory, TaxonomyLevelExternalIdObjectModel, TaxonomyLevelExternalIdKey> taxonomyLevelexternalIdCacheFactory;
        /// <summary>
        /// The taxonomy cache factory
        /// </summary>
        private IFactoryCache<TaxonomyFactory, TaxonomyObjectModel, TaxonomyKey> taxonomyCacheFactory;
        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;

        /// <summary>
        /// The taxonomy level factory
        /// </summary>

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomyLevelExternalIdController"/> class.
        /// </summary>
        public TaxonomyLevelExternalIdController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                taxonomyLevelexternalIdCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TaxonomyLevelExternalIdFactory, TaxonomyLevelExternalIdObjectModel, TaxonomyLevelExternalIdKey>>("TaxonomyLevelExternalId");
                taxonomyLevelexternalIdCacheFactory.ClientName = SessionClientName;
                taxonomyLevelexternalIdCacheFactory.Mode = FactoryCacheMode.All;

                taxonomyCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TaxonomyFactory, TaxonomyObjectModel, TaxonomyKey>>("Taxonomy");
                taxonomyCacheFactory.ClientName = SessionClientName;
                taxonomyCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;
            }
        }
        #endregion

        #region Constructor_Test
        /// <summary>
        /// Constructor for the Test Methods
        /// </summary>
        /// <param name="TaxonomyLevelExternalIdFactoryCache">The taxonomy level external identifier factory cache.</param>
        /// <param name="UserFactoryCache">The user factory cache.</param>
        /// <param name="TaxonomyLevelFactoryCache">The taxonomy level factory cache.</param>
        public TaxonomyLevelExternalIdController(IFactoryCache<TaxonomyLevelExternalIdFactory, TaxonomyLevelExternalIdObjectModel, TaxonomyLevelExternalIdKey> TaxonomyLevelExternalIdFactoryCache, 
            IFactoryCache<UserFactory, UserObjectModel, int> UserFactoryCache, 
            IFactoryCache<TaxonomyFactory, TaxonomyObjectModel, TaxonomyKey> TaxonomyLevelFactoryCache)
        {
            taxonomyLevelexternalIdCacheFactory = TaxonomyLevelExternalIdFactoryCache;
            taxonomyLevelexternalIdCacheFactory.ClientName = SessionClientName;
            taxonomyLevelexternalIdCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = UserFactoryCache;
            userCacheFactory.Mode = FactoryCacheMode.All;

            taxonomyCacheFactory = TaxonomyLevelFactoryCache;
            taxonomyCacheFactory.ClientName = SessionClientName;
            taxonomyCacheFactory.Mode = FactoryCacheMode.All;
        }

        #endregion

        #region View
        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult List()
        {
            ViewData["SelectedCustomer"] = SessionClientName;
            return View();
        }
        #endregion

        #region GetAllTaxonomyLevelExternalIdDetails
        /// <summary>
        /// Gets all taxonomy level external identifier details.
        /// </summary>
        /// <param name="levelID">The level identifier.</param>
        /// <param name="taxonomyID">The taxonomy identifier.</param>
        /// <param name="externalID">The external identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllTaxonomyLevelExternalIdDetails(string levelID, string taxonomyID, string externalID)
        {
            int id;
            TaxonomyLevelExternalIdSearchDetail objSearchDetail = new TaxonomyLevelExternalIdSearchDetail()
            {
                Level = !(string.IsNullOrEmpty(levelID)) ? (int.TryParse(levelID, out id) ? id : 0) : (int?)null,
                TaxonomyId = !(string.IsNullOrEmpty(taxonomyID)) ? (int.TryParse(taxonomyID, out id) ? id : 0) : (int?)null,
                ExternalId = string.IsNullOrEmpty(externalID) ? null : externalID
            };
            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            TaxonomyLevelExternalIdSortColumn SortColumn = TaxonomyLevelExternalIdSortColumn.Level;
            switch (kendoGridPost.SortColumn)
            {
                case "Taxonomy":
                    SortColumn = TaxonomyLevelExternalIdSortColumn.TaxonomyName;
                    break;
                case "ExternalId":
                    SortColumn = TaxonomyLevelExternalIdSortColumn.ExternalId;
                    break;
                case "IsPrimary":
                    SortColumn = TaxonomyLevelExternalIdSortColumn.IsPrimary;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            return Json
                (new
                    {
                        total = taxonomyLevelexternalIdCacheFactory.GetEntitiesBySearch(objSearchDetail).Select(p => p.TaxonomyId).Count(),
                        data = (from dbo in taxonomyLevelexternalIdCacheFactory.GetEntitiesBySearch(
                                    startRowIndex,
                                    kendoGridPost.PageSize,
                                    objSearchDetail,
                                    new TaxonomyLevelExternalIdSortDetail() { Column = SortColumn, Order = sortOrder })
                                select new
                                {
                                    LevelId = dbo.Level,
                                    Level = Enum.GetName(typeof(TaxonomyLevelEnums), dbo.Level),
                                    TaxonomyId = dbo.TaxonomyId,
                                    Taxonomy = dbo.TaxonomyName,
                                    ExternalId = dbo.ExternalId,
                                    IsPrimary = dbo.IsPrimary.ToString(),
                                    SendDocumentUpdate = dbo.SendDocumentUpdate.ToString()
                                })
                    });
        }
        #endregion

        #region Load Search Creteria Methods.

        /// <summary>
        /// Gets the levels.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetLevels()
        {
            List<DisplayValuePair> lstDisplayValuePair = new List<DisplayValuePair>();

            foreach (int key in Enum.GetValues(typeof(TaxonomyLevelEnums)))
            {
                lstDisplayValuePair.Add(new DisplayValuePair { Display = Enum.GetName(typeof(TaxonomyLevelEnums), key), Value = key.ToString() });
            }
            return Json(lstDisplayValuePair, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Gets the taxonomy.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetTaxonomy()
        {
            return Json((from dbo in taxonomyLevelexternalIdCacheFactory.GetAllEntities()
                         select new { Display = dbo.TaxonomyName, Value = dbo.TaxonomyId.ToString() }).Distinct(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the external identifier.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetExternalId()
        {
            return Json((from dbo in taxonomyLevelexternalIdCacheFactory.GetAllEntities()
                         select new { Display = dbo.ExternalId, Value = dbo.ExternalId.ToString() }).Distinct(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Edit
        /// <summary>
        /// Edit Taxonomy Level External - GET
        /// </summary>
        /// <param name="levelId">The level identifier.</param>
        /// <param name="TaxonomyIdentifier">The taxonomy identifier.</param>
        /// <param name="externalIdentifier">The external identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult EditTaxonomyLevelExternalId(int? levelId, int? TaxonomyIdentifier, string externalIdentifier)
        {
            EditTaxonomyLevelExternalIdViewModel viewModel = new EditTaxonomyLevelExternalIdViewModel();
            
            viewModel.Level = new List<DisplayValuePair>();
            viewModel.Level.Add(new DisplayValuePair { Value = dropvalue, Display = dropLevel });
            foreach (int key in Enum.GetValues(typeof(TaxonomyLevelEnums)))
            {
                viewModel.Level.Add(new DisplayValuePair { Display = Enum.GetName(typeof(TaxonomyLevelEnums), key), Value = key.ToString() });
            }

            viewModel.TaxonomyId = new List<DisplayValuePair>();
            viewModel.TaxonomyId.Add(new DisplayValuePair { Value = dropvalue, Display = dropTaxonomy });
            var taxonomyDetails = taxonomyCacheFactory.GetAllEntities();
            if (taxonomyDetails != null)
            {
                foreach (var taxonomyObjectModel in taxonomyDetails.OrderBy(x => x.TaxonomyName))
                {
                    viewModel.TaxonomyId.Add(new DisplayValuePair { Display = taxonomyObjectModel.TaxonomyName, Value = taxonomyObjectModel.TaxonomyId.ToString() });
                }
            }

            if ((TaxonomyIdentifier.HasValue && TaxonomyIdentifier.Value > 0) && (levelId.HasValue && levelId.Value > 0))
            {
                //TaxonomyLevelExternalIdObjectModel objObjectModel =taxonomyLevelexternalIdCacheFactory.GetEntityByKey(new TaxonomyLevelExternalIdKey(levelId.Value, TaxonomyIdentifier.Value, externalIdentifier));
                
                
                IEnumerable<TaxonomyLevelExternalIdObjectModel> objTaxonomyLevelExternalDetails = taxonomyLevelexternalIdCacheFactory.GetAllEntities();
                TaxonomyLevelExternalIdObjectModel objObjectModel = (from dbo in objTaxonomyLevelExternalDetails
                                                                     where dbo.Level == levelId && dbo.TaxonomyId == TaxonomyIdentifier && dbo.ExternalId.Equals(externalIdentifier)
                                                                     select dbo).FirstOrDefault();
                    
                
                viewModel.SelectedLevelId = objObjectModel.Level;
                viewModel.SelectedTaxonomyId = objObjectModel.TaxonomyId;
                viewModel.ExternalId = objObjectModel.ExternalId;
                viewModel.IsPrimary = objObjectModel.IsPrimary;
                viewModel.SendDocumentUpdate = objObjectModel.SendDocumentUpdate;
                viewModel.UTCLastModifiedDate = objObjectModel.LastModified;
                viewModel.ModifiedBy = objObjectModel.ModifiedBy;

                if (objObjectModel.ModifiedBy != 0)
                {
                    viewModel.ModifiedByName = objObjectModel.ModifiedBy.ToString();
                    if (userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = objObjectModel.ModifiedBy }).Count() > 0)
                    {
                        viewModel.ModifiedByName = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = objObjectModel.ModifiedBy }).FirstOrDefault().UserName;
                    }
                }
                else
                {
                    viewModel.ModifiedByName = string.Empty;
                }
            }
            else
            {
                viewModel.SelectedLevelId = -1;
                viewModel.SelectedTaxonomyId = -1;
                viewModel.IsPrimary = false;
                viewModel.SendDocumentUpdate = true;
                viewModel.ModifiedBy = 0;
            }

            ViewData["Success"] = "In Progress";
            return View(viewModel);

        }

        /// <summary>
        /// Edits the taxonomy level external identifier.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult EditTaxonomyLevelExternalId(EditTaxonomyLevelExternalIdViewModel viewModel)
        {
            try
            {
                TaxonomyLevelExternalIdObjectModel objObjectModel = new TaxonomyLevelExternalIdObjectModel
                {
                    Level = viewModel.SelectedLevelId,
                    TaxonomyId = viewModel.SelectedTaxonomyId,
                    ExternalId = viewModel.ExternalId,
                    IsPrimary = viewModel.IsPrimary,
                    SendDocumentUpdate=viewModel.SendDocumentUpdate
                };
                taxonomyLevelexternalIdCacheFactory.SaveEntity(objObjectModel, SessionUserID);

                viewModel.Level = new List<DisplayValuePair>();
                viewModel.Level.Add(new DisplayValuePair { Value = dropvalue, Display = dropLevel });
                foreach (int key in Enum.GetValues(typeof(TaxonomyLevelEnums)))
                {
                    viewModel.Level.Add(new DisplayValuePair { Display = Enum.GetName(typeof(TaxonomyLevelEnums), key), Value = key.ToString() });
                }

                viewModel.TaxonomyId = new List<DisplayValuePair>();
                viewModel.TaxonomyId.Add(new DisplayValuePair { Value = dropvalue, Display = dropTaxonomy });
                foreach (TaxonomyObjectModel taxonomyObjectModel in taxonomyCacheFactory.GetAllEntities())
                {
                    viewModel.TaxonomyId.Add(new DisplayValuePair { Display = taxonomyObjectModel.TaxonomyName, Value = taxonomyObjectModel.TaxonomyId.ToString() });
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

        #region Disable
        /// <summary>
        /// Disables the specified level identifier.
        /// </summary>
        /// <param name="LevelId">The level identifier.</param>
        /// <param name="TaxonomyId">The taxonomy identifier.</param>
        /// <param name="ExternalId">The external identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult Disable(int LevelId, int TaxonomyId, string ExternalId)
        {
            taxonomyLevelexternalIdCacheFactory.DeleteEntity(new TaxonomyLevelExternalIdObjectModel
            {
                Level = LevelId,
                TaxonomyId = TaxonomyId,
                ExternalId = string.IsNullOrEmpty(ExternalId) ? null : ExternalId
            }, SessionUserID);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CheckTaxonomyLevelExternalAlreadyExists
        /// <summary>
        /// Checks the data already exists.
        /// </summary>
        /// <param name="levelID">The level identifier.</param>
        /// <param name="TaxonomyID">The taxonomy identifier.</param>
        /// <param name="externalID">The external identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckDataAlreadyExists(int? levelID, int? TaxonomyID, string externalID)
        {
            bool isDataExists = false;
            var taxonomyLevelexternalIdCollection = taxonomyLevelexternalIdCacheFactory.GetEntitiesBySearch(new TaxonomyLevelExternalIdSearchDetail() { Level = levelID, TaxonomyId = TaxonomyID, ExternalId = string.IsNullOrEmpty(externalID) ? null : externalID });

            if (taxonomyLevelexternalIdCollection != null && taxonomyLevelexternalIdCollection.Count() > 0)
            {
                isDataExists = true;
            }

            return Json(isDataExists, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CheckExternalIdAlreadyExists
        /// <summary>
        /// Checks the external identifier already exists.
        /// </summary>
        /// <param name="externalID">The external identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckExternalIdAlreadyExists(string externalID)
        {
            bool isDataExists = false;
            var taxonamyLevelexternalIdCollection = taxonomyLevelexternalIdCacheFactory.GetEntitiesBySearch(new TaxonomyLevelExternalIdSearchDetail() { ExternalId = string.IsNullOrEmpty(externalID) ? null : externalID });

            if (taxonamyLevelexternalIdCollection != null && taxonamyLevelexternalIdCollection.Count() > 0)
            {
                isDataExists = true;
            }

            return Json(isDataExists, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}