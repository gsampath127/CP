// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-29-2015
// ***********************************************************************

using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class SiteActivityObjectModel.
    /// </summary>
    public class SiteActivityObjectModel : AuditedBaseModel<int>, IComparable<SiteActivityObjectModel>
    {
        /// <summary>
        /// SiteActivityId
        /// </summary>        
        /// <value>The site activity identifier.</value>
        public int SiteActivityId { get; set; }

        /// <summary>
        /// SiteId
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteId { get; set; }

        /// <summary>
        /// SiteName
        /// </summary>       
        /// <value>The name of the site.</value>
        public string SiteName { get; set; }

        /// <summary>
        /// ClientIPAddress
        /// </summary>
        /// <value>The client ip address.</value>
        public string ClientIPAddress { get; set; }

        /// <summary>
        /// UserAgentString
        /// </summary>
        /// <value>The user agent string.</value>
        public string UserAgentString { get; set; }

        /// <summary>
        /// HTTPMethod
        /// </summary>
        /// <value>The HTTP method.</value>
        public string HTTPMethod { get; set; }

        /// <summary>
        /// RequestUriString
        /// </summary>
        /// <value>The request URI string.</value>
        public string RequestUriString { get; set; }

        /// <summary>
        /// ParsedRequestUriString
        /// </summary>
        /// <value>The parsed request URI string.</value>
        public string ParsedRequestUriString { get; set; }        
       
        /// <summary>
        /// ServerName
        /// </summary>
        /// <value>The name of the server.</value>
        public string ServerName { get; set; }

        /// <summary>
        /// ReferrerUri
        /// </summary>
        /// <value>The referrer URI string.</value>
        public string ReferrerUriString { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        /// <value>The user identifier.</value>
        public int? UserId { get; set; }

        /// <summary>
        /// PageId
        /// </summary>
        /// <value>The page identifier.</value>
        public int? PageId { get; set; }

        /// <summary>
        /// TaxonomyAssociationGroupId
        /// </summary>
        /// <value>The taxonomy association group identifier.</value>
        public int? TaxonomyAssociationGroupId { get; set; }

        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        /// <value>The level.</value>
        public int? Level { get; set; }

        /// <summary>
        /// TaxonomyExternalId
        /// </summary>        
        /// <value>The taxonomy external identifier.</value>
        public string TaxonomyExternalId { get; set; }

        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>        
        /// <value>The taxonomy association identifier.</value>
        public int? TaxonomyAssociationId { get; set; }

        /// <summary>
        /// DocumentTypeId
        /// </summary>
        /// <value>The document type identifier.</value>
        public int? DocumentTypeId { get; set; }

        /// <summary>
        /// DocumentTypeName
        /// </summary>
        /// <value>The document type external identifier.</value>
        public string DocumentTypeExternalID { get; set; }

        /// <summary>
        /// ClientDocumentGroupId
        /// </summary>
        /// <value>The client document group identifier.</value>
        public int? ClientDocumentGroupId { get; set; }

        /// <summary>
        /// ClientDocumentId
        /// </summary>
        /// <value>The client document identifier.</value>
        public int? ClientDocumentId { get; set; }

        /// <summary>
        /// RequestUniqueIdentifier
        /// </summary>
        /// <value>The request batch identifier.</value>
        public Guid RequestBatchId { get; set; }

        /// <summary>
        /// If the request is Initial Request InitDoc
        /// </summary>
        /// <value><c>true</c> if [initialize document]; otherwise, <c>false</c>.</value>
        public bool InitDoc { get; set; }

        /// <summary>
        /// XBRLDocumentName
        /// </summary>        
        /// <value>The name of the XBRL document.</value>
        public string XBRLDocumentName { get; set; }
        
        /// <summary>
        /// Gets or sets the click.
        /// </summary>
        /// <value>The click.</value>
        public int Click { get; set; }

        /// <summary>
        /// XBRLItemType
        /// </summary>
        /// <value>The type of the XBRL item.</value>
        public int XBRLItemType { get; set; }

        /// <summary>
        /// BadRequestIssue
        /// </summary>
        /// <value>The bad request issue.</value>
        public int BadRequestIssue { get; set; }

        /// <summary>
        /// BadRequestIssue
        /// </summary>
        /// <value>The bad request issue description.</value>
        public string  BadRequestIssueDescription { get; set; }

        /// <summary>
        /// BadRequestParameterName
        /// </summary>
        /// <value>The name of the bad request parameter.</value>
        public string BadRequestParameterName { get; set; }

        /// <summary>
        /// BadRequestParameterValue
        /// </summary>
        /// <value>The bad request parameter value.</value>
        public string BadRequestParameterValue { get; set; }

        /// <summary>
        /// Gets or sets the request UTC date.
        /// </summary>
        /// <value>The request UTC date.</value>
        public DateTime RequestUtcDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>The type of the document.</value>
        public string DocumentType { get; set; }

        /// <summary>
        /// DocumentTypeMarketId
        /// </summary>
        /// <value>The Market Id of the document.</value>
        public string DocumentTypeMarketId { get; set; }

        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(SiteActivityObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
