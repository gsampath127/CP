// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 11-17-2015
// ***********************************************************************

using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// Class ErrorController.
    /// </summary>
    public class ErrorController : BaseController
    {
        /// <summary>
        /// Indexes the specified error code.
        /// </summary>
        /// <param name="ErrorCode">The error code.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Index(string ErrorCode)
        {
            switch(ErrorCode)
            {
                case "404":
                    ViewBag.Heading = "Error 404 - Resource Not Found";
                    ViewBag.Message = "The resource you are looking for has been removed, had its name changed, or is temporarily unavailable. Please try again later";
                    break;
                case "500":
                    ViewBag.Heading = "Error 500 - Internal Server Error";
                    ViewBag.Message = "An internal server error occurred. Please try again later or contact website administrator";
                    break;
                case "600":
                    ViewBag.Heading = "Error 600 - Invalid URL";
                    ViewBag.Message = "The server cannot or will not process the request. Please check the URL and try again.";
                    break;
                default:
                    ViewBag.Heading = "Error " + ErrorCode + " - Application Error";
                    ViewBag.Message = "An application error occurred in the server. Please try again later or contact website administrator";
                    break;
            }
            
            return View();
        }
    }
}