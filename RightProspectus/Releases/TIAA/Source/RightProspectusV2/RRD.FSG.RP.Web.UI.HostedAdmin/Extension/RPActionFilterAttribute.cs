// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 11-09-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;


/// <summary>
/// The Extensions namespace.
/// </summary>
namespace RP.Extensions
{
    /// <summary>
    /// Class RPActionFilterAttribute. This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public sealed class RPActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //CHECK IF ACTION FILTER HAS TO BE SKIPPED
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(SkipRPActionFilterAttribute), false).Any())
            {
                return;
            }

            //SESSION CHECK 
            if (((BaseController)(((ControllerContext)(filterContext)).Controller)).SessionUserID == 0)
            {
                
                filterContext.Controller.TempData["SessionTimeOutMessage"] = "Session Timed Out!";

                if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(IsPopUpAttribute), false).Any())
                {
                    filterContext.Result = new RedirectResult("~/Base/SessionTimedOut");
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Home/SelectCustomer");
                }
                return;
            }

            //ADD COMMON LOGIC THAT HAS TO BE EXECUTED ACROSS RPV2 ACTIONS HERE

            //BASE CALL
            base.OnActionExecuting(filterContext);
        }
    }
}