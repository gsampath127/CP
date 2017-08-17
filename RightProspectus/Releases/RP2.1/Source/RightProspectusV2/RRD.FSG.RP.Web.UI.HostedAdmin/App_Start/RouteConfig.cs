// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

using System.Web.Mvc;
using System.Web.Routing;

/// <summary>
/// The HostedAdmin namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin
{
    /// <summary>
    /// Class RouteConfig.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");

            routes.MapMvcAttributeRoutes();
                       
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id1}/{id2}",
                defaults: new { controller = "Home", action = "SelectCustomer", id1 = UrlParameter.Optional, id2 = UrlParameter.Optional }
            );
        }
    }
}