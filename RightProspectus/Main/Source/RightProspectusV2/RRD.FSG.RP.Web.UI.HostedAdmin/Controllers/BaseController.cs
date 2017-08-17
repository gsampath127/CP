// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************

using RP.Extensions;
using RRD.FSG.RP.Web.UI.HostedAdmin.Extension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// Added to Provide common behavior across Controller
    /// </summary>
    [RPAuthorize]
    [RPActionFilterAttribute]
    public class BaseController : Controller
    {

        /// <summary>
        /// Gets the base URL.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetBaseUrl()
        {
            return Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~");
        }

        /// <summary>
        /// ClientID to session
        /// </summary>
        /// <value>The session client identifier.</value>
        /// <param name="userLoginID"></param>
        public int SessionClientID
        {
            get
            {

                if (System.Web.HttpContext.Current != null)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(System.Web.HttpContext.Current.Session["CLIENT_ID"])))
                    {
                        return Convert.ToInt32(System.Web.HttpContext.Current.Session["CLIENT_ID"]);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                Session["CLIENT_ID"] = value;
            }
        }

        /// <summary>
        /// UserID to session
        /// </summary>
        /// <value>The session user identifier.</value>
        /// <param name="userLoginID"></param>
        public int SessionUserID
        {
            get
            {

                if (System.Web.HttpContext.Current != null)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(System.Web.HttpContext.Current.Session["USER_ID"])))
                    {
                        return Convert.ToInt32(System.Web.HttpContext.Current.Session["USER_ID"]);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                Session["USER_ID"] = value;
            }
        }



        /// <summary>
        /// ClientName to session
        /// </summary>
        /// <value>The name of the session client.</value>
        /// <param name="userLoginID"></param>
        public string SessionClientName
        {
            get
            {

                if (System.Web.HttpContext.Current != null)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(System.Web.HttpContext.Current.Session["CLIENT_NAME"])))
                    {
                        return Convert.ToString(System.Web.HttpContext.Current.Session["CLIENT_NAME"]);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                Session["CLIENT_NAME"] = value;
            }
        }


        /// <summary>
        /// SiteID to session
        /// </summary>
        /// <value>The session site identifier.</value>
        /// <param name="userLoginID"></param>
        public int SessionSiteID
        {
            get
            {

                if (System.Web.HttpContext.Current != null)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(System.Web.HttpContext.Current.Session["SITE_ID"])))
                    {
                        return Convert.ToInt32(System.Web.HttpContext.Current.Session["SITE_ID"]);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                Session["SITE_ID"] = value;
            }
        }

        // <summary>
        /// <summary>
        /// Gets or sets the name of the session site.
        /// </summary>
        /// <value>The name of the session site.</value>
        public string SessionSiteName
        {
            get
            {

                if (System.Web.HttpContext.Current != null)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(System.Web.HttpContext.Current.Session["SITE_NAME"])))
                    {
                        return Convert.ToString(System.Web.HttpContext.Current.Session["SITE_NAME"]);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                Session["SITE_NAME"] = value;
            }
        }

        /// <summary>
        /// SiteExist to session
        /// </summary>
        /// <value><c>true</c> if [session site exist]; otherwise, <c>false</c>.</value>
        /// <param name="userLoginID"></param>
        public bool SessionSiteExist
        {
            get
            {

                if (System.Web.HttpContext.Current != null)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(System.Web.HttpContext.Current.Session["SITE_EXIST"])))
                    {
                        return Convert.ToBoolean(System.Web.HttpContext.Current.Session["SITE_EXIST"]);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            set
            {
                Session["SITE_EXIST"] = value;
            }
        }

        #region GetUserFromActiveDirectory
        /// <summary>
        /// Searches all AD domains in the Forest for the user and returns UserPrincipal
        /// </summary>
        /// <param name="activeDirectoryID">The active directory identifier.</param>
        /// <returns>UserPrincipal.</returns>
        public UserPrincipal GetUserFromActiveDirectory(string activeDirectoryID)
        {
            return GetUserFromActiveDirectory(activeDirectoryID, null);
        }

        /// <summary>
        /// Searches specified AD domains for the user and returns UserPrincipal
        /// </summary>
        /// <param name="activeDirectoryID">The active directory identifier.</param>
        /// <param name="lstUserDomains">The LST user domains.</param>
        /// <returns>UserPrincipal.</returns>
        public UserPrincipal GetUserFromActiveDirectory(string activeDirectoryID, List<string> lstUserDomains)
        {
            UserPrincipal adUser = null;

            //Get all domains in the forest if none specified
            if (lstUserDomains == null)
            {
                lstUserDomains = new List<string>();

                foreach (Domain d in Forest.GetCurrentForest().Domains)
                {
                    lstUserDomains.Add(d.Name);
                }
            }

            //Find user in listed domains
            foreach (string userDomain in lstUserDomains)
            {
                var context = new PrincipalContext(ContextType.Domain, userDomain);
                if (context != null)
                {
                    adUser = UserPrincipal.FindByIdentity(context, activeDirectoryID);
                    if (adUser != null)
                    {
                        break;
                    }
                }
            }

            return adUser;
        }
        #endregion

        #region RPFormatDate
        /// <summary>
        /// Formats the date
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>System.String.</returns>
        public string RPFormatDate(DateTime dt)
        {
            return dt.ToString(ConfigurationManager.AppSettings["DateFormat"].ToString(), CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Formats the nullable date
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>System.String.</returns>
        public string RPFormatDate(DateTime? dt)
        {
            return (dt == null ? string.Empty : dt.Value.ToString(ConfigurationManager.AppSettings["DateFormat"].ToString(), CultureInfo.InvariantCulture));
        }
        #endregion

        #region SessionTimedOut
        /// <summary>
        /// Session Timed Out
        /// </summary>
        /// <returns>ActionResult.</returns>
        [SkipRPActionFilter]
        [HttpGet]
        public ActionResult SessionTimedOut()
        {
            return View("~/Views/Shared/SessionTimedOut.cshtml");
        }
        #endregion

    }
}