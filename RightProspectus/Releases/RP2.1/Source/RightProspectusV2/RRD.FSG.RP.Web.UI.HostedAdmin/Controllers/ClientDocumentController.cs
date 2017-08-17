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
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.HostedAdmin.Common;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// Class ClientDocumentController.
    /// </summary>
    public class ClientDocumentController : BaseController
    {
        #region Constants
        /// <summary>
        /// Constants
        /// </summary>
        private const string defaultvalue = "-1";
        private const string defaultDocument = "--Please select Document Type--";
        #endregion

        #region Properties
        /// <summary>
        /// The client document cache factory
        /// </summary>
        private IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int> clientDocumentCacheFactory;
        /// <summary>
        /// The client document type cache factory
        /// </summary>
        private IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int> clientDocumentTypeCacheFactory;
        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDocumentController"/> class.
        /// </summary>
        public ClientDocumentController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                clientDocumentCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int>>("ClientDocument");
                clientDocumentCacheFactory.ClientName = SessionClientName;
                clientDocumentCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;

                clientDocumentTypeCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int>>("ClientDocumentType");
                clientDocumentTypeCacheFactory.ClientName = SessionClientName;
                clientDocumentTypeCacheFactory.Mode = FactoryCacheMode.All;
            }


        }
        #endregion


        #region Test_Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDocumentController"/> class.
        /// </summary>
        /// <param name="ClientDocumentCacheFactory">The client document cache factory.</param>
        /// <param name="ClientDocumentTypeCacheFactory">The client document type cache factory.</param>
        /// <param name="UserCacheFactory">The user cache factory.</param>
        public ClientDocumentController(IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int> ClientDocumentCacheFactory, IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int> ClientDocumentTypeCacheFactory, IFactoryCache<UserFactory, UserObjectModel, int> UserCacheFactory)
        {
            clientDocumentCacheFactory = ClientDocumentCacheFactory;
            clientDocumentTypeCacheFactory = ClientDocumentTypeCacheFactory;
            userCacheFactory = UserCacheFactory;

        }
        #endregion


        #region List
        // GET: ClientDocument
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


        #region Index
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            ViewData["SelectedCustomer"] = SessionClientName;
            return View("ClientDocument");
        }
        #endregion


        #region GetAllClientDocumentDetails
        /// <summary>
        /// Gets all client document details.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="name">The name.</param>
        /// <param name="clientDocumentTypeId">The client document type identifier.</param>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <param name="isPrivate">The is private.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllClientDocumentDetails(string fileName, string name, int? clientDocumentTypeId, string mimeType, string isPrivate)
        {

            ClientDocumentSearchDetail objClientDocumentSearchDetail = new ClientDocumentSearchDetail()
            {
                FileName = string.IsNullOrEmpty(fileName) ? null : fileName,
                Name = string.IsNullOrEmpty(name) ? null : name,
                ClientDocumentTypeId = clientDocumentTypeId.HasValue ? clientDocumentTypeId : null,
                MimeType = string.IsNullOrEmpty(mimeType) ? null : mimeType,
                IsPrivate = string.IsNullOrEmpty(isPrivate) ? (bool?)null : Convert.ToBoolean(isPrivate)
            };

            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            ClientDocumentSortColumn SortColumn = ClientDocumentSortColumn.Name;
            switch (kendoGridPost.SortColumn)
            {
                case "FileName":
                    SortColumn = ClientDocumentSortColumn.FileName;
                    break;
                case "MimeType":
                    SortColumn = ClientDocumentSortColumn.MimeType;
                    break;
                case "IsPrivate":
                    SortColumn = ClientDocumentSortColumn.IsPrivate;
                    break;
                case "ModifiedBy":
                    SortColumn = ClientDocumentSortColumn.ModifiedBy;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }


            return Json(new
            {
                total = clientDocumentCacheFactory.GetEntitiesBySearch(objClientDocumentSearchDetail).Select(p => p.ClientDocumentId).Count(),
                data = (from objectModel in clientDocumentCacheFactory.GetEntitiesBySearch(startRowIndex, kendoGridPost.PageSize,
                                                                  objClientDocumentSearchDetail,
                                                                  new ClientDocumentSortDetail() { Column = SortColumn, Order = sortOrder })
                        select new
                        {
                            ClientDocumentId = objectModel.ClientDocumentId,
                            ClientDocumentTypeId = objectModel.ClientDocumentTypeId,
                            ClientDocumentTypeName = objectModel.ClientDocumentTypeName,
                            Name = objectModel.Name,
                            FileName = objectModel.FileName,
                            MimeType = objectModel.MimeType,
                            IsPrivateString = objectModel.IsPrivate.ToString(),
                            ContentUri = objectModel.ContentUri,
                            UTCLastModifiedDate = RPFormatDate(objectModel.LastModified),
                            ModifiedBy = objectModel.ModifiedBy
                        }
                    )
            });
        }
        #endregion


        #region Load Search Creteria Methods.

        /// <summary>
        /// Gets the client document name.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetName()
        {
            return Json((from key in clientDocumentCacheFactory.GetAllEntities().Select(x => x.Name).Distinct()
                         select new { Display = key, Value = key }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetFileName()
        {
            return Json((from key in clientDocumentCacheFactory.GetAllEntities().Select(x => x.FileName).Distinct()
                         select new { Display = key, Value = key }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the name of the client document type.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetClientDocumentTypeName()
        {
            return Json((from key in clientDocumentCacheFactory.GetAllEntities().Select(y => y.ClientDocumentTypeId).Distinct()
                         select new { Display = clientDocumentTypeCacheFactory.GetAllEntities().FirstOrDefault(x => x.ClientDocumentTypeId == key).Name, Value = key.ToString() }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the type of the MIME.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetMimeType()
        {
            return Json((from key in clientDocumentCacheFactory.GetAllEntities().Select(x => x.MimeType).Distinct()
                         select new { Display = key, Value = key }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the is private.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetIsPrivate()
        {
            return Json((from key in clientDocumentCacheFactory.GetAllEntities().Select(x => x.IsPrivate).Distinct()
                         select new { Display = key.ToString(), Value = key.ToString() }), JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Edit
        /// <summary>
        /// Edits the client document.
        /// </summary>
        /// <param name="id1">The id1.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult EditClientDocument(int id1)
        {
            ClientDocumentViewModel objViewModel = new ClientDocumentViewModel();
            objViewModel.DocumentType = new List<DisplayValuePair>();

            objViewModel.DocumentType.Add(new DisplayValuePair { Value = defaultvalue, Display = defaultDocument });

            IEnumerable<ClientDocumentTypeObjectModel> clientDocumentTypes = clientDocumentTypeCacheFactory.GetAllEntities();
            if (clientDocumentTypes != null)
            {
                foreach (var clientDocumentType in clientDocumentTypes)
                {
                    objViewModel.DocumentType.Add(new DisplayValuePair { Display = clientDocumentType.Name, Value = clientDocumentType.ClientDocumentTypeId.ToString() });
                }
            }

            if (id1 != 0)
            {
                ClientDocumentObjectModel objObjectModel = (from dbo in clientDocumentCacheFactory.GetAllEntities()
                                                            where dbo.ClientDocumentId == id1
                                                            select dbo).FirstOrDefault();

                objViewModel.ClientDocumentId = objObjectModel.ClientDocumentId;
                objViewModel.Name = objObjectModel.Name;
                objViewModel.FileName = objObjectModel.FileName;
                objViewModel.MimeType = objObjectModel.MimeType;
                objViewModel.Description = objObjectModel.Description;
                objViewModel.IsPrivate = objObjectModel.IsPrivate;
                objViewModel.SelectedClientDocumentTypeId = objObjectModel.ClientDocumentTypeId;
                objViewModel.UTCLastModifiedDate = objObjectModel.LastModified.ToString();
                if (objObjectModel.ModifiedBy != 0)
                {
                    objViewModel.ModifiedByName = objObjectModel.ModifiedBy.ToString();
                    if (userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = objObjectModel.ModifiedBy }).Count() > 0)
                    {
                        objViewModel.ModifiedByName = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = objObjectModel.ModifiedBy }).FirstOrDefault().UserName;
                    }
                }
                else
                {
                    objViewModel.ModifiedByName = string.Empty;
                }
            }
            else
            {
                objViewModel.SelectedClientDocumentTypeId = -1;
            }

            ViewData["Success"] = "In Progress";
            return View(objViewModel);
        }


        /// <summary>
        /// Edits the client document.
        /// </summary>
        /// <param name="objViewModel">The object view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult EditClientDocument(ClientDocumentViewModel objViewModel)
        {
            try
            {
                int id;
                int.TryParse(Request.Form["ClientDocumentId"], out id);

                ClientDocumentObjectModel objObjectModel = new ClientDocumentObjectModel()
                {
                    ClientDocumentId = id,
                    Name = objViewModel.Name,
                    Description = objViewModel.Description,
                    ClientDocumentTypeId = objViewModel.SelectedClientDocumentTypeId,
                    IsPrivate = objViewModel.IsPrivate
                };

                objViewModel.DocumentType = new List<DisplayValuePair>();
                objViewModel.DocumentType.Add(new DisplayValuePair { Value = defaultvalue, Display = defaultDocument });

                IEnumerable<ClientDocumentTypeObjectModel> clientDocumentTypes = clientDocumentTypeCacheFactory.GetAllEntities();
                if (clientDocumentTypes != null)
                {
                    foreach (var clientDocumentType in clientDocumentTypes)
                    {
                        objViewModel.DocumentType.Add(new DisplayValuePair { Display = clientDocumentType.Name, Value = clientDocumentType.ClientDocumentTypeId.ToString() });
                    }
                }

                var file = Request.Files[0];
                if (file.ContentLength != 0)
                {
                    objObjectModel.FileName = Path.GetFileName(file.FileName);
                    objObjectModel.MimeType = file.ContentType;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        objObjectModel.FileData = binaryReader.ReadBytes(file.ContentLength);
                    }
                }
                else
                {
                    //TODO: Consider adding validation as this code is vulnerable
                    objObjectModel.FileName = Request.Form["fileName"];
                    objObjectModel.MimeType = Request.Form["mimeType"];
                }
                clientDocumentCacheFactory.SaveEntity(objObjectModel, SessionUserID);

                ViewData["Success"] = "OK";
            }
            catch (Exception exception)
            {
                objViewModel.SuccessOrFailedMessage = exception.Message;
            }
            return View(objViewModel);

        }
        #endregion


        #region CheckNameAndDescriptionAlreadyExists
        /// <summary>
        /// Checks the data already exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="currentID">The current identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckDataAlreadyExists(string name, string description, string currentID)
        {
            bool isDataExists = false;

            if ((from cd in clientDocumentCacheFactory.GetEntitiesBySearch(new ClientDocumentSearchDetail() { Name = string.IsNullOrEmpty(name) ? null : name, Description = string.IsNullOrEmpty(description) ? null : description })
                 where cd.ClientDocumentId != Convert.ToInt32(currentID)
                 select cd).Count() > 0)
            {
                isDataExists = true;
            }


            return Json(isDataExists, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Disable Client Document Type
        /// <summary>
        /// Deletes the type of the client document.
        /// </summary>
        /// <param name="clientDocumentId">The client document identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DeleteClientDocument(int clientDocumentId)
        {
            clientDocumentCacheFactory.DeleteEntity(clientDocumentId, SessionUserID);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}