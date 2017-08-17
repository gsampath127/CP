// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By :
// Last Modified On : 11-17-2015
// ***********************************************************************

/// <summary>
/// The Hosted namespace.
/// </summary>
using RRD.FSG.RP.Model.Entities.System;
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class BaseViewModel.
    /// </summary>
    public class BaseViewModel
    {
        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        public string ClientName { get; set; }
        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>The name of the site.</value>
        public string SiteName { get; set; }
        /// <summary>
        /// Gets or sets the is proofing.
        /// </summary>
        /// <value>The is proofing.</value>
        public int IsProofing { get; set; }
        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        /// <value>The page identifier.</value>
        public int PageId { get; set; }
        /// <summary>
        /// Gets or sets the is internal taid.
        /// </summary>
        /// <value>The is internal taid.</value>
        public int IsInternalTAID { get; set; }
        /// <summary>
        /// Gets or sets the external id1.
        /// </summary>
        /// <value>The external id1.</value>
        public string ExternalID1 { get; set; }
        /// <summary>
        /// Gets or sets the external id2.
        /// </summary>
        /// <value>The external id2.</value>
        public string ExternalID2 { get; set; }
        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>The base URL.</value>
        public string BaseURL { get; set; }
        /// <summary>
        /// Gets or sets the logo text.
        /// </summary>
        /// <value>The logo text.</value>
        public string LogoText { get; set; }
        /// <summary>
        /// Gets or sets the browser alert text.
        /// </summary>
        /// <value>The browser alert text.</value>
        public string BrowserAlertText { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [show XBRL in landing page].
        /// </summary>
        /// <value><c>true</c> if [show XBRL in landing page]; otherwise, <c>false</c>.</value>
        public bool ShowXBRLInLandingPage { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [display XBRL in new tab].
        /// </summary>
        /// <value><c>true</c> if [display XBRL in new tab]; otherwise, <c>false</c>.</value>
        public bool DisplayXBRLInNewTAB { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [display request material].
        /// </summary>
        /// <value><c>true</c> if [display request material]; otherwise, <c>false</c>.</value>
        public bool DisplayRequestMaterial { get; set; }
        /// <summary>
        /// Gets or sets the page CSS resource key.
        /// </summary>
        /// <value>The page CSS resource key.</value>
        public string PageCSSResourceKey { get; set; }
        /// <summary>
        /// Gets or sets the page navigation XML.
        /// </summary>
        /// <value>The page navigation XML.</value>
        public string PageNavigationXML { get; set; }
        /// <summary>
        /// Gets or sets the page navigation XSLT.
        /// </summary>
        /// <value>The page navigation XSLT.</value>
        public string PageNavigationXSLT { get; set; }
        /// <summary>
        /// Gets or sets the document not available text.
        /// </summary>
        /// <value>The document not available text.</value>
        public string DocumentNotAvailableText { get; set; }
        /// <summary>
        /// Gets or sets the taxonomy not available text.
        /// </summary>
        /// <value>The taxonomy not available text.</value>
        public string TaxonomyNotAvailableText { get; set; }
        /// <summary>
        /// Gets or Sets the value to show Browser Alert Message
        /// </summary>
        /// <value>The DisplayBrowserAlert.</value>
        public bool DisplayBrowserAlert { get; set; }

        public bool SinglePdfViewShowClientFrame { get; set; }
        public bool IsSinglePdfView { get; set; }
        
        public BrowserVersionObjectModel BrowserDetails { get; set; }

        /// <summary>
        /// Gets or sets value for SARValidation
        /// </summary>
        public bool IsSARValidationEnabled { get; set; }
    }
}