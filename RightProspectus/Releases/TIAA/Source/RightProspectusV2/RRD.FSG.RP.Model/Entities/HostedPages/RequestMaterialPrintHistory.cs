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
    /// Class RequestMaterialPrintHistory.
    /// </summary>
    public class RequestMaterialPrintHistory
    {

        /// <summary>
        /// Gets or sets the request material print history identifier.
        /// </summary>
        /// <value>The request material print history identifier.</value>
        public int RequestMaterialPrintHistoryID {get; set;}

        /// <summary>
        /// Gets or sets the name of the client company.
        /// </summary>
        /// <value>The name of the client company.</value>
        public string ClientCompanyName {get; set;}

        /// <summary>
        /// Gets or sets the first name of the client.
        /// </summary>
        /// <value>The first name of the client.</value>
        public string  ClientFirstName {get; set;}

        /// <summary>
        /// Gets or sets the name of the client middle.
        /// </summary>
        /// <value>The name of the client middle.</value>
        public string ClientMiddleName {get; set;}

        /// <summary>
        /// Gets or sets the last name of the client.
        /// </summary>
        /// <value>The last name of the client.</value>
        public string ClientLastName {get; set;}

        /// <summary>
        /// Gets or sets the full name of the client.
        /// </summary>
        /// <value>The full name of the client.</value>
        public string ClientFullName {get; set;}

        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>The address1.</value>
        public string Address1 {get; set;}

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>The address2.</value>
        public string Address2 {get; set;}

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City {get; set;}

        /// <summary>
        /// Gets or sets the state or province.
        /// </summary>
        /// <value>The state or province.</value>
        public string StateOrProvince { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode {get; set;}

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        public int Quantity { get; set; }

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
        /// Gets or sets the request batch identifier.
        /// </summary>
        /// <value>The request batch identifier.</value>
        public Guid RequestBatchId { get; set; }

        /// <summary>
        /// Gets or sets the request URI string.
        /// </summary>
        /// <value>The request URI string.</value>
        public string RequestUriString { get; set; }

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
        /// Gets or sets the referer.
        /// </summary>
        /// <value>The referer.</value>
        public string Referer { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy association data.
        /// </summary>
        /// <value>The taxonomy association data.</value>
        public TaxonomyAssociationData TaxonomyAssociationData { get; set; }
    }
}
