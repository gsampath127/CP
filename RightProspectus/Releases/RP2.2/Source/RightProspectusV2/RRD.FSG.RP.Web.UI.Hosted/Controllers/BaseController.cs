// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

using System;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Added to Provide common behavior across Controller
    /// </summary>
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
        /// Sets the context.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void SetContext(string customer)
        {
            if (HttpContext.Items["CustomerName"] == null && !string.IsNullOrWhiteSpace(customer))
                HttpContext.Items.Add("CustomerName", customer);
        }
    }
}