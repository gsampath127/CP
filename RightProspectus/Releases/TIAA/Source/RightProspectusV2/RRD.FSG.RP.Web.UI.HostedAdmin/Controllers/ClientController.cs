// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
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
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
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
    /// To Hold the Client related actions
    /// </summary>
    public class ClientController : BaseController
    {
        #region Constants
        ///<summary>
        ///Constants 
        ///</summary>
        private const string dropClient = "--Please select Client Connection String Name--";
        private const string dropVerticalMarket = "--Please select Vertical Market--" ;
        private const string defaultdropvalue = "-1";

        #endregion
        /// <summary>
        /// The client cache factory
        /// </summary>
        private IFactoryCache<ClientFactory, ClientObjectModel, int> clientCacheFactory;
        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        /// <summary>
        /// The vertical markets cache factory
        /// </summary>
        private IFactoryCache<VerticalMarketsFactory, VerticalMarketsObjectModel, int> verticalMarketsCacheFactory;
        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;
   

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController" /> class.
        /// </summary>
        public ClientController()
        {
            clientCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientFactory, ClientObjectModel, int>>("Client");
            clientCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
            userCacheFactory.Mode = FactoryCacheMode.All;

            verticalMarketsCacheFactory = RPV2Resolver.Resolve<IFactoryCache<VerticalMarketsFactory, VerticalMarketsObjectModel, int>>("VerticalMarket");
            verticalMarketsCacheFactory.Mode = FactoryCacheMode.All;

            siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
            siteCacheFactory.Mode = FactoryCacheMode.All;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController"/> class.
        /// </summary>
        /// <param name="ClientCacheFactory">The client cache factory.</param>
        /// <param name="UserCacheFactory">The user cache factory.</param>
        /// <param name="VerticalMarketsCacheFactory">The vertical markets cache factory.</param>
        /// <param name="SiteCacheFactory">The site cache factory.</param>
        public ClientController(IFactoryCache<ClientFactory, ClientObjectModel, int> ClientCacheFactory, IFactoryCache<UserFactory, UserObjectModel, int> UserCacheFactory, IFactoryCache<VerticalMarketsFactory, VerticalMarketsObjectModel, int> VerticalMarketsCacheFactory, IFactoryCache<SiteFactory, SiteObjectModel, int> SiteCacheFactory)
        {
            clientCacheFactory = ClientCacheFactory;
            clientCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = UserCacheFactory;
            userCacheFactory.Mode = FactoryCacheMode.All;

            verticalMarketsCacheFactory = VerticalMarketsCacheFactory;
            verticalMarketsCacheFactory.Mode = FactoryCacheMode.All;

            siteCacheFactory = SiteCacheFactory;
            siteCacheFactory.Mode = FactoryCacheMode.All;
        }
        #endregion

        #region View
        /// <summary>
        /// List clients
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult List()
        {
            return View();
        }
        #endregion

        #region Edit
        /// <summary>
        /// Edit Client - GET
        /// </summary>
        /// <param name="id1">The id1.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult EditClient(int id1)
        {
            ClientViewModel objClientViewModel = new ClientViewModel();
            if (id1 > 0)
            {
                ClientObjectModel objClientObjectModel = clientCacheFactory.GetEntityByKey(id1);
                objClientViewModel.ClientID = objClientObjectModel.ClientID;
                objClientViewModel.ClientName = objClientObjectModel.ClientName;
                objClientViewModel.SelectedVerticalMarketId = objClientObjectModel.VerticalMarketId;
                objClientViewModel.ClientDatabaseName = objClientObjectModel.ClientDatabaseName;
                objClientViewModel.SelectedClientConnectionStringName = objClientObjectModel.ClientConnectionStringName;
                objClientViewModel.ClientDescription = objClientObjectModel.ClientDescription;
                objClientViewModel.IsClientDocumentsAvailableFromFTP = objClientObjectModel.IsClientDocumentsAvailableFromFTP;
            }
            else
            {
                if (clientCacheFactory.GetAllEntities().Count() == 0)
                {
                    objClientViewModel.IsClientDocumentsAvailableFromFTP = true;
                }
                
                objClientViewModel.ClientID = 0;
            }


            objClientViewModel.ClientConnectionStringNames = new List<DisplayValuePair>();
            objClientViewModel.ClientConnectionStringNames.Add(new DisplayValuePair { Value = defaultdropvalue, Display = dropClient });

            (from dbo in ConfigValues.GetAllConnectionStringsFromConfig()
             select dbo).Distinct().ToList().ForEach(key =>
            {
                objClientViewModel.ClientConnectionStringNames.Add(new DisplayValuePair { Display = key, Value = key });
            });

            var marketDetails = (from dbo in verticalMarketsCacheFactory.GetAllEntities()
                                 select new { dbo.MarketName, dbo.VerticalMarketId }).Distinct();
            objClientViewModel.VerticalMarket = new List<DisplayValuePair>();
            objClientViewModel.VerticalMarket.Add(new DisplayValuePair { Value = defaultdropvalue, Display = dropVerticalMarket });
            foreach (var item in marketDetails)
            {
                objClientViewModel.VerticalMarket.Add(new DisplayValuePair { Display = item.MarketName, Value = item.VerticalMarketId.ToString() });
            }

            ViewData["Success"] = "In Progress";
            return View(objClientViewModel);
        }


        /// <summary>
        /// Edit Client - POST
        /// </summary>
        /// <param name="objClientViewModel">The object client view model.</param>
        /// <param name="id1">The id1.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult EditClient(ClientViewModel objClientViewModel, int id1)
        {
            ClientObjectModel objClientObjectModel = new ClientObjectModel
            {
                ClientID = (int)(objClientViewModel.ClientID),
                ClientName = objClientViewModel.ClientName,
                VerticalMarketId = objClientViewModel.SelectedVerticalMarketId,
                ClientConnectionStringName = objClientViewModel.SelectedClientConnectionStringName,
                ClientDescription = objClientViewModel.ClientDescription,
                ClientDatabaseName = objClientViewModel.ClientDatabaseName,
                IsClientDocumentsAvailableFromFTP=objClientViewModel.IsClientDocumentsAvailableFromFTP
            };
           
            objClientViewModel.VerticalMarket = new List<DisplayValuePair>();
            objClientViewModel.VerticalMarket.Add(new DisplayValuePair { Value = defaultdropvalue, Display = dropVerticalMarket});

            var marketDetails = (from dbo in verticalMarketsCacheFactory.GetAllEntities()
                                 select new { dbo.MarketName, dbo.VerticalMarketId }).Distinct();
            foreach (var item in marketDetails)
            {
                objClientViewModel.VerticalMarket.Add(new DisplayValuePair { Display = item.MarketName, Value = item.VerticalMarketId.ToString() });
            }

            objClientViewModel.ClientConnectionStringNames = new List<DisplayValuePair>();
            objClientViewModel.ClientConnectionStringNames.Add(new DisplayValuePair { Value = defaultdropvalue, Display =dropClient});

            var connectionStrings = (from dbo in ConfigValues.GetAllConnectionStringsFromConfig()
                                     select dbo).Distinct();
            foreach (string key in connectionStrings)
            {
                objClientViewModel.ClientConnectionStringNames.Add(new DisplayValuePair { Display = key, Value = key });
            }

            List<int> listClientUsers = new List<int>();
            var hdnUsers = Request.Form["hdnUsers"];
            if (hdnUsers != null)
            {
                int result;
                foreach (string userID in hdnUsers.ToString().Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(userID))
                    {
                        if (int.TryParse(userID, out result))
                        {
                            listClientUsers.Add(result);
                        }
                    }
                }
            }
            
            objClientObjectModel.Users = listClientUsers;
            clientCacheFactory.SaveEntity(objClientObjectModel, SessionUserID);
            ViewData["Success"] = "OK";
            return View(objClientViewModel);
        }
        #endregion

        #region GetClientUsers
        /// <summary>
        /// Get the list of users for the client
        /// </summary>
        /// <param name="clientID">The client identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetClientUsers(int clientID)
        {
            return Json
             (
                (
                  from dbo in userCacheFactory.GetAllEntities()
                  select new
                    {
                        Display = dbo.Email,
                        Value = dbo.UserId.ToString(),
                        Selected = clientID > 0 && clientCacheFactory.GetEntityByKey(clientID).Users.Contains(dbo.UserId) ? true : false
                    }
                ),
              JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region WelcomeClient
        /// <summary>
        /// View to welcome client after selecting particular Customer/Client from SelectCustomers page
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <returns>ActionResult.</returns>
        [IsPopUp]
        public ActionResult WelcomeClient(int? clientId, string clientName)
        {
            if (clientId != null)
            {
                SessionClientID = clientId.Value;
                SessionClientName = clientName;
            }
            siteCacheFactory.ClientName = SessionClientName;
            IEnumerable<SiteObjectModel> sites = siteCacheFactory.GetAllEntities();
            if (sites.Count() > 0)
            {
                SessionSiteExist = true;
            }
            ViewData["SelectedClient"] = SessionClientName;
            return View();
        }
        #endregion

        #region GetAllClients
        /// <summary>
        /// Get all clients
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="verticalMarket">The vertical market.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllClients(string clientName, string verticalMarket, string databaseName)
        {
            int i;
            ClientSearchDetail objClientSearchDetail = new ClientSearchDetail()
            {
                ClientName = string.IsNullOrEmpty(clientName) ? null : clientName,
                VerticalMarketId = !(string.IsNullOrEmpty(verticalMarket)) ? (int.TryParse(verticalMarket, out i) ? i : 0) : (int?)null,
                ClientDatabaseName = string.IsNullOrEmpty(databaseName) ? null : databaseName,
                 
            };

            
            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            ClientSortColumn SortColumn = ClientSortColumn.ClientName;
            switch (kendoGridPost.SortColumn)
            {
                case "VerticalMarketName":
                    SortColumn = ClientSortColumn.VerticalMarketName;
                    break;
                case "ClientDatabaseName":
                    SortColumn = ClientSortColumn.ClientDatabaseName;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            return Json(new
            {
                total = clientCacheFactory.GetEntitiesBySearch(objClientSearchDetail).Count(),
                data = (from dbo in clientCacheFactory.GetEntitiesBySearch(startRowIndex, kendoGridPost.PageSize, objClientSearchDetail,
                        new ClientSortDetail() { Column = SortColumn, Order = sortOrder })
                        select new
                        {
                            ClientID = dbo.ClientID,
                            ClientName = dbo.ClientName,
                            VerticalMarketName = dbo.VerticalMarketName,
                            ClientDatabaseName = dbo.ClientDatabaseName,
                            SelectedClientConnectionStringName = dbo.ClientConnectionStringName,
                           
                        })
            });
        }
        #endregion

        #region Load Search Creteria Methods.

        /// <summary>
        /// Gets the client names.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetClientNames()
        {
           return Json
           ((
                 from dbo in clientCacheFactory.GetAllEntities()
                 select new { Display = dbo.ClientName, Value = dbo.ClientName }).OrderBy(client => client.Display).Distinct(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the vertical markets.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetVerticalMarkets()
        {
            return Json
            ((
               from dbo in clientCacheFactory.GetAllEntities()
               select new { Display = dbo.VerticalMarketName, Value = dbo.VerticalMarketId }).OrderBy(client => client.Display).Distinct(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the database names.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetDatabaseNames()
        {
            return Json
            ((
              from dbo in clientCacheFactory.GetAllEntities()
              select new { Display = dbo.ClientDatabaseName }).OrderBy(client => client.Display).Distinct(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Delete Client Details
        /// <summary>
        /// Deletes the client details.
        /// </summary>
        /// <param name="clientID">The client identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DeleteClientDetails(int clientID)
        {
            clientCacheFactory.DeleteEntity(clientID, SessionUserID);
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CheckClientNameAlreadyExists
        /// <summary>
        /// Checks the client name already exists.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckClientNameAlreadyExists(string clientName)
        {
            bool isClientExists = false;

            if (clientCacheFactory.GetEntitiesBySearch(new ClientSearchDetail { ClientName = clientName }).Count() > 0)
            {
                isClientExists = true;
            }
            return Json(isClientExists, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetAllConnectionStringsFromConfig
        /// <summary>
        /// Gets all connection strings from configuration.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetAllConnectionStringsFromConfig()
        {
           return Json
            ((
              from dbo in ConfigValues.GetAllConnectionStringsFromConfig()
              select new { Display = dbo }).Distinct(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}