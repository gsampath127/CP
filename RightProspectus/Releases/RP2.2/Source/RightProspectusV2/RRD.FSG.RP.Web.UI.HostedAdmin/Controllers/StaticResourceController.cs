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
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// To Hold the Static Resource Controller related actions
    /// </summary>
    public class StaticResourceController : BaseController
    {

        /// <summary>
        /// The hosted engine URL
        /// </summary>
        private const string HostedEngineURL = "HostedEngineURL";
        /// <summary>
        /// The static resource cache factory
        /// </summary>
        private IFactoryCache<StaticResourceFactory, StaticResourceObjectModel, int> staticResourceCacheFactory;
        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticResourceController" /> class.
        /// </summary>
        public StaticResourceController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                this.staticResourceCacheFactory = RPV2Resolver.Resolve<IFactoryCache<StaticResourceFactory, StaticResourceObjectModel, int>>("StaticResource");
                this.staticResourceCacheFactory.ClientName = SessionClientName;
                this.staticResourceCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;
            }
        }

        #region Constructor_Test
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticResourceController"/> class.
        /// </summary>
        /// <param name="StaticResourceCacheFactory">The static resource cache factory.</param>
        /// <param name="UserCacheFactory">The user cache factory.</param>
        public StaticResourceController(IFactoryCache<StaticResourceFactory, StaticResourceObjectModel, int> StaticResourceCacheFactory,
            IFactoryCache<UserFactory, UserObjectModel, int> UserCacheFactory)
        {
            staticResourceCacheFactory = StaticResourceCacheFactory;
            staticResourceCacheFactory.ClientName = SessionClientName;
            staticResourceCacheFactory.Mode = FactoryCacheMode.All;

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

        /// <summary>
        /// Deletes the static resource.
        /// </summary>
        /// <param name="staticResourceId">The static resource identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DeleteStaticResource(int staticResourceId)
        {
            staticResourceCacheFactory.DeleteEntity(staticResourceId, SessionUserID);
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the static resource.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="MimeType">Type of the MIME.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetStaticResource(string FileName, string MimeType)
        {
            if (string.IsNullOrWhiteSpace(FileName))
                FileName = null;
            if (string.IsNullOrWhiteSpace(MimeType))
                MimeType = null;

            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            StaticResourceSortColumn SortColumn = StaticResourceSortColumn.Key;
            switch (kendoGridPost.SortColumn)
            {
                case "FileName":
                    SortColumn = StaticResourceSortColumn.FileName;
                    break;
                case "MimeType":
                    SortColumn = StaticResourceSortColumn.MimeType;
                    break;
                case "ModifiedDate":
                    SortColumn = StaticResourceSortColumn.LastModified;
                    break;
            }
            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
                sortOrder = SortOrder.Descending;

            return Json(new
            {
                total = staticResourceCacheFactory.GetEntitiesBySearch(new StaticResourceSearchDetail() { FileName = FileName, MimeType = MimeType }).Select(p => p.StaticResourceId).Count(),
                data = (from staticResourceObjectModel in staticResourceCacheFactory.GetEntitiesBySearch(
                           startRowIndex,
                           kendoGridPost.PageSize,
                           new StaticResourceSearchDetail() { FileName = FileName, MimeType = MimeType },
                           new StaticResourceSortDetail() { Column = SortColumn, Order = sortOrder })
                        select new
                        {
                            StaticResourceId = staticResourceObjectModel.StaticResourceId.ToString(),
                            FileName = staticResourceObjectModel.FileName,
                            MimeType = staticResourceObjectModel.MimeType,
                            ImageURL = string.Format("staticresource/{0}?client={1}", staticResourceObjectModel.FileName, SessionClientName),
                            StaticResourceURL = string.Format("{0}staticresource/{1}?client={2}", ConfigurationManager.AppSettings.Get(HostedEngineURL), staticResourceObjectModel.FileName, SessionClientName)
                        })
            });
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetFileName()
        {
            return Json(
                (from dbo in staticResourceCacheFactory.GetAllEntities()
                 select new { Display = dbo.FileName, Value = dbo.FileName }).Distinct().OrderBy(obj => obj.Display),
                JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the type of the MIME.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetMimeType()
        {
            return Json((from dbo in staticResourceCacheFactory.GetAllEntities()
                         select new { Display = dbo.MimeType, Value = dbo.MimeType }).Distinct().OrderBy(obj => obj.Display),
                JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the modified date.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetModifiedDate()
        {
            return Json((from dbo in staticResourceCacheFactory.GetAllEntities()
                         select new { Display = dbo.LastModified.ToString(), Value = dbo.LastModified.ToString() }).Distinct().OrderBy(obj => obj.Display),
                JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Edits the static resources.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult EditStaticResources(int id)
        {
            EditStaticResourceViewModel objEditStaticResourceViewModel = new EditStaticResourceViewModel();
            if (id > 0)
            {
                StaticResourceObjectModel staticResourceDetails = staticResourceCacheFactory.GetEntityByKey(id);
                objEditStaticResourceViewModel.FileName = staticResourceDetails.FileName.ToString();
                objEditStaticResourceViewModel.StaticResourceId = staticResourceDetails.StaticResourceId.ToString();
                objEditStaticResourceViewModel.UTCLastModifiedDate = staticResourceDetails.LastModified;
                if (staticResourceDetails.ModifiedBy != 0)
                {
                    objEditStaticResourceViewModel.ModifiedByName = staticResourceDetails.ModifiedBy.ToString();
                    if (userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = staticResourceDetails.ModifiedBy }).Count() > 0)
                    {
                        objEditStaticResourceViewModel.ModifiedByName = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = staticResourceDetails.ModifiedBy }).FirstOrDefault().UserName;
                    }
                }
                else
                {
                    objEditStaticResourceViewModel.ModifiedByName = string.Empty;
                }
            }
            else
            {
                objEditStaticResourceViewModel.StaticResourceId = id.ToString();
            }
            ViewData["Success"] = "In-Progress";
            return View(objEditStaticResourceViewModel);
        }

        /// <summary>
        /// Edits the static resources.
        /// </summary>
        /// <param name="uploadFile">The upload file.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult EditStaticResources(HttpPostedFileBase uploadFile)
        {
            string id = Request.Form["StaticResourceId"];
            StaticResourceObjectModel objStaticResourceObjectModel = new StaticResourceObjectModel();
            EditStaticResourceViewModel objEditStaticResourceViewModel = new EditStaticResourceViewModel();
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(uploadFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Import/Create"), fileName);
                uploadFile.SaveAs(path);

                objStaticResourceObjectModel.FileName = objEditStaticResourceViewModel.FileName = Request.Form["FileName"];
                objStaticResourceObjectModel.StaticResourceId = Convert.ToInt32(id);
                objStaticResourceObjectModel.Data = System.IO.File.ReadAllBytes(path);
                objStaticResourceObjectModel.MimeType = uploadFile.ContentType;
                objStaticResourceObjectModel.Size = uploadFile.ContentLength;
                staticResourceCacheFactory.SaveEntity(objStaticResourceObjectModel, SessionUserID);

                System.IO.File.Delete(path);
                objEditStaticResourceViewModel.StaticResourceId = id;
                ViewData["Success"] = "OK";
            }
            return View(objEditStaticResourceViewModel);
        }

        /// <summary>
        /// Checks the name of the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckFileName(string fileName)
        {
            return Json(staticResourceCacheFactory.GetEntitiesBySearch(new StaticResourceSearchDetail { FileName = fileName }).Count() > 0,
                JsonRequestBehavior.AllowGet);
        }
    }
}