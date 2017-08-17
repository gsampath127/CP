// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-31-2015
// ***********************************************************************

using RRD.FSG.RP.Model.Entities.HostedPages;
using System;

/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class RequestMaterialViewModel.
    /// </summary>
    public class RequestMaterialViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the request batch identifier.
        /// </summary>
        /// <value>The request batch identifier.</value>
        public Guid? RequestBatchId { get; set; }
        /// <summary>
        /// Gets or sets the taxonomy association data.
        /// </summary>
        /// <value>The taxonomy association data.</value>
        public TaxonomyAssociationData TaxonomyAssociationData { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the selected document types.
        /// </summary>
        /// <value>The selected document types.</value>
        public string SelectedDocTypes { get; set; }
        /// <summary>
        /// Gets or sets the taxonomy association identifier.
        /// </summary>
        /// <value>The taxanomy association identifier.</value>
        public string TaxanomyAssociationId { get; set; }

        //Print Details

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName  { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName  { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName  { get; set; }
        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>The address1.</value>
        public string Address1  { get; set; }
        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>The address2.</value>
        public string Address2  { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City  { get; set; }
        /// <summary>
        /// Gets or sets the state or province.
        /// </summary>
        /// <value>The state or province.</value>
        public string StateOrProvince { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode { get; set; }

    }
}