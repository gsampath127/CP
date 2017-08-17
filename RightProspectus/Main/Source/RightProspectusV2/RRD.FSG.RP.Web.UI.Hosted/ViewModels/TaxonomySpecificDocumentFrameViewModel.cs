// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************

using RRD.FSG.RP.Model.Entities.HostedPages;
using System;


/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class TaxonomySpecificDocumentFrameViewModel.
    /// </summary>
    public class TaxonomySpecificDocumentFrameViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the taxonomy association data.
        /// </summary>
        /// <value>The taxonomy association data.</value>
        public TaxonomyAssociationData TaxonomyAssociationData { get; set; }

        /// <summary>
        /// Gets or sets the page load pdfurl.
        /// </summary>
        /// <value>The page load pdfurl.</value>
        public string PageLoadPDFURL { get; set; }
        /// <summary>
        /// Gets or sets the page load menu identifier.
        /// </summary>
        /// <value>The page load menu identifier.</value>
        public string PageLoadMenuID { get; set; }
        /// <summary>
        /// Gets or sets the name of the fund.
        /// </summary>
        /// <value>The name of the fund.</value>
        public string FundName { get; set; }
        /// <summary>
        /// Gets or sets the request batch identifier.
        /// </summary>
        /// <value>The request batch identifier.</value>
        public Guid RequestBatchId { get; set; }

        /// <summary>
        /// Gets or Sets TADFLogoText
        /// </summary>
        public string TADFLogoText { get; set; }
    }
}