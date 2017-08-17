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
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// Class UrlRewriteController.
    /// </summary>
    public class UrlRewriteController : BaseController
    {
        #region Constants
        ///<summary>
        ///Constants
        ///</summary>
        private const string defaultvalue = "-1";
        private const string defaultPatternName = "--Please select Pattern Name--";
        #endregion

        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        /// <summary>
        /// The Url Rewrite cache factory
        /// </summary>
        private IFactoryCache<UrlRewriteFactory, UrlRewriteObjectModel, int> urlRewriteCacheFactory;

        #region UrlRewriteController
        /// <summary>
        /// The default constructor which will resolve the dependency using RPResolver
        /// </summary>
        public UrlRewriteController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                urlRewriteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UrlRewriteFactory, UrlRewriteObjectModel, int>>("UrlRewrite");
                urlRewriteCacheFactory.ClientName = SessionClientName;
                urlRewriteCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;
            }
        }
        #endregion

        #region UrlRewriteController_Test_Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlRewriteController"/> class.
        /// </summary>
        /// <param name="UserCacheFactory">The user cache factory</param>
        /// <param name="UrlRewriteCacheFactory">The Urlrewrite cache factory</param>
        public UrlRewriteController(IFactoryCache<UserFactory, UserObjectModel, int> UserCacheFactory,
            IFactoryCache<UrlRewriteFactory, UrlRewriteObjectModel, int> UrlRewriteCacheFactory)
        {
            urlRewriteCacheFactory = UrlRewriteCacheFactory;
            urlRewriteCacheFactory.ClientName = SessionClientName;
            urlRewriteCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = UserCacheFactory;
            userCacheFactory.Mode = FactoryCacheMode.All;
        }

        #endregion

        #region ListView
        /// <summary>
        /// Displays the view "List"
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult List()
        {
            ViewData["SelectedCustomer"] = SessionClientName;
            return View();
        }
        #endregion

        #region GetURLRewriteDetails
        /// <summary>
        /// Gets Url Rewrite details.
        /// </summary>
        /// <param name="patternName">Name of the pattern.</param>
        /// <returns>JsonResult.</returns>

        [HttpPost]
        public JsonResult Search(string patternName)
        {
            if (string.IsNullOrWhiteSpace(patternName))
                patternName = null;

            var kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            UrlRewriteSortColumn SortColumn = UrlRewriteSortColumn.PatternName;
            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            return Json(
                new
                {
                    total = urlRewriteCacheFactory.GetEntitiesBySearch(new UrlRewriteSearchDetail() { PatternName = patternName }).Select(p => p.UrlRewriteId).Count(),
                    data = from urlRewriteObjectModel in urlRewriteCacheFactory.GetEntitiesBySearch(
                               startRowIndex,
                               kendoGridPost.PageSize,
                               new UrlRewriteSearchDetail() { PatternName = patternName },
                               new UrlRewriteSortDetail() { Column = SortColumn, Order = sortOrder })
                           select new
                           {
                               PatternName = urlRewriteObjectModel.PatternName,
                               UrlRewriteId = urlRewriteObjectModel.UrlRewriteId
                           }
                });
        }
        #endregion

        #region Load Search Criteria
        /// <summary>
        /// Gets the Pattern Names.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetPatternName()
        {
            return Json((from dbo in urlRewriteCacheFactory.GetAllEntities()
                         select new
                         {
                             Display = dbo.PatternName,
                             Value = dbo.PatternName
                         }).Distinct().OrderBy(obj => obj.Display), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ActionMethod For Edit
        /// <summary>
        /// Edits URL Rewrite.
        /// </summary>
        /// <param name="urlRewriteID">The URL rewrite identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult Edit(int urlRewriteID)
        {
            EditUrlRewriteViewModel viewModel = new EditUrlRewriteViewModel();
            if (urlRewriteID > 0)
            {
                UrlRewriteObjectModel urlRewriteObjectModel = urlRewriteCacheFactory.GetEntityByKey(urlRewriteID);
                if (urlRewriteObjectModel != null)
                {
                    viewModel.PatternName = urlRewriteObjectModel.PatternName;
                    viewModel.MatchPattern = urlRewriteObjectModel.MatchPattern;
                    viewModel.RewriteFormat = urlRewriteObjectModel.RewriteFormat;
                    viewModel.UrlRewriteId = urlRewriteObjectModel.UrlRewriteId;
                    if (urlRewriteObjectModel.ModifiedBy != 0)
                    {
                        viewModel.ModifiedByName = urlRewriteObjectModel.ModifiedBy.ToString();
                        if (userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = urlRewriteObjectModel.ModifiedBy }).Count() > 0)
                        {
                            viewModel.ModifiedByName = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = urlRewriteObjectModel.ModifiedBy }).FirstOrDefault().UserName;
                        }
                    }
                    else
                    {
                        viewModel.ModifiedByName = string.Empty;
                    }
                    viewModel.UTCLastModifiedDate = urlRewriteObjectModel.LastModified;
                }
            }
            ViewData["Success"] = "In Progress";
            return View(viewModel);
        }
        /// <summary>
        /// Edits the UrlRewrite.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult Edit(EditUrlRewriteViewModel viewModel)
        {
            try
            {
                UrlRewriteObjectModel urlRewriteObjectModel = new UrlRewriteObjectModel
                {
                    PatternName = viewModel.PatternName,
                    MatchPattern = viewModel.MatchPattern,
                    RewriteFormat = viewModel.RewriteFormat,
                    UrlRewriteId = viewModel.UrlRewriteId
                };
                urlRewriteCacheFactory.SaveEntity(urlRewriteObjectModel, SessionUserID);

                ViewData["Success"] = "OK";
            }
            catch
            {
                //TODO: Add viewModel.SuccessOrFailedMessage = exception.Message;
            }
            return View(viewModel);
        }

        #endregion

        #region Disable URL Rewrite
        /// <summary>
        /// Disable Url rewrite
        /// </summary>
        /// <param name="urlRewriteId">The URL rewrite identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult Disable(int urlRewriteId)
        {
            urlRewriteCacheFactory.DeleteEntity(urlRewriteId, SessionUserID);
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PatterName
        /// <summary>
        /// Checks the Pattern Name already exists.
        /// </summary>
        /// <param name="patternName">Name of the patternName.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckPatternName(string patternName)
        {
            return Json((from dbo in urlRewriteCacheFactory.GetAllEntities()
                         where dbo.PatternName == patternName
                         select dbo.PatternName).Distinct().Any(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}