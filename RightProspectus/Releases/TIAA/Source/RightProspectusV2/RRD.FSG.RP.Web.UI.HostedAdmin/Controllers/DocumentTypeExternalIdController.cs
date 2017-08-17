// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-16-2015
// ***********************************************************************

using RP.Extensions;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
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
    /// Class DocumentTypeExternalIdController.
    /// </summary>
    public class DocumentTypeExternalIdController : BaseController
    {
        #region Constants
        ///<summary>
        ///Constants
        ///</summary>
        private const string defaultdropvalue = "-1";
        private const string dropDocumentType ="--Please select Document Type--";
        
        #endregion

        /// <summary>
        /// The document type external identifier cache factory
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public IFactoryCache<DocumentTypeExternalIdFactory, DocumentTypeExternalIdObjectModel, DocumentTypeExternalIdKey> documentTypeExternalIdCacheFactory;
        /// <summary>
        /// The user cache factory
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        /// <summary>
        /// The document type cache factory
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public IFactoryCache<DocumentTypeFactory, DocumentTypeObjectModel, int> documentTypeCacheFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeExternalIdController" /> class.
        /// </summary>
        public DocumentTypeExternalIdController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                documentTypeExternalIdCacheFactory = RPV2Resolver.Resolve<IFactoryCache<DocumentTypeExternalIdFactory, DocumentTypeExternalIdObjectModel, DocumentTypeExternalIdKey>>("DocumentTypeExternalID");
                documentTypeExternalIdCacheFactory.ClientName = SessionClientName;
                documentTypeExternalIdCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;

                documentTypeCacheFactory = RPV2Resolver.Resolve<IFactoryCache<DocumentTypeFactory, DocumentTypeObjectModel, int>>("DocumentType");
                documentTypeCacheFactory.ClientName = SessionClientName;
                documentTypeCacheFactory.Mode = FactoryCacheMode.All;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeExternalIdController"/> class.
        /// </summary>
        /// <param name="DocumentTypeExternalIdFactoryCache">The document type external identifier factory cache.</param>
        /// <param name="UserFactoryCache">The user factory cache.</param>
        /// <param name="DocumentTypeFactoryCache">The document type factory cache.</param>
        public DocumentTypeExternalIdController(IFactoryCache<DocumentTypeExternalIdFactory, DocumentTypeExternalIdObjectModel, DocumentTypeExternalIdKey> DocumentTypeExternalIdFactoryCache, IFactoryCache<UserFactory, UserObjectModel, int> UserFactoryCache, IFactoryCache<DocumentTypeFactory, DocumentTypeObjectModel, int> DocumentTypeFactoryCache)
        {
            documentTypeExternalIdCacheFactory = DocumentTypeExternalIdFactoryCache;
            documentTypeExternalIdCacheFactory.ClientName = SessionClientName;
            documentTypeExternalIdCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = UserFactoryCache;
            userCacheFactory.Mode = FactoryCacheMode.All;

            documentTypeCacheFactory = DocumentTypeFactoryCache;
            documentTypeCacheFactory.ClientName = SessionClientName;
            documentTypeCacheFactory.Mode = FactoryCacheMode.All;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            ViewData["SelectedCustomer"] = SessionClientName;
            return View("DocumentTypeExternalId");
        }

        #region GetAllDocumentTypeExtrenalIdDetails
        /// <summary>
        /// Gets all document type external identifier details.
        /// </summary>
        /// <param name="DocumentTypeId">The document type identifier.</param>
        /// <param name="ExtrenalID">The external identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllDocumentTypeExtrenalIdDetails(string DocumentTypeId, string ExtrenalID)
        {
           
            int id;
            DocumentTypeExternalIdSearchDetail objSearchDetail = new DocumentTypeExternalIdSearchDetail()
            {
                DocumentTypeID = !(string.IsNullOrEmpty(DocumentTypeId)) ? (int.TryParse(DocumentTypeId, out id) ? id : 0) : (int?)null,
                ExternalId = string.IsNullOrEmpty(ExtrenalID) ? null : ExtrenalID
            };
            
            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            DocumentTypeExternalIdSortColumn SortColumn = DocumentTypeExternalIdSortColumn.DocumentTypeName;
            switch (kendoGridPost.SortColumn)
            {
                case "ExternalId":
                    SortColumn = DocumentTypeExternalIdSortColumn.ExternalId;
                    break;
                case "DocumentTypeName":
                    SortColumn = DocumentTypeExternalIdSortColumn.DocumentTypeId;
                    break;
               
                case "IsPrimary":
                    SortColumn = DocumentTypeExternalIdSortColumn.IsPrimary;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            var Result = (from dbo in
                              (from dte in documentTypeExternalIdCacheFactory.GetEntitiesBySearch(
                                   startRowIndex,
                                   kendoGridPost.PageSize,
                                   objSearchDetail,
                                   new DocumentTypeExternalIdSortDetail { Column = SortColumn, Order = sortOrder })
                               join dt in documentTypeCacheFactory.GetAllEntities() on dte.DocumentTypeId equals dt.DocumentTypeId
                               orderby SortColumn
                               select new
                               {
                                   DocumentTypeId = dte.DocumentTypeId,
                                   DocumentTypeName = dt.Name,
                                   ExternalId = dte.ExternalId,
                                   IsPrimary = dte.IsPrimary
                               })
                          orderby (SortColumn)
                          select dbo);

            return Json(new
            {
                total = documentTypeExternalIdCacheFactory.GetEntitiesBySearch(objSearchDetail).Count(),
                data = from obj in Result
                       select new 
                       {
                           DocumentTypeName = obj.DocumentTypeName,
                           ExternalId = obj.ExternalId,
                           DocumentTypeId = obj.DocumentTypeId,
                           IsPrimary = obj.IsPrimary.ToString()
                       }
            });
        }
        #endregion

        #region Load Search Creteria Methods.

        /// <summary>
        /// Gets the document types.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetDocumentTypes()
        {
            return (Json((from dbo in documentTypeExternalIdCacheFactory.GetAllEntities()
                          join dt in documentTypeCacheFactory.GetAllEntities() on dbo.DocumentTypeId equals dt.DocumentTypeId
                          select new
                          {
                              Value = dbo.DocumentTypeId,
                              Display = dt.Name
                          }).Distinct(), JsonRequestBehavior.AllowGet));

        }
        /// <summary>
        /// Gets the external ids.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetExternalIds()
        {
            return (Json((from dbo in documentTypeExternalIdCacheFactory.GetAllEntities()
                          select new { Display = dbo.ExternalId, Value = dbo.ExternalId }).Distinct().OrderBy(ExternalId => ExternalId.Display),
                          JsonRequestBehavior.AllowGet));
        }

        #endregion

        #region Edit Document Type External Id
        /// <summary>
        /// Edits the document type external identifier.
        /// </summary>
        /// <param name="DocumentType">Type of the document.</param>
        /// <param name="ExternalId">The external identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult EditDocumentTypeExternalId(int DocumentType, string ExternalId)
        {
            EditDocumentTypeExternalIdViewModel EditViewModel = new EditDocumentTypeExternalIdViewModel();
            EditViewModel.DocumentType = new List<DisplayValuePair>();
            EditViewModel.DocumentType.Add(new DisplayValuePair { Value = defaultdropvalue, Display = dropDocumentType });

            IEnumerable<DocumentTypeObjectModel> documentTypeDetails = documentTypeCacheFactory.GetAllEntities();

            foreach (var item in documentTypeDetails)
            {
                EditViewModel.DocumentType.Add(new DisplayValuePair()
                {
                    Display = item.Name,
                    Value = item.DocumentTypeId.ToString()
                });
            }

            DocumentTypeExternalIdObjectModel objObjectModel = documentTypeExternalIdCacheFactory.GetEntitiesBySearch(new DocumentTypeExternalIdSearchDetail { DocumentTypeID = DocumentType, ExternalId = ExternalId }).FirstOrDefault();

            if (objObjectModel != null)
            {
                EditViewModel.SelectedDocumentTypeId = objObjectModel.DocumentTypeId;
                EditViewModel.ExternalId = objObjectModel.ExternalId;
                EditViewModel.IsPrimary = objObjectModel.IsPrimary;
                EditViewModel.ModifiedDate = objObjectModel.ModifiedDate.ToString();
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
                EditViewModel.SelectedDocumentTypeId = -1;
                EditViewModel.ExternalId = string.Empty;
                EditViewModel.IsPrimary = false;
            }

            ViewData["Success"] = "In Progress";
            return View(EditViewModel);
        }

        /// <summary>
        /// Edits the document type external identifier.
        /// </summary>
        /// <param name="editViewModel">The edit view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult EditDocumentTypeExternalId(EditDocumentTypeExternalIdViewModel editViewModel)
        {
            try
            {
                DocumentTypeExternalIdObjectModel objObjectModel = new DocumentTypeExternalIdObjectModel()
                {
                    DocumentTypeId = editViewModel.SelectedDocumentTypeId,
                    ExternalId = editViewModel.ExternalId,
                    IsPrimary = editViewModel.IsPrimary
                };
                documentTypeExternalIdCacheFactory.SaveEntity(objObjectModel, SessionUserID);

                editViewModel.DocumentType = new List<DisplayValuePair>();
                editViewModel.DocumentType.Add(new DisplayValuePair { Value = defaultdropvalue, Display = dropDocumentType });
             
                foreach (var item in documentTypeCacheFactory.GetAllEntities())
                {
                    editViewModel.DocumentType.Add(new DisplayValuePair()
                    {
                        Display = item.Name,
                        Value = item.DocumentTypeId.ToString()
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

        #region DisableDocumentTypeExtrenalId
        /// <summary>
        /// Disables the document type external identifier.
        /// </summary>
        /// <param name="DocumentType">Type of the document.</param>
        /// <param name="ExternalId">The external identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DisableDocumentTypeExtrenalId(int DocumentType, string ExternalId)
        {
            //TODO: Idealy should pass the DocumentTypeExternalIdKey object.
            documentTypeExternalIdCacheFactory.DeleteEntity(
                new DocumentTypeExternalIdObjectModel()
                {
                    DocumentTypeId = DocumentType,
                    ExternalId = ExternalId
                },
                SessionUserID);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CheckCombinationDataAlreadyExists
        /// <summary>
        /// Checks the combination data already exists.
        /// </summary>
        /// <param name="DocumentTypeId">The document type identifier.</param>
        /// <param name="ExternalID">The external identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckCombinationDataAlreadyExists(int DocumentTypeId, string ExternalID)
        {
            bool isDataExists = false;
            if (documentTypeExternalIdCacheFactory.GetEntitiesBySearch(new DocumentTypeExternalIdSearchDetail() { DocumentTypeID = DocumentTypeId, ExternalId = ExternalID }).Count() > 0)
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
        /// <param name="ExternalId">The external identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckExternalIdAlreadyExists(string ExternalId)
        {
            bool isDataExists = false;            
            if (documentTypeExternalIdCacheFactory.GetEntitiesBySearch(new DocumentTypeExternalIdSearchDetail() { ExternalId = ExternalId }).Count() > 0)
            {
                isDataExists = true;
            }

            return Json(isDataExists, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}