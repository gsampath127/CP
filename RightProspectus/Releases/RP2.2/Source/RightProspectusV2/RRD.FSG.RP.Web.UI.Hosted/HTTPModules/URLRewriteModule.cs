// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           :
// Created          : 10-05-2015
//
// Last Modified By :
// Last Modified On : 11-17-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// httpModule to rewrite RP V1 urls to RP V2 url
    /// </summary>
    public class URLRewriteModule : IHttpModule
    {
        /// <summary>
        /// The URL rewrite cache factory
        /// </summary>
        private IFactoryCache<UrlRewriteFactory, UrlRewriteObjectModel, int> urlRewriteCacheFactory;
        /// <summary>
        /// The client cache factory
        /// </summary>
        private IFactoryCache<ClientFactory, ClientObjectModel, int> clientCacheFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="URLRewriteModule"/> class.
        /// </summary>
        public URLRewriteModule()
        {
            urlRewriteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UrlRewriteFactory, UrlRewriteObjectModel, int>>("UrlRewrite");
            clientCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientFactory, ClientObjectModel, int>>("Client");
            clientCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        /// <summary>
        /// Handles the BeginRequest event of the context control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication context = sender as HttpApplication;

            context.Context.Trace.Write("Trace Begin");

            string strClientName = string.Empty;
            if (context != null && context.Request != null)
            {
                string strAbsoluteUri = context.Request.Url.AbsoluteUri;
                string[] urlSegments = context.Request.Url.Segments;

                context.Context.Trace.Write("AbsoluteUri: " + strAbsoluteUri);

                if (ConfigurationManager.AppSettings["hostedBaseUrl"] != null && !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["hostedBaseUrl"].ToString()))
                {
                    if (strAbsoluteUri.Contains(ConfigurationManager.AppSettings["hostedBaseUrl"].ToString()))
                    {
                        if (urlSegments.Length > 1)
                        {
                            if (ConfigurationManager.AppSettings["hostedBaseUrl"].ToString().Contains(urlSegments[1].ToString().Replace("/", string.Empty).Trim()))
                            {
                                context.Context.Trace.Write("Virtual Directory: " + urlSegments[1].ToString().Replace("/", string.Empty).Trim());
                                if (urlSegments.Length > 2)
                                    strClientName = urlSegments[2].ToString().Replace("/", string.Empty).Trim();                                
                            }
                            else
                                strClientName = urlSegments[1].ToString().Replace("/", string.Empty).Trim();

                            context.Context.Trace.Write("Client Name Exist: " + strClientName);
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(strClientName))
                {
                    context.Context.Trace.Write("Verify Client DNS name to get client name");

                    var Client = clientCacheFactory.GetAllEntities().Where(c => c.ClientDnsList.Where(d => strAbsoluteUri.Contains(d.Dns)).Count() > 0).FirstOrDefault();
                    if (Client != null)
                        strClientName = Client.ClientName;
                }

                if(!string.IsNullOrWhiteSpace(strClientName) && isValidclient(strClientName))
                { 
                    string redirectURL = string.Empty;

                    context.Context.Trace.Write("Absolute URL : " + strAbsoluteUri); 

                    urlRewriteCacheFactory.ClientName = strClientName;
                    urlRewriteCacheFactory.Mode = Model.Cache.FactoryCacheMode.All;
                    IEnumerable<UrlRewriteObjectModel> urlRewritePatters = urlRewriteCacheFactory.GetAllEntities();

                    context.Context.Trace.Write("Count of Patterns : " + urlRewritePatters.Count().ToString());                    

                    foreach (UrlRewriteObjectModel urlPattern in urlRewritePatters)
                    {
                        if (Regex.Match(strAbsoluteUri, urlPattern.MatchPattern, RegexOptions.IgnoreCase).Success)
                        {                            
                            context.Context.Trace.Write("Matching Pattern: " + urlPattern.MatchPattern);

                            redirectURL = urlPattern.RewriteFormat;
                            context.Context.Trace.Write("Rewrite Format: " + redirectURL);

                            string[] queryStringValues = context.Request.Url.Query.Trim().Replace("?", string.Empty).Split('&');
                            foreach (string queryString in queryStringValues)
                            {
                                if (!string.IsNullOrWhiteSpace(queryString))
                                {
                                    string queryStringName = queryString.Substring(0, queryString.IndexOf('=')).Trim();
                                    string queryStringVal = queryString.Substring(queryString.IndexOf('='), (queryString.Length - queryString.IndexOf('='))).Trim().Replace("=",string.Empty);

                                    redirectURL = Regex.Replace(redirectURL, "/" + queryStringName, "/" + queryStringVal, RegexOptions.IgnoreCase);
                                }
                            }

                            context.Context.Trace.Write("Redirect URL: " + redirectURL);

                            context.Response.Redirect(redirectURL);
                            context.CompleteRequest();
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Determines whether the specified string client name is valid client.
        /// </summary>
        /// <param name="strClientName">Name of the string client.</param>
        /// <returns></returns>
        private bool isValidclient(string strClientName)
        {
            if (clientCacheFactory.GetEntitiesBySearch(new ClientSearchDetail() { ClientName = strClientName.ToLower() }).Count() > 0)
                return true;
            else
                return false;
        }
    }
}