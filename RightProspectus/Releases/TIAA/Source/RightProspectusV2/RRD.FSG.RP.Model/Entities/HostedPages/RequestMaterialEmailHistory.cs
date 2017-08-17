// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 10-31-2015
// ***********************************************************************
using System;

/// <summary>
/// The HostedPages namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    /// <summary>
    /// Class RequestMaterialEmailHistory.
    /// </summary>
    public class RequestMaterialEmailHistory
    {

        /// <summary>
        /// Gets or sets the request material email history identifier.
        /// </summary>
        /// <value>The request material email history identifier.</value>
        public int RequestMaterialEmailHistoryId {get; set;} 

        /// <summary>
        /// Gets or sets the recip email.
        /// </summary>
        /// <value>The recip email.</value>
        public string RecipEmail {get; set;}

        /// <summary>
        /// Gets or sets the request date UTC.
        /// </summary>
        /// <value>The request date UTC.</value>
        public DateTime RequestDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public Guid UniqueID {get; set;}

        /// <summary>
        /// Gets or sets the f click date.
        /// </summary>
        /// <value>The f click date.</value>
        public DateTime  FClickDate {get; set;}

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        /// <value>The user agent.</value>
        public string UserAgent {get; set;}

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        public string IPAddress {get; set;}

        /// <summary>
        /// Gets or sets the request URI string.
        /// </summary>
        /// <value>The request URI string.</value>
        public string RequestUriString { get; set; }

        /// <summary>
        /// Gets or sets the refererr
        /// </summary>
        /// <value>The referer.</value>
        public string Referer {get; set;}

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RequestMaterialEmailHistory"/> is sent.
        /// </summary>
        /// <value><c>true</c> if sent; otherwise, <c>false</c>.</value>
        public bool Sent { get; set; }

        /// <summary>
        /// Gets or sets the request batch identifier.
        /// </summary>
        /// <value>The request batch identifier.</value>
        public Guid RequestBatchId { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy association data.
        /// </summary>
        /// <value>The taxonomy association data.</value>
        public TaxonomyAssociationData TaxonomyAssociationData { get; set; }
    }
}
