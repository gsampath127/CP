
/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class DocumentTypeExternalIdViewModel.
    /// </summary>
    public class DocumentTypeExternalIdViewModel
    {
        /// <summary>
        /// PageTextID
        /// </summary>
        /// <value>The document type identifier.</value>
        public int DocumentTypeId { get; set; }
        /// <summary>
        /// Page Name
        /// </summary>
        /// <value>The name of the document type.</value>
        public string DocumentTypeName { get; set; }
        /// <summary>
        /// PageDescription
        /// </summary>
        /// <value>The external identifier.</value>
        public string ExternalId { get; set; }
        /// <summary>
        /// ModifiedDate
        /// </summary>
        /// <value>The modified date.</value>
        public string ModifiedDate { get; set; }

        /// <summary>
        /// Modified By Name
        /// </summary>
        /// <value>The name of the modified by.</value>
        public string ModifiedByName { get; set; }

        /// <summary>
        /// ModifiedBy
        /// </summary>
        /// <value>The modified by.</value>
        public int ModifiedBy { get; set; }

        /// <summary>
        /// IsPrimary
        /// </summary>
        /// <value>The is primary.</value>
        public string IsPrimary { get; set; }
        
    }
}
