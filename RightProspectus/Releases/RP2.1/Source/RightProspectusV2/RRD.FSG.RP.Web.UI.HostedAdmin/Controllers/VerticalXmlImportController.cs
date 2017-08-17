// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************

using RP.Extensions;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.HostedAdmin.Common;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Schema;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// Class VerticalXmlImportController.
    /// </summary>
    public class VerticalXmlImportController : BaseController
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
            /// The backup completed
            /// </summary>
            BackupCompleted = 2,
            /// <summary>
            /// The import completed
            /// </summary>
            ImportCompleted = 3,
            /// <summary>
            /// The roll back not started
            /// </summary>
            RollBackNotStarted = 4,
            /// <summary>
            /// The roll back in progress
            /// </summary>
            RollBackInProgress = 5,
            /// <summary>
            /// The roll back completed
            /// </summary>
            RollBackCompleted = 6,
            /// <summary>
            /// The error
            /// </summary>
            Error = -1,
            /// <summary>
            /// The error
            /// </summary>
            ImportCompletedMarketIdsMissing = 7
        };
        /// <summary>
        /// User cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;

        /// <summary>
        /// VerticalXmlImport cache factory
        /// </summary>
        private IFactoryCache<VerticalXmlImportFactory, VerticalXmlImportObjectModel, int> verticalXmlImportCacheFactory;

        /// <summary>
        /// VerticalXmlImport factory
        /// </summary>
        private IFactory<VerticalXmlImportObjectModel, int> verticalXmlImportFactory;

        /// <summary>
        /// VerticalXmlExport cache factory
        /// </summary>
        private IFactoryCache<VerticalXmlExportFactory, VerticalXmlExportObjectModel, int> verticalXmlExportCacheFactory;

        /// <summary>
        /// VerticalXmlImport factory
        /// </summary>
        private IFactory<VerticalXmlExportObjectModel, int> verticalXmlExportFactory;

        /// <summary>
        /// ErrorLog factory
        /// </summary>
        private IFactory<ErrorLogObjectModel, int> errorLogFactory;


        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalXmlImportController"/> class.
        /// </summary>
        public VerticalXmlImportController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;

                verticalXmlImportCacheFactory = RPV2Resolver.Resolve<IFactoryCache<VerticalXmlImportFactory, VerticalXmlImportObjectModel, int>>("VerticalXmlImport");
                verticalXmlImportCacheFactory.ClientName = SessionClientName;
                verticalXmlImportCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;

                verticalXmlImportFactory = RPV2Resolver.Resolve<IFactory<VerticalXmlImportObjectModel, int>>("VerticalXmlImportFactory");
                verticalXmlImportFactory.ClientName = SessionClientName;

                verticalXmlExportCacheFactory = RPV2Resolver.Resolve<IFactoryCache<VerticalXmlExportFactory, VerticalXmlExportObjectModel, int>>("VerticalXmlExport");
                verticalXmlExportCacheFactory.ClientName = SessionClientName;
                verticalXmlExportCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;

                verticalXmlExportFactory = RPV2Resolver.Resolve<IFactory<VerticalXmlExportObjectModel, int>>("VerticalXmlExportFactory");
                verticalXmlExportFactory.ClientName = SessionClientName;

                errorLogFactory = RPV2Resolver.Resolve<IFactory<ErrorLogObjectModel, int>>("ErrorLogFactory");
                errorLogFactory.ClientName = SessionClientName;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalXmlImportController"/> class.
        /// </summary>
        /// <param name="Userfactorycache">The userfactorycache.</param>
        /// <param name="VerticalXmlImportFactoryCache">The vertical XML import factory cache.</param>
        /// <param name="VerticalXmlImportFactory">The vertical XML import factory.</param>
        /// <param name="VerticalXmlExportFactoryCache">The vertical XML export factory cache.</param>
        /// <param name="VerticalXmlExportFactory">The vertical XML export factory.</param>
        /// <param name="ErrorLogFactory">The error log factory.</param>
        public VerticalXmlImportController(IFactoryCache<UserFactory, UserObjectModel, int> Userfactorycache,
        IFactoryCache<VerticalXmlImportFactory, VerticalXmlImportObjectModel, int> VerticalXmlImportFactoryCache,
        IFactory<VerticalXmlImportObjectModel, int> VerticalXmlImportFactory,
        IFactoryCache<VerticalXmlExportFactory, VerticalXmlExportObjectModel, int> VerticalXmlExportFactoryCache,
        IFactory<VerticalXmlExportObjectModel, int> VerticalXmlExportFactory,
        IFactory<ErrorLogObjectModel, int> ErrorLogFactory)
        {
            userCacheFactory = Userfactorycache;
            userCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;

            verticalXmlImportCacheFactory = VerticalXmlImportFactoryCache;
            verticalXmlImportCacheFactory.ClientName = SessionClientName;
            verticalXmlImportCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;

            verticalXmlImportFactory = VerticalXmlImportFactory;
            verticalXmlImportFactory.ClientName = SessionClientName;

            verticalXmlExportCacheFactory = VerticalXmlExportFactoryCache;
            verticalXmlExportCacheFactory.ClientName = SessionClientName;
            verticalXmlExportCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;

            verticalXmlExportFactory = VerticalXmlExportFactory;
            verticalXmlExportFactory.ClientName = SessionClientName;

            errorLogFactory = ErrorLogFactory;
            errorLogFactory.ClientName = SessionClientName;
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
        /// Gets the Users for ImportedBy dropdown.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [OutputCache(Duration = 0, VaryByParam = "*")]
        [HttpGet]
        public JsonResult GetUsers()
        {
            IEnumerable<int> importUserIDs = verticalXmlImportCacheFactory.GetAllEntities().Select(x => x.ImportedBy).Distinct();

            List<DisplayValuePair> lstDisplayValuePair = new List<DisplayValuePair>();
            foreach(int userID in importUserIDs)
            {
                UserObjectModel userDetails = userCacheFactory.GetEntityByKey(userID);
                lstDisplayValuePair.Add(new DisplayValuePair { Display = userDetails.FirstName + " " + userDetails.LastName, Value = userDetails.UserId.ToString() });
            }
            return Json(lstDisplayValuePair, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Searches the VerticalXMLImport Jobs.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult Search(DateTime? fromDate, DateTime? toDate, string userID)
        {
            int i;
            VerticalXmlImportSearchDetail objSearchDetail = new VerticalXmlImportSearchDetail()
            {
                FromImportDate = fromDate, 
                ToImportDate = toDate, 
                ImportedBy = !(string.IsNullOrEmpty(userID)) ? (int.TryParse(userID, out i) ? i : 0) : (int?)null
                
            };
            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            VerticalXmlImportSortColumn SortColumn = VerticalXmlImportSortColumn.VerticalXmlImportId;
            switch (kendoGridPost.SortColumn)
            {
                case "ImportDate":
                    SortColumn = VerticalXmlImportSortColumn.ImportDate;
                    break;
                case "ImportDescription":
                    SortColumn = VerticalXmlImportSortColumn.ImportDescription;
                    break;
                case "ImportedByName":
                    SortColumn = VerticalXmlImportSortColumn.ImportedByName;
                    break;
                case "Status":
                    SortColumn = VerticalXmlImportSortColumn.Status;
                    break;
            }

            SortOrder sortOrder = SortOrder.Descending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("asc"))
            {
                sortOrder = SortOrder.Ascending;
            }

            int totalRecords = verticalXmlImportCacheFactory.GetEntitiesBySearch(0, 0, objSearchDetail).Select(p => p.VerticalXmlImportId).Count();

            IEnumerable<VerticalXmlImportObjectModel> verticalXmlImportJobs = verticalXmlImportCacheFactory.GetEntitiesBySearch(startRowIndex, kendoGridPost.PageSize,
                                                                                                        objSearchDetail,
                                                                                                        new VerticalXmlImportSortDetail() { Column = SortColumn, Order = sortOrder });


            List<VerticalXmlImportViewModel> lstVerticalXmlImportJobs = new List<VerticalXmlImportViewModel>();
            foreach(VerticalXmlImportObjectModel importObjModel in verticalXmlImportJobs)
            {
                lstVerticalXmlImportJobs.Add(new VerticalXmlImportViewModel()
                {
                    ImportDate = importObjModel.ImportDate.ToString("MM/dd/yyyy hh:mm:ss"),
                    ImportDescription = importObjModel.ImportDescription,
                    ImportedBy = importObjModel.ImportedBy,
                    ImportedByName = importObjModel.ImportedByName,
                    ImportTypes = importObjModel.ImportTypes,
                    ImportXml = importObjModel.ImportXml,
                    Status = GetStatus(importObjModel.Status),
                    StatusID = importObjModel.Status,
                    VerticalXmlImportId = importObjModel.VerticalXmlImportId,
                    ExportBackupId = importObjModel.ExportBackupId
                });
            }

            return Json(new { total = totalRecords, data = lstVerticalXmlImportJobs });
        }

        /// <summary>
        /// Get EnableRollback Button.
        /// </summary>
        /// <returns>System.Int32.</returns>
        [HttpGet]
        public int GetLatestJobStatus()
        {
            int maxImportId = verticalXmlImportCacheFactory.GetAllEntities().Count() > 0 ? verticalXmlImportCacheFactory.GetAllEntities().Max(i => i.VerticalXmlImportId) : 0;
            return verticalXmlImportCacheFactory.GetAllEntities().Where(x => x.VerticalXmlImportId == maxImportId).Count() > 0 ? verticalXmlImportCacheFactory.GetAllEntities().Where(x => x.VerticalXmlImportId == maxImportId).FirstOrDefault().Status : 0;
        }

        /// <summary>
        /// Rollback Last Job.
        /// </summary>
        /// <returns>System.String.</returns>
        [HttpGet]
        public string Rollback()
        {
            try
            {
                int maxImportId = verticalXmlImportCacheFactory.GetAllEntities().Max(i => i.VerticalXmlImportId);
                VerticalXmlImportObjectModel objModel = verticalXmlImportCacheFactory.GetEntityByKey(maxImportId);
                if (objModel.Status == (int)JobStatus.ImportCompleted)
                {
                    objModel.Status = (int)JobStatus.RollBackNotStarted;
                    verticalXmlImportCacheFactory.SaveEntity(objModel);

                    return "Rollback Job added to the Queue.";
                }
                else
            {
                    return "Error: Cannot add Rollback Job to the Queue";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Get GetErrorLogs.
        /// </summary>
        /// <param name="ImportXmlID">The import XML identifier.</param>
        /// <returns>FileContentResult.</returns>
        [HttpGet]
        public FileContentResult GetErrorLogs(string ImportXmlID)
        {
            IEnumerable<ErrorLogObjectModel> errorLogs = errorLogFactory.GetEntitiesBySearch(new ErrorLogSearchDetail { EventId = Convert.ToInt32(ImportXmlID), Title = "Import XML Error" });
            StringBuilder errorMessages = new StringBuilder();

            var fileName = "error_" + SessionUserID + ".xml";

            errorMessages.Append("<errorLog>");
            foreach(ErrorLogObjectModel error in errorLogs)
                    {
                errorMessages.Append("<error>");
                errorMessages.Append("<code>" + error.ErrorCode + "</code>");
                errorMessages.Append("<date>" + error.ErrorUtcDate + "</date>");
                errorMessages.Append("<title>" + error.Title + "</title>");
                errorMessages.Append("<machine>" + error.MachineName + "</machine>");
                errorMessages.Append("<process>" + error.ProcessName + "</process>");
                errorMessages.Append("<message>" + error.Message + "</message>");
                errorMessages.Append("<formattedmessage>" + error.FormattedMessage + "</formattedmessage>");
                errorMessages.Append("</error>");
            }
            errorMessages.Append("</errorLog>");

            var mimeType = "application/xml";

            byte[] fileContent = Encoding.ASCII.GetBytes(errorMessages.ToString());
            return File(fileContent, mimeType, fileName);
                    }

        /// <summary>
        /// Get ImportedXml.
        /// </summary>
        /// <param name="ImportXmlID">The import XML identifier.</param>
        /// <returns>FileContentResult.</returns>
        [HttpGet]
        public FileContentResult GetImportedXml(string ImportXmlID)
        {
            string importXml = verticalXmlImportFactory.GetEntityByKey(Convert.ToInt32(ImportXmlID)).ImportXml;

            if (!String.IsNullOrEmpty(importXml))
                    {
                var mimeType = "application/xml";

                byte[] fileContent = Encoding.ASCII.GetBytes(importXml);
                return File(fileContent, mimeType, "import.xml");

            }


            return null;
                    }

        /// <summary>
        /// Get BackupXml.
        /// </summary>
        /// <param name="BackupXmlID">The backup XML identifier.</param>
        /// <returns>FileContentResult.</returns>
        [HttpGet]
        public FileContentResult GetBackupXml(string BackupXmlID)
        {
            string backupXml = verticalXmlExportFactory.GetEntityByKey(Convert.ToInt32(BackupXmlID)) != null ? verticalXmlExportFactory.GetEntityByKey(Convert.ToInt32(BackupXmlID)).ExportXml : string.Empty;

            if (!String.IsNullOrEmpty(backupXml))
                    {
                var mimeType = "application/xml";

                byte[] fileContent = Encoding.ASCII.GetBytes(backupXml);
                return File(fileContent, mimeType, "backup.xml");

            }

            return null;
        }

        /// <summary>
        /// Add new Vertical Xml Import Job.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult Add()
        {
            AddVerticalXmlImportViewModel viewModel = new AddVerticalXmlImportViewModel();
            viewModel.InProgressJobCount = verticalXmlImportCacheFactory.GetAllEntities().Where(x => x.Status == (int)JobStatus.NotStarted || x.Status == (int)JobStatus.InProgress || x.Status == (int)JobStatus.BackupCompleted && x.ImportedBy == SessionUserID).Count();
            viewModel.ImportDescription = "Data Import on " + DateTime.Now.ToShortDateString();
            ViewData["Success"] = "In Progress";
            return View(viewModel);
        }

        /// <summary>
        /// Add new Vertical Xml Import Job.
        /// </summary>
        /// <param name="uploadFile">The upload file.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult Add(HttpPostedFileBase uploadFile)
        {
            AddVerticalXmlImportViewModel viewModel = new AddVerticalXmlImportViewModel();
            viewModel.InProgressJobCount = verticalXmlImportCacheFactory.GetAllEntities().Where(x => x.Status == 0 || x.Status == 1 || x.Status == 2).Count(); 

            VerticalXmlImportObjectModel objModel = new VerticalXmlImportObjectModel();
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(uploadFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Import/Create"), fileName);
                uploadFile.SaveAs(path);
                try
                {
                    if (ValidXml(path))
                    {
                        objModel.ImportTypes = 1;
                        objModel.ImportXml = System.IO.File.ReadAllText(path);
                        objModel.ImportedBy = SessionUserID;
                        objModel.ImportDescription = Request.Form["ImportDescription"];

                        verticalXmlImportCacheFactory.SaveEntity(objModel);
                        System.IO.File.Delete(path);
                        ViewData["Success"] = "OK";
                    }
                    else
                    {
                        ViewData["Success"] = "Error";
                    }
                }
                catch(Exception ex)
                {
                    ViewData["Success"] = "Error";
                    List<string> ErrorList = new List<string>();
                    ErrorList.Add(ex.Message);
                    ViewBag.XmlValidationErrors = ErrorList;
                    Server.ClearError();
                }
            }
            return View(viewModel);
        }

        /// <summary>
        /// Valids the XML.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool ValidXml(string path)
        {
            List<string> ErrorList = new List<string>();
            bool isValid = true;
            if(!string.IsNullOrWhiteSpace(path))
            {
                XmlSchemaSet schema = new XmlSchemaSet();
                schema.Add(null, Path.Combine(Server.MapPath("~/XmlSchema/VerticalImport"), "import.xsd"));

                XDocument xmlDoc = XDocument.Load(path);

                if (string.IsNullOrWhiteSpace(xmlDoc.Root.GetDefaultNamespace().NamespaceName) || xmlDoc.Root.GetDefaultNamespace().NamespaceName != "http://rightprospectus.com/hostedSchema")
                {
                    ErrorList.Add("Xml doesn't contain 'http://rightprospectus.com/hostedSchema' namespace");
                }

                xmlDoc.Validate(schema, (o, e) => {
                    if (e.Severity != XmlSeverityType.Warning)
                    {
                        if (!ErrorList.Contains(e.Message))
                        {
                            ErrorList.Add(e.Message);
                        }
                        isValid = false;
                    }
                });
            }
            if (ErrorList.Count > 0)
                ViewBag.XmlValidationErrors = ErrorList;

            return isValid;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>System.String.</returns>
        private string GetStatus(int status)
        {
            switch(status)
            {
                case (int)JobStatus.NotStarted: 
                    return "Job Not Started";
                case (int)JobStatus.InProgress:
                    return "Job In Progress";
                case (int)JobStatus.BackupCompleted:
                    return "Backup Completed";
                case (int)JobStatus.ImportCompleted:
                    return "Import Completed";
                case (int)JobStatus.RollBackNotStarted:
                    return "Rollback Queued";
                case (int)JobStatus.RollBackInProgress:
                    return "Rollback In Progress";
                case (int)JobStatus.RollBackCompleted:
                    return "Rollback Completed";
                case (int)JobStatus.ImportCompletedMarketIdsMissing:
                    return "Import Completed - MarketIds Missing";

                default:
                    return "Error";
            }
        }
    }
}