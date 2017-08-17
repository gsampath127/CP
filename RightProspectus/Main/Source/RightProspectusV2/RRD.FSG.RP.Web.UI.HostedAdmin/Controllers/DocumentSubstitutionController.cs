
using RP.Extensions;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Factories;
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
using System.Net;
using System.Web.Mvc;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    public class DocumentSubstitutionController : BaseController
    {
        #region Constants
        private const string defaultdropvalue = "-1";
        private const string dropName = "--Please select Name--";
        #endregion
        private IFactoryCache<DocumentTypeFactory, DocumentTypeObjectModel, int> documentTypeCacheFactory;
        private IFactoryCache<DocumentSubstitutionFactory, DocumentSubstitutionObjectModel, int> documentSubstitutionCacheFactory;

        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentSubstitutionController" /> class.
        /// </summary>
        public DocumentSubstitutionController()
        {
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                documentSubstitutionCacheFactory = RPV2Resolver.Resolve<IFactoryCache<DocumentSubstitutionFactory, DocumentSubstitutionObjectModel, int>>("DocumentSubstitution");
                documentSubstitutionCacheFactory.ClientName = SessionClientName;
                documentSubstitutionCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;

                documentTypeCacheFactory = RPV2Resolver.Resolve<IFactoryCache<DocumentTypeFactory, DocumentTypeObjectModel, int>>("DocumentType");
                documentTypeCacheFactory.ClientName = SessionClientName;
                documentTypeCacheFactory.Mode = FactoryCacheMode.All;
            }
        }

        #endregion

        // GET: DocumentSubstitution
        public ActionResult DocumentSubstitution()
        {
            ViewData["SelectedCustomer"] = SessionClientName;
            return View();
        }



        #region GetAllDocumentSubstitutionDetails
        /// <summary>
        /// Get Combo drop down value for Document Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetDocumenType()
        {

            List<string> documentSubstitutionDetails = documentSubstitutionCacheFactory.GetAllEntities().Select(v => v.DocumentType).ToList();
            List<DocumentTypeObjectModel> documentTypeDetails = documentTypeCacheFactory.GetAllEntities().Where(x => documentSubstitutionDetails.Contains(x.MarketId)).ToList();
            return (Json(documentTypeDetails, JsonRequestBehavior.AllowGet));

        }

        /// <summary>
        /// GetAllDocumentSubstitutionDetails
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="version"></param>
        /// <param name="downloadUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAllDocumentSubstitutionDetails(string documentType)
        {
            DocumentSubstitutionSearchDetail objSearchDetail = new DocumentSubstitutionSearchDetail()
            {
                DocumentType = string.IsNullOrEmpty(documentType) ? null : documentType
            };

            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            DocumentSubstitutionSortColumn SortColumn = DocumentSubstitutionSortColumn.DocumentType;
            switch (kendoGridPost.SortColumn)
            {
                case "SubstituteDocumentType":
                    SortColumn = DocumentSubstitutionSortColumn.SubstituteDocumentType;
                    break;
                case "NDocumentType":
                    SortColumn = DocumentSubstitutionSortColumn.NDocumentType;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            var Result = (from dbo in
                              (from ds in documentSubstitutionCacheFactory.GetEntitiesBySearch(
                                   startRowIndex,
                                   kendoGridPost.PageSize,
                                   objSearchDetail,
                                   new DocumentSubstitutionSortDetail { Column = SortColumn, Order = sortOrder })
                               orderby SortColumn
                               select new
                               {
                                   Id = ds.Id,
                                   DocumentType = ds.DocumentType,
                                   SubstituteDocumentType = ds.SubstituteDocumentType,
                                   NDocumentType = ds.NDocumentType
                               })
                          orderby (SortColumn)
                          select dbo);
            return Json(new
            {
                total = documentSubstitutionCacheFactory.GetEntitiesBySearch(objSearchDetail).Count(),
                data = from obj in Result
                       select new
                       {
                           Id = obj.Id,
                           DocumentType = obj.DocumentType,
                           SubstituteDocumentType = obj.SubstituteDocumentType,
                           NDocumentType = obj.NDocumentType
                       }
            });
        }
        #endregion

        #region DisableDocumentSubstitution

        [HttpGet]
        public JsonResult DisableDocumentSubstitution(int DocumentSubstitutionId)
        {
            documentSubstitutionCacheFactory.DeleteEntity(
                new DocumentSubstitutionObjectModel()
                {
                    Id = DocumentSubstitutionId
                },
                SessionUserID);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region EditDocumentSubstitution
        [HttpGet]
        [IsPopUp]
        public ActionResult EditDocumentSubstitution(int DocumentSubstitutionID)
        {
            EditDocumentSubstitutionViewModel EditViewModel = new EditDocumentSubstitutionViewModel();
            EditViewModel.DocumentType = new List<DocumentTypeObjectModel>();
            EditViewModel.SubstituteDocumentType = new List<DocumentTypeObjectModel>();
            IEnumerable<DocumentTypeObjectModel> documentTypeDetails = documentTypeCacheFactory.GetAllEntities().Distinct().ToList();
            EditViewModel.DocumentType = documentTypeDetails.ToList();
            EditViewModel.SubstituteDocumentType = EditViewModel.DocumentType;
            DocumentSubstitutionObjectModel objObjectModel = documentSubstitutionCacheFactory.GetEntitiesBySearch(new DocumentSubstitutionSearchDetail { Id = DocumentSubstitutionID }).FirstOrDefault();

            if (objObjectModel != null)
            {
                if (DocumentSubstitutionID > 0)
                {
                    EditViewModel.Id = objObjectModel.Id;
                    EditViewModel.SelectedDocumentType = objObjectModel.DocumentType;
                    EditViewModel.SelectedSubstituteDocumentType = objObjectModel.SubstituteDocumentType;
                    EditViewModel.ModifiedDate = objObjectModel.LastModified.ToString();
                    EditViewModel.ModifiedBy = objObjectModel.ModifiedBy;
                    EditViewModel.SelectedNDocumentType = objObjectModel.NDocumentType;
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

            }
            List<string> docTypes = documentSubstitutionCacheFactory.GetAllEntities().Select(x => x.DocumentType).ToList();
            docTypes.Remove(EditViewModel.SelectedDocumentType);
            EditViewModel.DocumentType = EditViewModel.DocumentType.Where(x => !docTypes.Contains(x.MarketId)).ToList();
            EditViewModel.DocumentType.Insert(0, new DocumentTypeObjectModel { MarketId = defaultdropvalue, Name = dropName });
            EditViewModel.SubstituteDocumentType.Insert(0, new DocumentTypeObjectModel { MarketId = defaultdropvalue, Name = dropName });
            ViewData["Success"] = "In Progress";
            return View(EditViewModel);
        }

        [HttpPost]
        [IsPopUp]
        public ActionResult EditDocumentSubstitution(EditDocumentSubstitutionViewModel editViewModel, string selectedNDocumentType)
        {
            try
            {
                DocumentSubstitutionObjectModel objObjectModel = new DocumentSubstitutionObjectModel()
                {
                    Id = editViewModel.Id,
                    DocumentType = editViewModel.SelectedDocumentType,
                    SubstituteDocumentType = editViewModel.SelectedSubstituteDocumentType == "-1" ? null : editViewModel.SelectedSubstituteDocumentType,
                    NDocumentType = editViewModel.SelectedNDocumentType
                };
                documentSubstitutionCacheFactory.SaveEntity(objObjectModel, SessionUserID);
                editViewModel.DocumentType = new List<DocumentTypeObjectModel>();
                editViewModel.SubstituteDocumentType = new List<DocumentTypeObjectModel>();
                List<string> docTypes = documentSubstitutionCacheFactory.GetAllEntities().Select(x => x.DocumentType).ToList();
                docTypes.Remove(editViewModel.SelectedDocumentType);
                editViewModel.DocumentType = editViewModel.DocumentType.Where(x => !docTypes.Contains(x.MarketId)).ToList();
                editViewModel.DocumentType.Insert(0, new DocumentTypeObjectModel { MarketId = defaultdropvalue, Name = dropName });
                editViewModel.SubstituteDocumentType.Insert(0, new DocumentTypeObjectModel { MarketId = defaultdropvalue, Name = dropName });
                //if (!string.IsNullOrEmpty(selectedNDocumentType))
                //{
                //    foreach (var item in selectedNDocumentType.Trim().Split(','))
                //    {
                //    }
                //}
                ViewData["Success"] = "OK";
            }
            catch (Exception e)
            {
                editViewModel.SuccessOrFailedMessage = e.Message;
            }
            return View(editViewModel);
        }


        #endregion

        #region
        /// <summary>
        /// Get All GetNDocumentType
        /// </summary>
        /// <param name="siteFeatureKey">The NDocumentType.</param>
        /// <returns>JsonResult.</returns>
        public JsonResult GetNDocumentType()
        {
            return Json((from objDocumentTypeObjectModel in documentTypeCacheFactory.GetAllEntities()
                         select new
                         {
                             label = objDocumentTypeObjectModel.Name,
                             title = objDocumentTypeObjectModel.Name,
                             value = objDocumentTypeObjectModel.MarketId
                         }),
                        JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// Get pre selected client documents for selected client document groups
        /// </summary>
        /// <param name="clientDocumentGroupId">The client document group identifier.</param>
        /// <returns>JsonResult.</returns>
        public JsonResult GetPreSelectNDocumentType(int documentTypeId)
        {
            var a = from objObjectModel in documentSubstitutionCacheFactory.GetAllEntities().Where(x => x.Id == documentTypeId)
                    select new
                    {
                        value = objObjectModel.NDocumentType
                    };
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
