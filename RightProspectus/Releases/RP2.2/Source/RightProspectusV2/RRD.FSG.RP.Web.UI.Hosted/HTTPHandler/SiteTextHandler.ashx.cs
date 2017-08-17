using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Model.Interfaces.HostedPages;

namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Summary description for SiteText
    /// </summary>
    public class SiteTextHandler : IHttpHandler
    {
        /// <summary>
        /// The hosted client page scenarios
        /// </summary>
        private IHostedClientPageScenariosFactory hostedClientPageScenarios;
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteTextHandler"/> class.
        /// </summary>
        public SiteTextHandler()
        {
            this.hostedClientPageScenarios = RPV2Resolver.Resolve<IHostedClientPageScenariosFactory>();
        }
        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            string ClientName = string.Empty, SiteName = string.Empty, ResourceKey = string.Empty;
            bool IsCurrentProductionVersion = true;           
            

            if (!string.IsNullOrWhiteSpace(context.Request.QueryString["ClientName"]))
                ClientName = context.Request.QueryString["ClientName"].ToString().Trim();

            if (!string.IsNullOrWhiteSpace(context.Request.QueryString["SiteName"]))
                SiteName = context.Request.QueryString["SiteName"].ToString().Trim();

            if (!string.IsNullOrWhiteSpace(context.Request.QueryString["Key"]))
                ResourceKey = context.Request.QueryString["Key"].ToString().Trim();

            if (!string.IsNullOrWhiteSpace(context.Request.QueryString["IsProofing"]) && context.Request.QueryString["IsProofing"].ToString().Trim() == "1")
            {
                IsCurrentProductionVersion = false;
            }
                

            if (!string.IsNullOrEmpty(ClientName) && !string.IsNullOrEmpty(ResourceKey))
            {
                if (!string.IsNullOrWhiteSpace(ClientName))
                {                    
                    var siteText = hostedClientPageScenarios.GetSiteTextFromCache(ClientName, SiteName);

                    string cssText = string.Empty;

                    var cssTextData = siteText.Find(x => x.ResourceKey == ResourceKey
                                                && x.IsCurrentProductionVersion == IsCurrentProductionVersion);
                    if (cssTextData != null)
                    {
                        cssText = cssTextData.Text;
                    }
                    context.Response.ContentType = "text/css";
                    context.Response.Write(cssText);
                    context.ApplicationInstance.CompleteRequest();
                }
            }

        }

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}