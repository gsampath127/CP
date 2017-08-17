using RP.Extensions;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Enumerations;
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
using System.Web;
using System.Web.Mvc;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    public class BrowserVersionController : BaseController
    {
        #region Constants
        private const string defaultdropvalue = "-1";
        private const string dropName = "--Please select Name--";
        #endregion
        /// <summary>
        /// The document type external identifier cache factory
        /// </summary>
        
        private IFactoryCache<BrowserVersionFactory, BrowserVersionObjectModel, int> browserVersionCacheFactory;

        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeExternalIdController" /> class.
        /// </summary>
        public BrowserVersionController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                browserVersionCacheFactory = RPV2Resolver.Resolve<IFactoryCache<BrowserVersionFactory, BrowserVersionObjectModel, int>>("BrowserVersion");
                browserVersionCacheFactory.ClientName = SessionClientName;
                browserVersionCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;

            }
        }

        #endregion

        // GET: BrowserVersion
        public ActionResult BrowserVersion()
        {
            ViewData["SelectedCustomer"] = SessionClientName;
            return View();
        }

        #region Load Search Creteria Methods.
        [HttpGet]
        public JsonResult GetBrowserNames()
        {
            return (Json((from dbo in browserVersionCacheFactory.GetAllEntities()
                          select new
                          {
                              Value = dbo.Name,
                              Display = dbo.Name
                          }).Distinct(), JsonRequestBehavior.AllowGet));

        }
        #endregion

        #region GetAllBrowserVersionDetails
        /// <summary>
        /// GetAllBrowserVersionDetails
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="version"></param>
        /// <param name="downloadUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAllBrowserVersionDetails(string Name)
        {
            BrowserVersionSearchDetail objSearchDetail = new BrowserVersionSearchDetail()
            {
                Name = string.IsNullOrEmpty(Name) ? null : Name
            };

            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            BrowserVersionSortColumn SortColumn = BrowserVersionSortColumn.Name;
            switch (kendoGridPost.SortColumn)
            {
                case "Version":
                    SortColumn = BrowserVersionSortColumn.Version;
                    break;
                case "DownloadUrl":
                    SortColumn = BrowserVersionSortColumn.DownloadURL;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            var Result = (from dbo in
                              (from bv in browserVersionCacheFactory.GetEntitiesBySearch(
                                   startRowIndex,
                                   kendoGridPost.PageSize,
                                   objSearchDetail,
                                   new BrowserVersionSortDetail { Column = SortColumn, Order = sortOrder })
                               orderby SortColumn
                               select new
                               {
                                   Id = bv.Id,
                                   Name = bv.Name,
                                   Version = bv.MinimumVersion,
                                   DownLoadUrl = bv.DownloadUrl
                               })
                          orderby (SortColumn)
                          select dbo);
            return Json(new
            {
                total = browserVersionCacheFactory.GetEntitiesBySearch(objSearchDetail).Count(),
                data = from obj in Result
                       select new
                       {
                           Id = obj.Id,
                           Name = obj.Name,
                           Version = obj.Version,
                           DownLoadUrl = obj.DownLoadUrl
                       }
            });
        }
        #endregion

        #region DisableBrowserVersion
        [HttpGet]
        public JsonResult DisableBrowserVersion(int BrowserVersionId)
        {
            browserVersionCacheFactory.DeleteEntity(
                new BrowserVersionObjectModel()
                {
                    Id = BrowserVersionId
                },
                SessionUserID);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region EditBrowserVersion
        [HttpGet]
        [IsPopUp]
        public ActionResult EditBrowserVersion(int BrowserVersionID)
        {
            EditBrowserVersionViewModel EditViewModel = new EditBrowserVersionViewModel();
            EditViewModel.Name = new List<DisplayValuePair>();
            EditViewModel.Name.Add(new DisplayValuePair { Value = defaultdropvalue, Display = dropName });

            List<string> lstExclude = browserVersionCacheFactory.GetAllEntities().Select(x => x.Name).Distinct().ToList();
            var results = lstExclude;

            //// ENUM names - DB names
            //var results = Enum.GetNames(typeof(BrowserVersionEnum)).ToList().Where(i => !lstExclude.Any(e => i.Contains(e)));

            //foreach (string key in results)
            //{
            //    EditViewModel.Name.Add(new DisplayValuePair { Display = key, Value = key });
            //}

            BrowserVersionObjectModel objObjectModel = browserVersionCacheFactory.GetEntitiesBySearch(new BrowserVersionSearchDetail { Id = BrowserVersionID }).FirstOrDefault();

            if (objObjectModel != null)
            {

                EditViewModel.SelectedID = objObjectModel.Id;
                EditViewModel.SelectedName = objObjectModel.Name;
                EditViewModel.Version = objObjectModel.MinimumVersion;
                EditViewModel.DownloadURL = objObjectModel.DownloadUrl;

                EditViewModel.ModifiedDate = objObjectModel.LastModified.ToString();
                EditViewModel.ModifiedBy = objObjectModel.ModifiedBy;
                if (objObjectModel.ModifiedBy != 0)
                {
                    EditViewModel.ModifiedByName = objObjectModel.ModifiedBy.ToString();
                    if (userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = objObjectModel.ModifiedBy }).Count() > 0)
                    {
                        EditViewModel.ModifiedByName = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = objObjectModel.ModifiedBy }).FirstOrDefault().UserName;
                    }
                }
                else
                {
                    EditViewModel.ModifiedByName = string.Empty;
                }
            }
            else
            {
                //// ENUM names - DB names
                List<string> addNames = Enum.GetNames(typeof(BrowserVersionEnum)).ToList().Where(i => !lstExclude.Any(e => i.Contains(e))).ToList();
                results = addNames;

                EditViewModel.SelectedID = 0;
                //EditViewModel.SelectedName = null;
                EditViewModel.Version = null;
                EditViewModel.DownloadURL = string.Empty;
            }

            foreach (string key in results)
            {
                EditViewModel.Name.Add(new DisplayValuePair { Display = key, Value = key });
            }

            ViewData["Success"] = "In Progress";
            return View(EditViewModel);
        }

        [HttpPost]
        [IsPopUp]
        public ActionResult EditBrowserVersion(EditBrowserVersionViewModel editViewModel)
        {
            try
            {
                BrowserVersionObjectModel objObjectModel = new BrowserVersionObjectModel()
                {
                    SelectedId=editViewModel.SelectedID,
                    Name = editViewModel.SelectedName,
                    MinimumVersion =  Convert.ToInt32(editViewModel.Version),
                    DownloadUrl = editViewModel.DownloadURL
                };
                browserVersionCacheFactory.SaveEntity(objObjectModel, SessionUserID);

                editViewModel.Name = new List<DisplayValuePair>();
                editViewModel.Name.Add(new DisplayValuePair { Value = defaultdropvalue, Display = dropName });

                List<string> lstName = browserVersionCacheFactory.GetAllEntities().Select(x => x.Name).Distinct().ToList();

                foreach (var item in lstName)
                {
                    editViewModel.Name.Add(new DisplayValuePair()
                    {
                        Display = item,
                        Value = item
                    });
                }

                ViewData["Success"] = "OK";
            }
            catch (Exception e)
            {
                editViewModel.SuccessOrFailedMessage = e.Message;
            }
            return View(editViewModel);
        }

        #endregion
    }
}