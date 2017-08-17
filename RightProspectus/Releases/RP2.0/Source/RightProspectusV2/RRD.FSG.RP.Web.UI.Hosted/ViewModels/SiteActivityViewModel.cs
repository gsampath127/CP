// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using System;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted.ViewModels
{
    /// <summary>
    /// Class SiteActivityViewModel.
    /// </summary>
    public class SiteActivityViewModel
    {
        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        /// <value>The customer.</value>
        public string Customer { get; set; }
        /// <summary>
        /// Gets or sets the site.
        /// </summary>
        /// <value>The site.</value>
        public string Site { get; set; }
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
        /// Gets or sets a value indicating whether this instance is internal taid.
        /// </summary>
        /// <value><c>true</c> if this instance is internal taid; otherwise, <c>false</c>.</value>
        public bool IsInternalTAID { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is internal dtid.
        /// </summary>
        /// <value><c>true</c> if this instance is internal dtid; otherwise, <c>false</c>.</value>
        public bool IsInternalDTID { get; set; }
        /// <summary>
        /// Gets or sets the request batch identifier.
        /// </summary>
        /// <value>The request batch identifier.</value>
        public Guid RequestBatchId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [initialize document].
        /// </summary>
        /// <value><c>true</c> if [initialize document]; otherwise, <c>false</c>.</value>
        public bool InitDoc { get; set; }
        /// <summary>
        /// Gets or sets the bad request issue.
        /// </summary>
        /// <value>The bad request issue.</value>
        public int BadRequestIssue { get; set; }
        /// <summary>
        /// Gets or sets the name of the bad request parameter.
        /// </summary>
        /// <value>The name of the bad request parameter.</value>
        public string BadRequestParameterName { get; set; }
        /// <summary>
        /// Gets or sets the bad request parameter value.
        /// </summary>
        /// <value>The bad request parameter value.</value>
        public string BadRequestParameterValue { get; set; }
        /// <summary>
        /// Gets or sets the name of the XBRL document.
        /// </summary>
        /// <value>The name of the XBRL document.</value>
        public string XBRLDocumentName { get; set; }
        /// <summary>
        /// Gets or sets the type of the XBRL item.
        /// </summary>
        /// <value>The type of the XBRL item.</value>
        public int XBRLItemType { get; set; }
        /// <summary>
        /// Gets or sets the XBRL request URL.
        /// </summary>
        /// <value>The XBRL request URL.</value>
        public string XBRLRequestURL { get; set; }
    }
}