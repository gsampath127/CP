// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************

using RP.Extensions;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.HostedAdmin.Common;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    /// Class UserController.
    /// </summary>
    public class UserController : BaseController
    {
        #region Properties
        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        /// <summary>
        /// The client cache factory
        /// </summary>
        private IFactoryCache<ClientFactory, ClientObjectModel, int> clientCacheFactory;
        /// <summary>
        /// The role cache factory
        /// </summary>
        private IFactoryCache<RolesFactory, RolesObjectModel, int> roleCacheFactory;
        #endregion

        #region User_Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class.
        /// </summary>
        #endregion

        #region Constructor
        public UserController()
        {        
            userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
            userCacheFactory.Mode = FactoryCacheMode.All;
            clientCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientFactory, ClientObjectModel, int>>("Client");
            clientCacheFactory.Mode = FactoryCacheMode.All;

            roleCacheFactory = RPV2Resolver.Resolve<IFactoryCache<RolesFactory, RolesObjectModel, int>>("Role");
            roleCacheFactory.Mode = FactoryCacheMode.All;
            
        }
        #endregion

        #region User_Test_Constructor
        /// <summary>
        /// User_Test_Constructor
        /// </summary>
        /// <param name="UserFactoryCache">The user factory cache.</param>
        /// <param name="ClientFactoryCache">The client factory cache.</param>
        /// <param name="RoleFactoryCache">The role factory cache.</param>
        public UserController(IFactoryCache<UserFactory, UserObjectModel, int>UserFactoryCache,IFactoryCache<ClientFactory, ClientObjectModel, int> ClientFactoryCache,IFactoryCache<RolesFactory, RolesObjectModel, int> RoleFactoryCache)
        {
            userCacheFactory = UserFactoryCache;
            userCacheFactory.Mode = FactoryCacheMode.All;

            clientCacheFactory = ClientFactoryCache;
            clientCacheFactory.Mode = FactoryCacheMode.All;

            roleCacheFactory = RoleFactoryCache;
            roleCacheFactory.Mode = FactoryCacheMode.All;
        }
#endregion

        #region List
        /// <summary>
        /// List Users
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult List()
        {
            IEnumerable<UserObjectModel> userobjects = userCacheFactory.GetEntitiesBySearch(0, 0, new UserSearchDetail());
            if (userobjects == null)
            { }
            return View();
        }
        #endregion

        #region Action_Edit_Post
        /// <summary>
        /// Edit User - POST
        /// </summary>
        /// <param name="userViewModel">The user view model.</param>
        /// <param name="id1">The id1.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [IsPopUp]
        public ActionResult EditUser(UserViewModel userViewModel, int id1)
        {
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = (int)userViewModel.UserID;
            objUserObjectModel.UserName = userViewModel.UserName;
            objUserObjectModel.FirstName = userViewModel.FirstName;
            objUserObjectModel.LastName = userViewModel.LastName;
            objUserObjectModel.Email = userViewModel.Email;
            objUserObjectModel.EmailConfirmed = true;
            objUserObjectModel.PasswordHash = "Password@123";
            objUserObjectModel.SecurityStamp = Guid.NewGuid().ToString();
            objUserObjectModel.TwoFactorEnabled = true;
            objUserObjectModel.LockoutEnabled = true;
            objUserObjectModel.AccessFailedCount = 0;
            objUserObjectModel.PhoneNumberConfirmed = true;

            if (userViewModel.IsAdmin)
            {
                int roleID = roleCacheFactory.GetEntitiesBySearch(0, 0, new RolesSearchDetail { Name = "Admin" }).FirstOrDefault().RoleId;

                objUserObjectModel.Roles = new List<RolesObjectModel>();
                objUserObjectModel.Roles.Add(new RolesObjectModel { RoleId = roleID });
            }

            List<int> listUserClients = new List<int>();
            if (Request.Form["hdnClients"] != null)
            {
                var arrUserClients = Request.Form["hdnClients"].ToString().Split(',');
                foreach (string clientID in arrUserClients)
                {
                    if (!string.IsNullOrWhiteSpace(clientID))
                    {
                        listUserClients.Add(Convert.ToInt32(clientID));
                    }
                }
            }
            objUserObjectModel.Clients = listUserClients;
            userCacheFactory.SaveEntity(objUserObjectModel, SessionUserID);
            ViewData["Success"] = "OK";
            return View(userViewModel);
        }
        #endregion

        #region UserName
        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetUserName()
        {
           return Json
             ((
                   from dbo in userCacheFactory.GetAllEntities()
                   select new { Display = dbo.UserName}).OrderBy(user=>user.Display).Distinct(), JsonRequestBehavior.AllowGet);
            }

        #endregion

        #region FirstName
        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetFirstName()
        {
           return Json
            ((
                  from dbo in userCacheFactory.GetAllEntities()
                  select new { Display = dbo.FirstName }).OrderBy(user => user.Display).Distinct(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region LastName
        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetLastName()
        {
            return Json
            ((
                  from dbo in userCacheFactory.GetAllEntities()
                  select new { Display = dbo.LastName }).OrderBy(user => user.Display).Distinct(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Email
        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetEmail()
        {

           return Json
           ((
                 from dbo in userCacheFactory.GetAllEntities()
                 select new { Display = dbo.Email }).OrderBy(user => user.Display).Distinct(), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region CheckUserNameAlreadyExists
        /// <summary>
        /// Checks the user name already exists.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult CheckUserNameAlreadyExists(string userName)
        {
            bool isUserExists = false;

            if (userCacheFactory.GetEntitiesBySearch(0, 0, new UserSearchDetail { UserName = userName }).Count() > 0)
            {
                isUserExists = true;
            }
            return Json(isUserExists, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region EditUser_Post
        /// <summary>
        /// Edit User - GET
        /// </summary>
        /// <param name="id1">The id1.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult EditUser(int id1)
        {
            UserViewModel userViewModel = new UserViewModel();
            if (id1 > 0)
            {
                UserObjectModel objUserObjectModel = userCacheFactory.GetEntityByKey(id1);
                userViewModel.UserID = objUserObjectModel.UserId;
                userViewModel.UserName = objUserObjectModel.UserName;
                userViewModel.FirstName = objUserObjectModel.FirstName;
                userViewModel.LastName = objUserObjectModel.LastName;
                userViewModel.Email = objUserObjectModel.Email;


                if (userCacheFactory.GetEntitiesBySearch(0, 0, new UserSearchDetail { UserID = id1 }).FirstOrDefault().Roles.Exists(p => p.RoleName == "Admin"))
                {
                    userViewModel.IsAdmin = true;
                }
                else
                {
                    userViewModel.IsAdmin = false;
                }

  
            }
            else
            {
                userViewModel.UserID = 0;
            }
            ViewData["Success"] = "In Progress";
            return View(userViewModel);
        }
#endregion
        

        #region Disable Page text
        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DeleteUser(int userID)
        {            
            //int? userId = userCacheFactory.GetEntitiesBySearch(0, 0, new UserSearchDetail { UserName = HttpContext.User.Identity.Name }).First().UserId;
            userCacheFactory.DeleteEntity(userID, SessionUserID);           
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetAllUsers
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllUsers(string userName, string firstName, string lastName, string email)
        {
            UserSearchDetail objUserSearchDetail = new UserSearchDetail()
            {                  
                FirstName = string.IsNullOrEmpty(firstName) ? null : firstName
                ,
                UserName = string.IsNullOrEmpty(userName) ? null : userName
                ,
                LastName = string.IsNullOrEmpty(lastName) ? null : lastName
                ,
                Email = string.IsNullOrEmpty(email) ? null : email
            };

           
            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            UserSortColumn SortColumn = UserSortColumn.UserName;
            switch (kendoGridPost.SortColumn)
            {
                case "FirstName":
                    SortColumn = UserSortColumn.FirstName;
                    break;
                case "LastName":
                    SortColumn = UserSortColumn.LastName;
                    break;
                case "Email":
                    SortColumn = UserSortColumn.Email;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            //Return total and data
            return Json(new
            {
                total = userCacheFactory.GetEntitiesBySearch(0, 0, objUserSearchDetail).Count(),
                data = (from dbo in userCacheFactory.GetEntitiesBySearch(startRowIndex, kendoGridPost.PageSize, objUserSearchDetail,
                        new UserSortDetail() { Column = SortColumn, Order = sortOrder })
                        select new { UserID = dbo.UserId, UserName = dbo.UserName, FirstName = dbo.FirstName, LastName = dbo.LastName, Email = dbo.Email })
            });

        }
        #endregion

        #region GetUserClients
        /// <summary>
        /// Get the list of clients for the users
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetUserClients(int userID)
        {

            return Json
              ((
                   from dbo in clientCacheFactory.GetAllEntities()
                   select new { Display = dbo.ClientName,
                            Value = dbo.ClientID.ToString(),
                            Selected = userID > 0 && userCacheFactory.GetEntityByKey(userID).Clients.Contains(dbo.ClientID) ? true : false }
                ), 
               JsonRequestBehavior.AllowGet);


        }
        #endregion

        #region GetUserDetails
        /// <summary>
        /// Get the logged in user name
        /// </summary>
        /// <param name="LoggedInUserID">The logged in user identifier.</param>
        /// <returns>System.String.</returns>
        public JsonResult GetUserDetails(string LoggedInUserID)
        {
            UserObjectModel userObjectModel = new UserObjectModel();
           
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
                        aduser = GetUserFromActiveDirectory(LoggedInUserID, ConfigurationManager.AppSettings["UserDomains"].ToString().Split(',').ToList());
                    }

                    if (aduser != null)
                    {
                        userObjectModel.FirstName = aduser.GivenName;
                        userObjectModel.LastName = aduser.Surname;
                        userObjectModel.Email = aduser.EmailAddress;
                       
                        aduser.Dispose();
                    }
                }
            }
            catch
            {
                return Json(new { FirstName = string.Empty, LastName = string.Empty, Email = string.Empty }, JsonRequestBehavior.AllowGet);

            }

            return Json( userObjectModel , JsonRequestBehavior.AllowGet);
           
        }
        #endregion
    }
}