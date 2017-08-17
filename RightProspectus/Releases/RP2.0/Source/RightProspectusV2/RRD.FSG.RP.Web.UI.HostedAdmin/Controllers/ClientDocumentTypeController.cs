// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : NI317175
// Created          : 10-20-2015
//
// Last Modified By : NI317175
// Last Modified On : 11-18-2015
// ***********************************************************************
// <copyright file="ClientDocumentTypeController.cs" company="RR Donnelley">
//     Copyright © RR Donnelley 2015
// </copyright>
// <summary></summary>
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

namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{

    /// <summary>
    /// Class ClientDocumentTypeController.
    /// </summary>
    public class ClientDocumentTypeController : BaseController
    {
        #region Constants
        /// <summary>
        /// Constants
        /// </summary>
        private const string Description = "Description";
        private const string ModifiedDate = "ModifiedDate";
        private const string ModifiedBy = "ModifiedBy";
        #endregion

        #region Properties
        /// <summary>
        /// The client document type cache factory
        /// </summary>
        private IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int> clientDocumentTypeCacheFactory;
        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;

        /// <summary>
        /// The client document cache factory
        /// </summary>
        private IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int> clientDocumentCacheFactory;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDocumentTypeController" /> class.
        /// </summary>
        public ClientDocumentTypeController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                clientDocumentTypeCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int>>("ClientDocumentType");
                clientDocumentTypeCacheFactory.ClientName = SessionClientName;
                clientDocumentTypeCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;

                clientDocumentCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int>>("ClientDocument");
                clientDocumentCacheFactory.ClientName = SessionClientName;
                clientDocumentCacheFactory.Mode = FactoryCacheMode.All;
            }
        }
        #endregion

        #region Constructor_Test
        /// <summary>
        /// Test_Constructor
        /// </summary>
        /// <param name="ClientDocTypeFactoryCache">The client document type factory cache.</param>
        /// <param name="UserFactoryCache">The user factory cache.</param>
        /// <param name="ClientDocumentFactoryCache">The client document factory cache.</param>
        public ClientDocumentTypeController(IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int> ClientDocTypeFactoryCache, IFactoryCache<UserFactory, UserObjectModel, int> UserFactoryCache, IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int> ClientDocumentFactoryCache)
        {
            clientDocumentTypeCacheFactory = ClientDocTypeFactoryCache;
            clientDocumentTypeCacheFactory.ClientName = SessionClientName;
            clientDocumentTypeCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = UserFactoryCache;
            userCacheFactory.Mode = FactoryCacheMode.All;

            clientDocumentCacheFactory = ClientDocumentFactoryCache;
            clientDocumentCacheFactory.ClientName = SessionClientName;
            clientDocumentCacheFactory.Mode = FactoryCacheMode.All;
        }
        #endregion

        #region View
        /// <summary>
        /// Lists action method.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult List()
        {
            ViewData["SelectedCustomer"] = SessionClientName;
            return View();
        }
        #endregion

        #region GetAllClientDocumentTypeDetails
        /// <summary>
        /// Gets all client document type details.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllClientDocumentTypeDetails(string name, string description)
        {   
            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            ClientDocumentTypeSortColumn SortColumn = ClientDocumentTypeSortColumn.Name;
            switch (kendoGridPost.SortColumn)
            {
                case Description:
                    SortColumn = ClientDocumentTypeSortColumn.Description;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            return Json(new
            {
                total = clientDocumentTypeCacheFactory.GetEntitiesBySearch(new ClientDocumentTypeSearchDetail() { Name = string.IsNullOrEmpty(name) ? null : name, Description = string.IsNullOrEmpty(description) ? null : description }).Select(p => p.ClientDocumentTypeId).Count(),
                data = (from objectModel in clientDocumentTypeCacheFactory.GetEntitiesBySearch(startRowIndex, kendoGridPost.PageSize,
                          new ClientDocumentTypeSearchDetail() { Name = string.IsNullOrEmpty(name) ? null : name, Description = string.IsNullOrEmpty(description) ? null : description },
                          new ClientDocumentTypeSortDetail() { Column = SortColumn, Order = sortOrder })
                        select new
                        {
                            Name = objectModel.Name,
                            Description = objectModel.Description,
                            ClientDocumentTypeId = objectModel.ClientDocumentTypeId
                        })
            });
        }
        #endregion

        #region Edit
        /// <summary>
        /// Edits the type of the client document.
        /// </summary>
        /// <param name="id1">The id1.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult EditClientDocumentType(int id1)
        {
            ClientDocumentTypeViewModel objViewModel = new ClientDocumentTypeViewModel();
            if (id1 != 0)
            {   
                ClientDocumentTypeObjectModel objObjectModel = clientDocumentTypeCacheFactory.GetAllEntities().Where(param => param.ClientDocumentTypeId == id1).FirstOrDefault();
                objViewModel.ClientDocumentTypeId = objObjectModel.ClientDocumentTypeId;
                objViewModel.Name = objObjectModel.Name;
                objViewModel.Description = objObjectModel.Description;  
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

            ViewData["Success"] = "In Progress";
            return View(objViewModel);
        }


        /// <summary>
        /// Edits the type of the client document.
        /// </summary>
        /// <param name="objViewModel">The object view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult EditClientDocumentType(ClientDocumentTypeViewModel objViewModel)
        {
            try
            {
                ClientDocumentTypeObjectModel objObjectModel = new ClientDocumentTypeObjectModel
                {
                    ClientDocumentTypeId = objViewModel.ClientDocumentTypeId,
                    Name = objViewModel.Name,
                    Description = objViewModel.Description
                };

                clientDocumentTypeCacheFactory.SaveEntity(objObjectModel, SessionUserID);

                ViewData["Success"] = "OK";
            }
            catch (Exception exception)
            {
                objViewModel.SuccessOrFailedMessage = exception.Message;
            }
            return View(objViewModel);

        }
        #endregion

        #region Load Search Creteria Methods.

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetName()
        {
            return Json((from dbo in clientDocumentTypeCacheFactory.GetAllEntities()
                         select new { Display = dbo.Name }).OrderBy(Name => Name.Display).Distinct(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetDescription()
        {
            return Json((from dbo in clientDocumentTypeCacheFactory.GetAllEntities()
                         select new { Display = dbo.Description }).OrderBy(Description => Description.Display).Distinct(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Disable Client Document Type
        /// <summary>
        /// Deletes the type of the client document.
        /// </summary>
        /// <param name="clientDocumentTypeId">The client document type identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DeleteClientDocumentType(int clientDocumentTypeId)
        {           
            clientDocumentTypeCacheFactory.DeleteEntity(clientDocumentTypeId, SessionUserID);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
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
            if ((from cd in clientDocumentTypeCacheFactory.GetEntitiesBySearch(new ClientDocumentTypeSearchDetail() { Name = string.IsNullOrEmpty(name) ? null : name, Description = string.IsNullOrEmpty(description) ? null : description })
                 where cd.ClientDocumentTypeId != Convert.ToInt32(currentID)
                 select cd).Count() > 0)
            {
                isDataExists = true;
            }
            

            return Json(isDataExists, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /// <summary>
        /// Validates the delete.
        /// </summary>
        /// <param name="clientDocumentTypeId">The client document type identifier.</param>
        /// <returns>JsonResult.</returns>
        public JsonResult ValidateDelete(int clientDocumentTypeId)
        {
            return Json(clientDocumentCacheFactory.GetEntitiesBySearch(new ClientDocumentSearchDetail() { ClientDocumentTypeId = clientDocumentTypeId }).Count() > 0
                , JsonRequestBehavior.AllowGet);
        }
    }
}
