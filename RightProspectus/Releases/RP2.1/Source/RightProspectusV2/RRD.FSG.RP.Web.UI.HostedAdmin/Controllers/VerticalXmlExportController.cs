// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By :
// Last Modified On : 11-16-2015
// ***********************************************************************

using RP.Extensions;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.HostedAdmin.Common;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// Class VerticalXmlExportController.
    /// </summary>
    public class VerticalXmlExportController : BaseController
    {
        /// <summary>
        /// Enum JobStatus
        /// </summary>
        private enum JobStatus
        {
            /// <summary>
            /// The not started
            /// </summary>
            NotStarted = 0,
            /// <summary>
            /// The in progress
            /// </summary>
            InProgress = 1,
            /// <summary>
            /// The export completed
            /// </summary>
            ExportCompleted = 2,
            /// <summary>
            /// The error
            /// </summary>
            Error = -1
        };

        /// <summary>
        /// User cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;

        /// <summary>
        /// VerticalXmlExport cache factory
        /// </summary>
        private IFactoryCache<VerticalXmlExportFactory, VerticalXmlExportObjectModel, int> verticalXmlExportCacheFactory;

        /// <summary>
        /// VerticalXmlImport factory
        /// </summary>
        private IFactory<VerticalXmlExportObjectModel, int> verticalXmlExportFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalXmlExportController"/> class.
        /// </summary>
        public VerticalXmlExportController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;

                verticalXmlExportCacheFactory = RPV2Resolver.Resolve<IFactoryCache<VerticalXmlExportFactory, VerticalXmlExportObjectModel, int>>("VerticalXmlExport");
                verticalXmlExportCacheFactory.ClientName = SessionClientName;
                verticalXmlExportCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;

                verticalXmlExportFactory = RPV2Resolver.Resolve<IFactory<VerticalXmlExportObjectModel, int>>("VerticalXmlExportFactory");
                verticalXmlExportFactory.ClientName = SessionClientName;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalXmlExportController"/> class.
        /// </summary>
        /// <param name="UserFactoryCache">The user factory cache.</param>
        /// <param name="VerticalXmlExportFactoryCache">The vertical XML export factory cache.</param>
        /// <param name="VerticalXmlExportfactory">The vertical XML exportfactory.</param>
        public VerticalXmlExportController(IFactoryCache<UserFactory, UserObjectModel, int> UserFactoryCache,
          IFactoryCache<VerticalXmlExportFactory, VerticalXmlExportObjectModel, int> VerticalXmlExportFactoryCache,
            IFactory<VerticalXmlExportObjectModel, int> VerticalXmlExportfactory)
        {
            userCacheFactory = UserFactoryCache;
            userCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;

            verticalXmlExportCacheFactory = VerticalXmlExportFactoryCache;
            verticalXmlExportCacheFactory.ClientName = SessionClientName;
            verticalXmlExportCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;

            verticalXmlExportFactory = VerticalXmlExportfactory;
            verticalXmlExportFactory.ClientName = SessionClientName;
        }

        /// <summary>
        /// List the Jobs
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult List()
        {
            ViewData["SelectedCustomer"] = SessionClientName;
            return View();
        }

        /// <summary>
        /// Gets the Users for ExportedBy dropdown.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [OutputCache(Duration = 0, VaryByParam = "*")]
        [HttpGet]
        public JsonResult GetUsers()
        {
            IEnumerable<int> exportUserIDs = verticalXmlExportCacheFactory.GetAllEntities().Select(x => x.ExportedBy).Distinct();

            List<DisplayValuePair> lstDisplayValuePair = new List<DisplayValuePair>();
            foreach (int userID in exportUserIDs)
            {
                UserObjectModel userDetails = userCacheFactory.GetEntityByKey(userID);
                lstDisplayValuePair.Add(new DisplayValuePair { Display = userDetails.FirstName + " " + userDetails.LastName, Value = userDetails.UserId.ToString() });
            }
            return Json(lstDisplayValuePair, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Searches the VerticalXMLExport Jobs.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult Search(DateTime? fromDate, DateTime? toDate, string userID)
        {
            int i;
            VerticalXmlExportSearchDetail objSearchDetail = new VerticalXmlExportSearchDetail()
            {
                FromExportDate = fromDate,
                ToExportDate = toDate,
                ExportedBy = !(string.IsNullOrEmpty(userID)) ? (int.TryParse(userID, out i) ? i : 0) : (int?)null
            };
            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            VerticalXmlExportSortColumn SortColumn = VerticalXmlExportSortColumn.VerticalXmlExportId;
            switch (kendoGridPost.SortColumn)
            {
                case "ExportDate":
                    SortColumn = VerticalXmlExportSortColumn.ExportDate;
                    break;
                case "ExportDescription":
                    SortColumn = VerticalXmlExportSortColumn.ExportDescription;
                    break;
                case "ExportedByName":
                    SortColumn = VerticalXmlExportSortColumn.ExportedByName;
                    break;
                case "Status":
                    SortColumn = VerticalXmlExportSortColumn.Status;
                    break;
            }

            SortOrder sortOrder = SortOrder.Descending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("asc"))
            {
                sortOrder = SortOrder.Ascending;
            }

            int totalRecords = verticalXmlExportCacheFactory.GetEntitiesBySearch(0, 0, objSearchDetail).Select(p => p.VerticalXmlExportId).Count();

            IEnumerable<VerticalXmlExportObjectModel> verticalXmlImportJobs = verticalXmlExportCacheFactory.GetEntitiesBySearch(startRowIndex, kendoGridPost.PageSize,
                                                                                                        objSearchDetail,
                                                                                                        new VerticalXmlExportSortDetail() { Column = SortColumn, Order = sortOrder });


            List<VerticalXmlExportViewModel> lstVerticalXmlExportJobs = new List<VerticalXmlExportViewModel>();
            foreach (VerticalXmlExportObjectModel exportObjModel in verticalXmlImportJobs)
            {
                lstVerticalXmlExportJobs.Add(new VerticalXmlExportViewModel()
                {
                    ExportDate = exportObjModel.ExportDate.ToString("MM/dd/yyyy hh:mm:ss"),
                    ExportDescription = exportObjModel.ExportDescription,
                    ExportedBy = exportObjModel.ExportedBy,
                    ExportedByName = exportObjModel.ExportedByName,
                    ExportTypes = exportObjModel.ExportTypes,
                    ExportXml = exportObjModel.ExportXml,
                    Status = GetStatus(exportObjModel.Status),
                    StatusID = exportObjModel.Status,
                    VerticalXmlExportId = exportObjModel.VerticalXmlExportId
                });
            }

            return Json(new { total = totalRecords, data = lstVerticalXmlExportJobs });
        }

        /// <summary>
        /// Get ExportedXml.
        /// </summary>
        /// <param name="ExportXmlID">The export XML identifier.</param>
        /// <returns>FileContentResult.</returns>
        [HttpGet]
        public FileContentResult GetExportedXml(string ExportXmlID)
        {
            string exportXml = verticalXmlExportFactory.GetEntityByKey(Convert.ToInt32(ExportXmlID)).ExportXml;

            if (!String.IsNullOrEmpty(exportXml))
            {
                var mimeType = "application/xml";

                byte[] fileContent = Encoding.ASCII.GetBytes(exportXml);
                return File(fileContent, mimeType, "export.xml");

            }


            return null;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>System.String.</returns>
        private string GetStatus(int status)
        {
            switch (status)
            {
                case (int)JobStatus.NotStarted:
                    return "Not Started";
                case (int)JobStatus.InProgress:
                    return "In Progress";
                case (int)JobStatus.ExportCompleted:
                    return "Export Completed";
                default:
                    return "Error";
            }
        }

        /// <summary>
        /// Add new Vertical Xml Export Job.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult Add()
        {
            AddVerticalXmlExportViewModel viewModel = new AddVerticalXmlExportViewModel();
            viewModel.InProgressJobCount = verticalXmlExportCacheFactory.GetAllEntities().Where(x => x.Status == (int)JobStatus.NotStarted || x.Status == (int)JobStatus.InProgress && x.ExportedBy == SessionUserID).Count();
            viewModel.ExportDescription = "Data Export on " + DateTime.Now.ToShortDateString();
            ViewData["Success"] = "In Progress";
            return View(viewModel);
        }

        /// <summary>
        /// Add new Vertical Xml Import Job.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult Add(AddVerticalXmlExportViewModel viewModel)
        {
            VerticalXmlExportObjectModel objModel = new VerticalXmlExportObjectModel();
            if (viewModel != null)
            {
                try
                {
                    viewModel.InProgressJobCount = verticalXmlExportCacheFactory.GetAllEntities().Where(x => x.Status == (int)JobStatus.NotStarted || x.Status == (int)JobStatus.InProgress && x.ExportedBy == SessionUserID).Count();
                    objModel.ExportDescription = viewModel.ExportDescription;
                    objModel.ExportedBy = SessionUserID;
                    objModel.ExportTypes = 1;
                    objModel.ExportXml = "<xml>backup xml not ready</xml>";
                    verticalXmlExportCacheFactory.SaveEntity(objModel);
                    ViewData["Success"] = "OK";
                }
                catch
                {
                    ViewData["Success"] = "Error";
                }
            }
            return View(viewModel);
        }
    }
}