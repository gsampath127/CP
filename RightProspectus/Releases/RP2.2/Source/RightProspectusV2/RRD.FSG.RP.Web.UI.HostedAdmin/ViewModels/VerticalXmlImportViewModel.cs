
/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class VerticalXmlImportViewModel.
    /// </summary>
    public class VerticalXmlImportViewModel
    {
        /// <summary>
        /// VerticalXmlImportId
        /// </summary>
        /// <value>The vertical XML import identifier.</value>
        public int VerticalXmlImportId { get; set; }
        /// <summary>
        /// VerticalXMLImportImportTypes
        /// </summary>
        /// <value>The import types.</value>
        public int ImportTypes { get; set; }
        /// <summary>
        /// ImportXml
        /// </summary>
        /// <value>The import XML.</value>
        public string ImportXml { get; set; }
        /// <summary>
        /// ImportDate
        /// </summary>
        /// <value>The import date.</value>
        public string ImportDate { get; set; }
        /// <summary>
        /// ImportedBy
        /// </summary>
        /// <value>The imported by.</value>
        public int ImportedBy { get; set; }
        /// <summary>
        /// ImportedByName
        /// </summary>
        /// <value>The name of the imported by.</value>
        public string ImportedByName { get; set; }
        /// <summary>
        /// ImportDescription
        /// </summary>
        /// <value>The import description.</value>
        public string ImportDescription { get; set; }
        /// <summary>
        /// ExportBackupId
        /// </summary>
        /// <value>The export backup identifier.</value>
        public int? ExportBackupId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }
        /// <summary>
        /// StatusID
        /// </summary>
        /// <value>The status identifier.</value>
        public int StatusID { get; set; }
    }
}