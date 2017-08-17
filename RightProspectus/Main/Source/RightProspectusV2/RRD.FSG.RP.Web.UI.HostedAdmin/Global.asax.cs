// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-27-2015

// ***********************************************************************
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using RRD.FSG.RP.Utilities;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

/// <summary>
/// The HostedAdmin namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin
{
    /// <summary>
    /// Class MvcApplication.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RPV2Resolver.LoadConfiguration();
        }

        /// <summary>
        /// Handles the Error event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (ex is HttpUnhandledException && ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            var code = (ex is HttpException) ? (ex as HttpException).GetHttpCode() : 200;

            ex.Data.Add("ErrorCode", code);
            ex.Data.Add("Priority", "1");
            ex.Data.Add("Severity", "High");
            switch (code)
            {
                case 404:
                    ex.Data.Add("Title", "Resource Not Found");
                    break;
                case 500:
                    ex.Data.Add("Title", "Internal Server Error");
                    break;
                default:
                    ex.Data.Add("Title", "Admin Application Error");
                    break;
            }
            ex.Data.Add("MachineName", HttpContext.Current.Server.MachineName);
            ex.Data.Add("AppDomainName", AppDomain.CurrentDomain.FriendlyName);
            ex.Data.Add("ProcessID", System.Diagnostics.Process.GetCurrentProcess().Id.ToString());
            ex.Data.Add("ProcessName", "HostedAdmin");
            ex.Data.Add("ThreadName", System.Threading.Thread.CurrentThread.Name);
            ex.Data.Add("Win32ThreadId", System.Threading.Thread.CurrentThread.ManagedThreadId);
            ex.Data.Add("EventId", "1");

            ex.Data.Add("Url", HttpContext.Current.Request.Url.OriginalString);
            ex.Data.Add("AbsoluteUrl", HttpContext.Current.Request.Url.AbsoluteUri);


            // Variable to store the ExceptionManager instance. 
            ExceptionManager exManager;

            // Resolve the default ExceptionManager object from the container.
            exManager = EnterpriseLibraryContainer.Current.GetInstance<ExceptionManager>();

            if (ex != null && exManager != null)
            {
                exManager.HandleException(ex, "RPV2GlobalPolicy");

                Server.ClearError();
            }

            var httpContext = HttpContext.Current;
            httpContext.Server.TransferRequest("~/Error/Index?ErrorCode=" + code, true);
        }
    }
}