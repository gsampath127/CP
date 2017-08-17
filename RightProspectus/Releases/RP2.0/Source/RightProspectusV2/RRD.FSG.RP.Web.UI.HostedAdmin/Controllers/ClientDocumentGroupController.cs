// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
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
using System.Linq;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// Class ClientDocumentGroupController.
    /// </summary>
    public class ClientDocumentGroupController : BaseController
    {
        #region Constants
        ///<summary>
        ///Constants
        /// </summary>
        private const string DocumentGroup = "--Please select Document Group--";
        #endregion

        #region Properties

        /// <summary>
        /// The client document group cache factory
        /// </summary>
        private IFactoryCache<ClientDocumentGroupFactory, ClientDocumentGroupObjectModel, int> clientDocumentGroupCacheFactory;

        /// <summary>
        /// The client document cache factory
        /// </summary>
        private IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int> clientDocumentCacheFactory;
        
        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDocumentGroupController" /> class.
        /// </summary>
        public ClientDocumentGroupController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                clientDocumentGroupCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientDocumentGroupFactory, ClientDocumentGroupObjectModel, int>>("ClientDocumentGroup");
                clientDocumentGroupCacheFactory.ClientName = SessionClientName;
                clientDocumentGroupCacheFactory.Mode = FactoryCacheMode.All;

                clientDocumentCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int>>("ClientDocument");
                clientDocumentCacheFactory.ClientName = SessionClientName;
                clientDocumentCacheFactory.Mode = FactoryCacheMode.All;

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;
            }
        }

        #endregion
        
        #region Constructor_Test
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDocumentGroupController" /> class.
        /// </summary>
        /// <param name="ClientDocumentGroupCacheFactory">The client document group cache factory.</param>
        /// <param name="ClientDocumentCacheFactory">The client document cache factory.</param>
        /// <param name="UserCacheFactory">The user cache factory.</param>
        public ClientDocumentGroupController(IFactoryCache<ClientDocumentGroupFactory, ClientDocumentGroupObjectModel, int> ClientDocumentGroupCacheFactory, IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int> ClientDocumentCacheFactory, IFactoryCache<UserFactory, UserObjectModel, int> UserCacheFactory)
        {
            clientDocumentGroupCacheFactory = ClientDocumentGroupCacheFactory;
            clientDocumentGroupCacheFactory.ClientName = SessionClientName;
            clientDocumentGroupCacheFactory.Mode = FactoryCacheMode.All;
            
            clientDocumentCacheFactory = ClientDocumentCacheFactory;
            clientDocumentCacheFactory.ClientName = SessionClientName;
            clientDocumentCacheFactory.Mode = FactoryCacheMode.All;
            
            userCacheFactory = UserCacheFactory;
            userCacheFactory.Mode = FactoryCacheMode.All;
        }
        #endregion

        #region List
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

        #region GetAllClientDocumentGroupDetails

        /// <summary>
        /// Gets all client document group details.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parentClientDocumentGroupId">Parent client document group Id.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllClientDocumentGroupDetails(string name, string parentClientDocumentGroupId)
        {
            int id;
            ClientDocumentGroupSearchDetail objClientDocumentGroupSearchDetail = new ClientDocumentGroupSearchDetail()
            {
                Name = string.IsNullOrEmpty(name) ? null : name,
                ParentClientDocumentGroupId = int.TryParse(parentClientDocumentGroupId, out id) ? id : (int?)null
            };

            
            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;
            ClientDocumentGroupSortColumn SortColumn = ClientDocumentGroupSortColumn.Name;
            switch (kendoGridPost.SortColumn)
            {
                case "Name":
                    SortColumn = ClientDocumentGroupSortColumn.Name;
                    break;
                case "Description":
                    SortColumn = ClientDocumentGroupSortColumn.Description;
                    break;
            }
            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            return Json(new
            {
                total = clientDocumentGroupCacheFactory.GetEntitiesBySearch(objClientDocumentGroupSearchDetail).Select(p => p.ClientDocumentGroupId).Count(),
                data = (from objectModel in
                            clientDocumentGroupCacheFactory.GetEntitiesBySearch(
                                                                                  startRowIndex,
                                                                                  kendoGridPost.PageSize,
                                                                                  objClientDocumentGroupSearchDetail,
                                                                                  new ClientDocumentGroupSortDetail() { Column = SortColumn, Order = sortOrder })
                        select new
                        {
                            Name = objectModel.Name,
                            ClientDocumentGroupId = objectModel.ClientDocumentGroupId,
                            Description = objectModel.Description,
                            CssClass = objectModel.CssClass
                        })
            });
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
            return Json((from key in clientDocumentGroupCacheFactory.GetAllEntities()
                         select new { Display = key.Name, Value = key.Name }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetParent()
        {
            IEnumerable<ClientDocumentGroupObjectModel> totalClientDocumentGroupNameDetails = clientDocumentGroupCacheFactory.GetAllEntities();
            IEnumerable<int?> parentClientDocumentGroupId = totalClientDocumentGroupNameDetails.Where(x => x.ParentClientDocumentGroupId != null).Select(x => x.ParentClientDocumentGroupId).Distinct();

            return Json((from key in totalClientDocumentGroupNameDetails.Where(x => parentClientDocumentGroupId.Contains(x.ClientDocumentGroupId))
                         select new { Display = key.Name, Value = key.ClientDocumentGroupId.ToString() }), JsonRequestBehavior.AllowGet);
        
        }

        #endregion

        #region Edit

        /// <summary>
        /// Edits the specified id1.
        /// </summary>
        /// <param name="id1">The id1.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult Edit(int id1)
        {
            ClientDocumentGroupViewModel objViewModel = new ClientDocumentGroupViewModel();
            objViewModel.ParentClientDocumentGroup = new List<DisplayValuePair>();
            objViewModel.ParentClientDocumentGroup.Add(new DisplayValuePair { Value = null, Display = DocumentGroup });

            IEnumerable<ClientDocumentGroupObjectModel> clientDocumentGroupDetails = clientDocumentGroupCacheFactory.GetAllEntities();
            if (clientDocumentGroupDetails != null)
            {
                foreach (ClientDocumentGroupObjectModel clientDocumentGroupObjectModel in clientDocumentGroupDetails)
                {
                    objViewModel.ParentClientDocumentGroup.Add(new DisplayValuePair { Display = clientDocumentGroupObjectModel.Name, Value = clientDocumentGroupObjectModel.ClientDocumentGroupId.ToString() });
                }
            }
            
            if (id1 != 0)
            {   
                ClientDocumentGroupObjectModel objObjectModel = (from dbo in clientDocumentGroupCacheFactory.GetAllEntities()
                                                                 where dbo.ClientDocumentGroupId == id1
                                                                 select dbo).FirstOrDefault();
                objViewModel.ClientDocumentGroupId = objObjectModel.ClientDocumentGroupId;
                objViewModel.Name = objObjectModel.Name;
                objViewModel.Description = objObjectModel.Description;
                objViewModel.SelectedClientDocumentGroupId = objObjectModel.ParentClientDocumentGroupId == null ? -1 : (int)objObjectModel.ParentClientDocumentGroupId;
                objViewModel.CssClass = objObjectModel.CssClass;
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
                objViewModel.SelectedClientDocumentGroupId = -1;
            }

            ViewData["Success"] = "In Progress";
            return View(objViewModel);
        }

        /// <summary>
        /// Edits the specified object view model.
        /// </summary>
        /// <param name="objViewModel">The object view model.</param>
        /// <param name="selectedClientDocuments">The selected client documents.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult Edit(ClientDocumentGroupViewModel objViewModel, string selectedClientDocuments)
        {
            try
            {
                int id;
                if (int.TryParse(Request.Form["ClientDocumentGroupId"], out id))
                {
                    ClientDocumentGroupObjectModel objObjectModel = new ClientDocumentGroupObjectModel()
                    {
                        ClientDocumentGroupId = id,
                        ParentClientDocumentGroupId = objViewModel.SelectedClientDocumentGroupId,
                        Name = objViewModel.Name,
                        Description = objViewModel.Description,
                        CssClass = objViewModel.CssClass
                    };
                    objObjectModel.ClientDocuments = new List<ClientDocumentObjectModel>();
                    if (!string.IsNullOrEmpty(selectedClientDocuments))
                    {
                        foreach (var item in selectedClientDocuments.Trim().Split(','))
                        {
                            objObjectModel.ClientDocuments.Add(new ClientDocumentObjectModel() { ClientDocumentId = Convert.ToInt32(item), Order = 1 });
                        }
                    }

                    IEnumerable<ClientDocumentGroupObjectModel> clientDocumentGroupDetails = clientDocumentGroupCacheFactory.GetAllEntities();
                    objViewModel.ParentClientDocumentGroup = new List<DisplayValuePair>();
                    if (clientDocumentGroupDetails != null)
                    {
                        foreach (ClientDocumentGroupObjectModel clientDocumentGroupObjectModel in clientDocumentGroupDetails)
                        {
                            objViewModel.ParentClientDocumentGroup.Add(new DisplayValuePair { Display = clientDocumentGroupObjectModel.Name, Value = clientDocumentGroupObjectModel.ClientDocumentGroupId.ToString() });
                        }
                    }

                    clientDocumentGroupCacheFactory.SaveEntity(objObjectModel, SessionUserID);
                    ViewData["Success"] = "OK";
                }
            }
            catch (Exception exception)
            {
                objViewModel.SuccessOrFailedMessage = exception.Message;
            }
            return View(objViewModel);

        }

        #endregion

        #region MultiSelectDropDownViewModel
        /// <summary>
        /// Get client documents for all client document groups
        /// </summary>
        /// <returns>JsonResult.</returns>
        public JsonResult GetAllClientDocuments()
        {
            return Json((from clientDocumentObjectModel in clientDocumentCacheFactory.GetAllEntities()
                         select new
                         {
                             label = clientDocumentObjectModel.Name,
                             title = clientDocumentObjectModel.Name,
                             value = clientDocumentObjectModel.ClientDocumentId.ToString()
                         }),
                         JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get pre selected client documents for selected client document groups
        /// </summary>
        /// <param name="clientDocumentGroupId">The client document group identifier.</param>
        /// <returns>JsonResult.</returns>
        public JsonResult GetPreSelectClientDocuments(int clientDocumentGroupId)
        {
            return Json((from clientDocumentDetails in (clientDocumentGroupCacheFactory.GetAllEntities().Where(x => x.ClientDocumentGroupId == clientDocumentGroupId)).Select(x => x.ClientDocuments).FirstOrDefault()
                         select new
                         {
                             value = clientDocumentDetails.ClientDocumentId.ToString()
                         }),
                         JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CheckNameAlreadyExists
        /// <summary>
        /// Checks the data already exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckDataAlreadyExists(string name)
        {
            bool isDataExists = false;
            int count = clientDocumentGroupCacheFactory.GetEntitiesBySearch(new ClientDocumentGroupSearchDetail() { Name = name }).Count();
            if (count > 0)
            {
                isDataExists = true;
            }
            return Json(isDataExists, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Disable Client Document Group

        /// <summary>
        /// Deletes the client document group.
        /// </summary>
        /// <param name="clientDocumentGroupId">The client document group identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DeleteClientDocumentGroup(int clientDocumentGroupId)
        {
            clientDocumentGroupCacheFactory.DeleteEntity(clientDocumentGroupId, SessionUserID);
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}