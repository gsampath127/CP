// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-29-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.HostedPages;


/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class TaxonomyAssociationDocumentsViewModel.
    /// </summary>
    public class TaxonomyAssociationDocumentsViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the underlying fund documents help text.
        /// </summary>
        /// <value>The underlying fund documents help text.</value>
        public string UnderlyingFundDocumentsHelpText { get; set; }
        /// <summary>
        /// Gets or sets the underlaying fund grid fund name column text.
        /// </summary>
        /// <value>The underlaying fund grid fund name column text.</value>
        public string UnderlayingFundGridFundNameColumnText { get; set; }
        /// <summary>
        /// Gets or sets the underlaying fund grid na text.
        /// </summary>
        /// <value>The underlaying fund grid na text.</value>
        public string UnderlayingFundGridNAText { get; set; }
        /// <summary>
        /// Gets or sets the footnotes header text.
        /// </summary>
        /// <value>The footnotes header text.</value>
        public string FootnotesHeaderText { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy association documents model.
        /// </summary>
        /// <value>The taxonomy association documents model.</value>
        public TaxonomyAssociationDocumentsModel TaxonomyAssociationDocumentsModel { get; set; }
    }
}