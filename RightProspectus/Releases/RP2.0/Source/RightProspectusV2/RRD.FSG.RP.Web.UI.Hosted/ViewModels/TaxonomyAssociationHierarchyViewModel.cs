// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-29-2015
// ***********************************************************************



/// <summary>
/// The Hosted namespace.
/// </summary>
using RRD.FSG.RP.Model.Entities.HostedPages;
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class TaxonomyAssociationHierarchyViewModel.
    /// </summary>
    public class TaxonomyAssociationHierarchyViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the product header text.
        /// </summary>
        /// <value>The product header text.</value>
        public string ProductHeaderText { get; set; }
        /// <summary>
        /// Gets or sets the product grid product name column text.
        /// </summary>
        /// <value>The product grid product name column text.</value>
        public string ProductGridProductNameColumnText { get; set; }
        /// <summary>
        /// Gets or sets the underlaying fund header text.
        /// </summary>
        /// <value>The underlaying fund header text.</value>
        public string UnderlayingFundHeaderText { get; set; }
        /// <summary>
        /// Gets or sets the underlaying fund grid fund name column text.
        /// </summary>
        /// <value>The underlaying fund grid fund name column text.</value>
        public string UnderlayingFundGridFundNameColumnText { get; set; }
        /// <summary>
        /// Gets or sets the document not available text.
        /// </summary>
        /// <value>The document not available text.</value>
        public string DocumentNotAvailableText { get; set; }
        /// <summary>
        /// Gets or sets the product document not available text.
        /// </summary>
        /// <value>The product document not available text.</value>
        public string ProductDocumentNotAvailableText { get; set; }
        /// <summary>
        /// Gets or sets the XBRL not available text.
        /// </summary>
        /// <value>The XBRL not available text.</value>
        public string XBRLNotAvailableText { get; set; }
        /// <summary>
        /// Gets or sets the footnotes header text.
        /// </summary>
        /// <value>The footnotes header text.</value>
        public string FootnotesHeaderText { get; set; }
        /// <summary>
        /// Gets or sets the glossary.
        /// </summary>
        /// <value>The glossary.</value>
        public string Glossary { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy association hierarchy model data.
        /// </summary>
        /// <value>The taxonomy association hierarchy model data.</value>
        public TaxonomyAssociationHierarchyModel TaxonomyAssociationHierarchyModelData { get; set; }
        
    }
}