// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************

using RRD.FSG.RP.Model.Entities.Client;
using System.Collections.Generic;

/// <summary>
/// The HostedPages namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    /// <summary>
    /// This object model class will be used for "Taxonomy Association Documents"  page
    /// (Scenario 6 in https://docs.google.com/spreadsheets/d/1Kkqb-xvO1NkXoLR8zA8cXDcZyYkyAJthbd4Y0_Ql0nU/edit#gid=454512427 document)
    /// This class also be used to store Product data and underlaying fund data in TaxonomyAssociationHierarchy class.
    /// </summary>
    public class TaxonomyAssociationData 
    {
        /// <summary>
        /// Gets or sets the taxonomy association identifier.
        /// </summary>
        /// <value>The taxonomy association identifier.</value>
        public int TaxonomyAssociationID { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy identifier.
        /// </summary>
        /// <value>The taxonomy identifier.</value>
        public int TaxonomyID { get; set; }

        /// <summary>
        /// Gets or sets the name of the taxonomy.
        /// </summary>
        /// <value>The name of the taxonomy.</value>
        public string TaxonomyName { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy description override.
        /// </summary>
        /// <value>The taxonomy description override.</value>
        public string TaxonomyDescriptionOverride { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy CSS class.
        /// </summary>
        /// <value>The taxonomy CSS class.</value>
        public string TaxonomyCssClass { get; set; }

        /// <summary>
        /// Gets or sets the MarketId.
        /// </summary>
        /// <value>The name of the taxonomy.</value>
        public string MarketId { get; set; }
        /// <summary>
        /// Gets or sets SeriesID
        /// </summary>
        /// <value>The name of the taxonomy.</value>
        public string SeriesID { get; set; }

        /// <summary>
        /// Gets or sets the document types.
        /// </summary>
        /// <value>The document types.</value>
        public List<HostedDocumentType> DocumentTypes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is objectin vertical market.
        /// </summary>
        /// <value><c>true</c> if this instance is objectin vertical market; otherwise, <c>false</c>.</value>
        public bool IsObjectinVerticalMarket { get; set; }
        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        /// <value>The name of the taxonomy Order.</value>
        public int FundOrder { get; set; }

        /// <summary>
        /// Gets or sets the document types.
        /// </summary>
        /// <value>The document types.</value>
        public List<ClientDocumentObjectModel> ClientDocuments { get; set; }
    }
}
