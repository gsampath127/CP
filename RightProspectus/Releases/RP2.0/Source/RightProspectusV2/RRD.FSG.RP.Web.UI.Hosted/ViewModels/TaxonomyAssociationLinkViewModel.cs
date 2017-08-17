// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************


using RRD.FSG.RP.Model.Entities.HostedPages;
using System.Collections.Generic;

/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class TaxonomyAssociationLinkViewModel.
    /// </summary>
    public class TaxonomyAssociationLinkViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the product header text.
        /// </summary>
        /// <value>The product header text.</value>
        public string ProductHeaderText { get; set; }
        /// <summary>
        /// Gets or sets the taxonomy association link model data.
        /// </summary>
        /// <value>The taxonomy association link model data.</value>
        public List<TaxonomyAssociationLinkModel> TaxonomyAssociationLinkModelData { get; set; }
    }
}