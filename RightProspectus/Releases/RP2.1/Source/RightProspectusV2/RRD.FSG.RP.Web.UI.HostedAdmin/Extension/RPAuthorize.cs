// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-17-2015
// ***********************************************************************

using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Utilities;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;

/// <summary>
/// The Extension namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Extension
{
    /// <summary>
    /// Class RPAuthorize. This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class RPAuthorize : AuthorizeAttribute
    {

        #region OnAuthorization
        /// <summary>
        /// On Authorization
        /// </summary>
        /// <param name="filterContext">The filter context, which encapsulates information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />.</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            AuthenticationSection authSection = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");
            if (authSection.Mode.Equals(System.Web.Configuration.AuthenticationMode.None))
            {
                base.OnAuthorization(filterContext);
            }

            if (filterContext.HttpContext.Request != null && filterContext.HttpContext.Request.IsAuthenticated)
            {
                bool isValidUser = IsValidUser(filterContext.HttpContext.User.Identity.Name);

                if (!isValidUser)
                {
                    var errorView = new ViewResult();
                    errorView.ViewName = "~/Views/Error/InvalidUser.cshtml";
                    errorView.ViewBag.Message = filterContext.HttpContext.User.Identity.Name + " is not a valid user.";
                    filterContext.Result = errorView;
                }
            }
        }
        #endregion

        /// <summary>
        /// Checks if user is valid based on windows identity name
        /// </summary>
        /// <param name="windowsIdentityName">Name of the windows identity.</param>
        /// <returns><c>true</c> if [is valid user] [the specified windows identity name]; otherwise, <c>false</c>.</returns>
        private bool IsValidUser(string windowsIdentityName)
        {
            IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
            userCacheFactory.Mode = FactoryCacheMode.All;
            return userCacheFactory.GetEntitiesBySearch(0, 0, new UserSearchDetail() { UserName = windowsIdentityName }).Count() > 0;
        }
    }
}