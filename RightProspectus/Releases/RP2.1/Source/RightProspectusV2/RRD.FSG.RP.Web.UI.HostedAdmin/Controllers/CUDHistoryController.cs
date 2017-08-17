// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 11-17-2015
// ***********************************************************************

using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces;
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
    /// Class CUDHistoryController.
    /// </summary>
    public class CUDHistoryController : BaseController
    {
        /// <summary>
        /// The CUDHistory cache factory
        /// </summary>
        private IFactory<CUDHistoryObjectModel, CUDHistoryKey> cudHistoryFactory;

        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        /// <summary>
        /// IsAdmin
        /// </summary>        
        public bool IsAdmin { get; internal set; }



        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CUHistoryController" /> class.
        /// </summary>
        public CUDHistoryController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                cudHistoryFactory = RPV2Resolver.Resolve<IFactory<CUDHistoryObjectModel, CUDHistoryKey>>("CUDHistory");
                cudHistoryFactory.ClientName = SessionClientName;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;

                IsAdmin = checkIsAdmin();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CUDHistoryController" /> class.
        /// </summary>
        /// <param name="cudHistoryFactoryMock">The cud history factory mock.</param>
        /// <param name="mockuserCacheFactory">The mockuser cache factory.</param>
        public CUDHistoryController(IFactory<CUDHistoryObjectModel, CUDHistoryKey> cudHistoryFactoryMock, IFactoryCache<UserFactory, UserObjectModel, int> mockuserCacheFactory)
        {
            cudHistoryFactory = cudHistoryFactoryMock;
            cudHistoryFactory.ClientName = SessionClientName;

            userCacheFactory = mockuserCacheFactory;
            userCacheFactory.ClientName = SessionClientName;
            IsAdmin = checkIsAdmin();
        }

        #endregion

        // GET: CUDHistory
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            ViewData["SelectedCustomer"] = SessionClientName;
            return View("CUDHistoryView");
        }

        #region CheckAdmin
        /// <summary>
        /// Sets IsAdminProperty
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>

        public bool checkIsAdmin()
        {
            return userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = SessionUserID }).FirstOrDefault().Roles.Exists(p => p.RoleName == "Admin");
        }

        #endregion


        #region LoadSearchCriteria Methods
        /// <summary>
        /// Gets the Table Names
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetTableNames()
        {
            if (IsAdmin)
            {
                return Json((from dbo in cudHistoryFactory.GetAllEntities() select new { Display = dbo.TableName, Value = dbo.TableName }).Distinct(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json((from dbo in cudHistoryFactory.GetAllEntities() where dbo.UserId == SessionUserID select new { Display = dbo.TableName, Value = dbo.TableName }).Distinct(), JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// Gets the CUDType
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetCUDType()
        {
            return Json(from obj in GetCUDHistoryDictionary() select new { Display = obj.Value, Value = obj.Key }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the UserName
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetUserName()
        {
            IEnumerable<UserObjectModel> userDetails = userCacheFactory.GetAllEntities();
            if (IsAdmin)
            {
                return Json((from dbo in cudHistoryFactory.GetAllEntities()
                             select new
                             {
                                 Display = userDetails.FirstOrDefault(t => t.UserId == dbo.UserId) != null ? userDetails.FirstOrDefault(t => t.UserId == dbo.UserId).UserName : dbo.UserId.ToString(),
                                 Value = dbo.UserId.ToString()
                             }).Distinct(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json((from dbo in cudHistoryFactory.GetAllEntities()
                             where dbo.UserId == SessionUserID
                             select new
                             {
                                 Display = userDetails.FirstOrDefault(t => t.UserId == SessionUserID).UserName,
                                 Value = SessionUserID.ToString()
                             }).Distinct(), JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region GetAllCUDHistoryDetails_Returns_JsonResult

        /// <summary>
        /// Gets all cud history details.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="cudType">Type of the cud.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="userId">User Id.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllCUDHistoryDetails(string tableName, string cudType, DateTime? fromDate, DateTime? toDate, string userId)
        {
            IEnumerable<UserObjectModel> userDetails = userCacheFactory.GetAllEntities();

            int startRowIndex = 1;
            int pageSize = 10;
            KendoGridPost kendoGridPost = new KendoGridPost();
            startRowIndex = kendoGridPost.Page;
            pageSize = kendoGridPost.PageSize;

            CUDHistorySortColumn sortColumn = CUDHistorySortColumn.TableName;
            switch (kendoGridPost.SortColumn)
            {
                case "TableName":
                    sortColumn = CUDHistorySortColumn.TableName;
                    break;
                case "CUDType":
                    sortColumn = CUDHistorySortColumn.CUDType;
                    break;
                case "UtcCUDDate":
                    sortColumn = CUDHistorySortColumn.UtcCUDDate;
                    break;
            }
            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }
            int totalRecords;
            int id;
            IEnumerable<CUDHistoryObjectModel> enumCUDHistoryObjectModels = cudHistoryFactory.GetEntitiesBySearch(
                startRowIndex,
                pageSize,
                new CUDHistorySearchDetail()
                {
                    TableName = string.IsNullOrWhiteSpace(tableName) ? null : tableName,
                    CUDType = string.IsNullOrWhiteSpace(cudType) ? null : cudType,
                    UtcCUDDateFrom = fromDate,
                    UtcCUDDateTo = toDate,
                    IsHistoryData = false,
                    UserId = (!string.IsNullOrEmpty(userId)) ? (int.TryParse(userId, out id) ? id : -1) : (IsAdmin) ? (int?)null : SessionUserID
                },
                new CUDHistorySortDetail { Column = sortColumn, Order = sortOrder },
                out totalRecords
                );

            return Json(new
            {
                total = totalRecords,
                data = from obj in enumCUDHistoryObjectModels
                       select new
                       {
                           CUDHistoryId = obj.CUDHistoryId,
                           TableName = obj.TableName,
                           CUDType = GetCUDHistoryDictionary()[obj.CUDType],
                           UtcCUDDate = obj.UtcCUDDate.ToString(),
                           UserName = userDetails.FirstOrDefault(t => t.UserId == obj.UserId) != null ? userDetails.FirstOrDefault(t => t.UserId == obj.UserId).UserName : obj.UserId.ToString()
                       }
            });
        }

        /// <summary>
        /// Gets all cud history data details.
        /// </summary>
        /// <param name="cudHistoryId">The cud history identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllCUDHistoryDataDetails(int? cudHistoryId)
        {

            KendoGridPost kendoGridPost = new KendoGridPost();
            CUDHistorySortColumn sortColumn = CUDHistorySortColumn.ColumnName;
            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }
            int totalRecords;
            List<CUDHistoryViewModel> lstCudHistoryViewObjectModels = new List<CUDHistoryViewModel>();

            //TODO: Remove paging from CUDHistory Detail table, removes hardcoded maximumrows of 100 
            cudHistoryFactory.GetEntitiesBySearch(
                1,
                100,
                new CUDHistorySearchDetail() { CUDHistoryId = cudHistoryId, IsHistoryData = true },
                new CUDHistorySortDetail { Column = sortColumn, Order = sortOrder },
                out totalRecords)
                .Select(m => new 
                { 
                    m.CUDHistoryId, 
                    m.ColumnName, 
                    m.SqlDbType, 
                    m.OldValue, 
                    m.NewValue, 
                    m.IsBinaryImage, 
                    m.NewImageDataURL, 
                    m.OldImageDataURL 
                })
                .ForEach(obj => lstCudHistoryViewObjectModels.Add(new CUDHistoryViewModel 
                { 
                    CUDHistoryId = obj.CUDHistoryId, 
                    ColumnName = obj.ColumnName, 
                    SqlDbType = obj.SqlDbType, 
                    OldValue = obj.OldValue, 
                    NewValue = obj.NewValue, 
                    IsBinaryImage = obj.IsBinaryImage, 
                    NewImageDataURL = obj.NewImageDataURL, 
                    OldImageDataURL = obj.OldImageDataURL 
                }));

            return Json(new { total = totalRecords, data = lstCudHistoryViewObjectModels });
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Gets the CUD history actions dictionary
        /// </summary>
        /// <returns>Dictionary list</returns>
        private Dictionary<string, string> GetCUDHistoryDictionary()
        {
            Dictionary<string, string> dictCUDType = new Dictionary<string, string>();
            dictCUDType.Add("U", "Update");
            dictCUDType.Add("D", "Delete");
            dictCUDType.Add("I", "Insert");

            return dictCUDType;
        }
        #endregion
    }
}