// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

using RRD.FSG.RP.Model.Interfaces.HostedPages;
using RRD.FSG.RP.Utilities;
using System;
using System.IO;
using System.Web;

/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class StaticResourceModule.
    /// </summary>
    public class StaticResourceModule : IHttpModule
    {
        /// <summary>
        /// The hosted client page scenarios
        /// </summary>
        private IHostedClientPageScenariosFactory hostedClientPageScenarios;

        /// <summary>
        /// Constructor for StaticResourceModule
        /// </summary>
        public StaticResourceModule()
        {
            RPV2Resolver.LoadConfiguration();
            this.hostedClientPageScenarios = RPV2Resolver.Resolve<IHostedClientPageScenariosFactory>();
        }
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// Example: http://localhost:54346/staticresource/forethought_logo.jpg?client=forethought
        /// </summary>
        #region IHttpModule Members
        public void Dispose()
        {
            //clean-up code here.
        }


        /// <summary>
        /// Init Method
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }
        #endregion

        /// <summary>
        /// Handles the BeginRequest event of the context control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication context = sender as HttpApplication;
            if(context.Request.Path.ToUpper().Contains("STATICRESOURCE"))
            {
                string fileName = Path.GetFileName(context.Request.Path);
                string clientName = context.Request.QueryString["client"];

                var hostedStaticRes = hostedClientPageScenarios.GetStaticResourcesFromCache(clientName).Find(x => x.FileName == fileName);
                if (hostedStaticRes != null)
                {
                    context.Response.Clear();
                    context.Response.ContentType = hostedStaticRes.MimeType;

                    DateTime lastModifiedDate = hostedStaticRes.UtcModifiedDate;
                    if (IsClientCached(context.Request, lastModifiedDate))
                    {
                        context.Response.StatusCode = 304;
                        context.CompleteRequest();
                    }
                    context.Response.Cache.SetLastModified(lastModifiedDate);
                    context.Response.BinaryWrite(hostedStaticRes.Data);
                    context.CompleteRequest();
                }
            }
        }

        /// <summary>
        /// Determines whether [is client cached] [the specified request].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="contentModified">The content modified.</param>
        /// <returns><c>true</c> if [is client cached] [the specified request]; otherwise, <c>false</c>.</returns>
        private bool IsClientCached(HttpRequest request, DateTime contentModified)
        {
            string header = request.Headers["If-Modified-Since"];

            if (header != null)
            {
                DateTime isModifiedSince;
                if (DateTime.TryParse(header, out isModifiedSince))
                {
                    if (isModifiedSince >= contentModified)
                        return true;
                }
            }

            return false;
        } 
    }
}
