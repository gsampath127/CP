// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************

using RP.Extensions;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// To Hold the Home actions
    /// </summary>
    public class HomeController : BaseController
    {
        #region Constants
        ///<summary>
        ///Constants
        ///</summary>
        private const string defaultvalue = "-1";
        private const string dropClient = "--Please select Client--";

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
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        public HomeController()
        {
            clientCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientFactory, ClientObjectModel, int>>("Client");
            clientCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
            userCacheFactory.Mode = FactoryCacheMode.All;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="ClientFactoryCache">The client factory cache.</param>
        /// <param name="UserFactoryCache">The user factory cache.</param>
        public HomeController(IFactoryCache<ClientFactory, ClientObjectModel, int> ClientFactoryCache,
            IFactoryCache<UserFactory, UserObjectModel, int> UserFactoryCache)
        {
            clientCacheFactory = ClientFactoryCache;
            clientCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = UserFactoryCache;
            userCacheFactory.Mode = FactoryCacheMode.All;
        }

        /// <summary>
        /// Selects the customer.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [SkipRPActionFilter]
        [HttpGet]
        public ActionResult SelectCustomer()
        {
            SessionUserID = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserName = User.Identity.Name }).FirstOrDefault().UserId;
            SelectCustomerViewModel selectCustomerViewModel = new SelectCustomerViewModel 
            { 
                SelectedCustomerID = defaultvalue, 
                CustomerNames = GetCustomerNames(SessionUserID) 
            };
            ViewBag.UserName = GetLoggedInUserName(User.Identity.Name);
            
            return View(selectCustomerViewModel);
        }

        /// <summary>
        /// Selects the customer.
        /// </summary>
        /// <param name="selectCustomerViewModel">The select customer view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult SelectCustomer(SelectCustomerViewModel selectCustomerViewModel)
        {
            if (!selectCustomerViewModel.SelectedCustomerID.Equals(defaultvalue))
            {
                if (selectCustomerViewModel.SelectedCustomerID.Equals("ADMIN"))
                {
                    return RedirectToAction("List", "Client");
                }

                SessionClientID = int.Parse(selectCustomerViewModel.SelectedCustomerID);
                SessionClientName = clientCacheFactory.GetAllEntities().Where(p => p.ClientID == int.Parse(selectCustomerViewModel.SelectedCustomerID)).FirstOrDefault().ClientName;

                base.SessionSiteExist = false;
                return RedirectToAction("WelcomeClient", "Client");
            }

            selectCustomerViewModel.CustomerNames = GetCustomerNames(SessionUserID);
            ViewBag.UserName = GetLoggedInUserName(User.Identity.Name);
            return View(selectCustomerViewModel);
        }


        /// <summary>
        /// Get the model
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>List&lt;DisplayValuePair&gt;.</returns>
        private List<DisplayValuePair> GetCustomerNames(int userID)
        {
            List<DisplayValuePair> lstclientDetails = new List<DisplayValuePair>();
            lstclientDetails.Add(new DisplayValuePair { Value = defaultvalue, Display = dropClient });
            if (userCacheFactory.GetEntitiesBySearch(new UserSearchDetail { UserID = SessionUserID }).FirstOrDefault().Roles.Exists(p => p.RoleName == "Admin"))
            {
                lstclientDetails.Add(new DisplayValuePair { Value = "ADMIN", Display = "ADMIN" });
            }
            foreach (ClientObjectModel client in clientCacheFactory.GetAllEntities().Where(p => p.Users.Contains(userID)))
            {
                lstclientDetails.Add(new DisplayValuePair { Value = client.ClientID.ToString(), Display = client.ClientName });
            }

            return lstclientDetails;
        }

        #region GetLoggedInUserName
        /// <summary>
        /// Get the logged in user name
        /// </summary>
        /// <param name="LoggedInUserID">The logged in user identifier.</param>
        /// <returns>System.String.</returns>
        private string GetLoggedInUserName(string LoggedInUserID)
        {
            string loggedInUserName = string.Empty;

            try
            {
                AuthenticationSection authSection = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");

                //Internal User  - Windows Authentication - Get User name from AD
                if (authSection.Mode.Equals(System.Web.Configuration.AuthenticationMode.Windows))
                {
                    UserPrincipal aduser;
                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["FindUserAllDomains"]))
                    {
                        aduser = GetUserFromActiveDirectory(LoggedInUserID);
                    }
                    else
                    {
                        aduser = GetUserFromActiveDirectory(LoggedInUserID, ConfigurationManager.AppSettings["UserDomains"].Split(',').ToList());
                    }

                    if (aduser != null)
                    {
                        loggedInUserName = aduser.GivenName;
                        aduser.Dispose();
                    }
                }
            }
            catch
            {
                UserObjectModel userObjectModel = userCacheFactory.GetEntitiesBySearch(new UserSearchDetail() { UserName = LoggedInUserID }).FirstOrDefault();
                if (userObjectModel != null)
                {
                    loggedInUserName = userObjectModel.FirstName;
                }
            }

            return loggedInUserName;
        }
        #endregion

    }
}