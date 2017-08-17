using RRD.FSG.RP.Model.Entities.HostedPages;
using System;
/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class TaxonomyAssociationClientDocumentViewModel.
    /// </summary>
    public class TaxonomyAssociationClientDocumentViewModel : BaseViewModel
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
        /// Gets or sets the name of the FormNMFPText.
        /// </summary>
        /// <value>The name of the FormNMFPText.</value>
        public string FormNMFPText { get; set; }
        /// <summary>
        /// Gets or sets the name of the FormNMFPText.
        /// </summary>
        /// <value>The name of the FormNMFPText.</value>
        public string DMMDLinkText { get; set; }
        /// <summary>
        /// Gets or sets the name of the FormNMFPText.
        /// </summary>
        /// <value>The name of the FormNMFPText.</value>
        public string NCRLinkText { get; set; }
        /// <summary>
        /// Gets or sets the request batch identifier.
        /// </summary>
        /// <value>The request batch identifier.</value>
        public Guid RequestBatchId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [display DailyMoneyMarketDisclosure].
        /// </summary>
        /// <value><c>true</c> if [display DailyMoneyMarketDisclosure]; otherwise, <c>false</c>.</value>
        public bool DisplayDailyMoneyMarketDisclosure { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [display NCR].
        /// </summary>
        /// <value><c>true</c> if [display NCR]; otherwise, <c>false</c>.</value>
        public bool DisplayNCR { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [display FormN_MFP].
        /// </summary>
        /// <value><c>true</c> if [display FormN_MFP]; otherwise, <c>false</c>.</value>
        public bool DisplayFormN_MFP { get; set; }

    }
}