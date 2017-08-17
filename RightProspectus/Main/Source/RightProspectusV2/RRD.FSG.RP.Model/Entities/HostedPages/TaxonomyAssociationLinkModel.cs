// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : NI317175
// Created          : 10-05-2015
//
// Last Modified By : NI317175
// Last Modified On : 11-17-2015
// ***********************************************************************

// <summary>
/// The HostedPages namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    /// <summary>
    /// This object model class will be used for "Taxonomy Association Links"  page
    /// (Scenario 1 in https://docs.google.com/spreadsheets/d/1Kkqb-xvO1NkXoLR8zA8cXDcZyYkyAJthbd4Y0_Ql0nU/edit#gid=454512427 document)
    /// </summary>
    public class TaxonomyAssociationLinkModel 
    {
        /// <summary>
        /// Gets or sets the parent taxonomy associaiton identifier.
        /// </summary>
        /// <value>The parent taxonomy associaiton identifier.</value>
        public int ParentTaxonomyAssociaitonID { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy identifier.
        /// </summary>
        /// <value>The taxonomy identifier.</value>
        public int TaxonomyID { get; set; }

        /// <summary>
        /// Gets or sets the name of the taxonomy assocation.
        /// </summary>
        /// <value>The name of the taxonomy assocation.</value>
        public string TaxonomyAssocationName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is objectin vertical market.
        /// </summary>
        /// <value><c>true</c> if this instance is objectin vertical market; otherwise, <c>false</c>.</value>
        public bool IsObjectinVerticalMarket { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy description override.
        /// </summary>
        /// <value>The taxonomy description override.</value>
        public string TaxonomyDescriptionOverride { get; set; }

        /// <summary>
        /// Gets or sets the FundOrder.
        /// </summary>
        /// <value>The FundOrder.</value>
        public int FundOrder { get; set; }

   }
}
