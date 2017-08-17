
/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class VerticalXmlExportViewModel.
    /// </summary>
    public class VerticalXmlExportViewModel
    {
        /// <summary>
        /// VerticalXmlExportId
        /// </summary>
        /// <value>The vertical XML export identifier.</value>
        public int VerticalXmlExportId { get; set; }
        /// <summary>
        /// ExportTypes
        /// </summary>
        /// <value>The export types.</value>
        public int ExportTypes { get; set; }
        /// <summary>
        /// ExportXml
        /// </summary>
        /// <value>The export XML.</value>
        public string ExportXml { get; set; }
        /// <summary>
        /// ExportDate
        /// </summary>
        /// <value>The export date.</value>
        public string ExportDate { get; set; }
        /// <summary>
        /// ExportedBy
        /// </summary>
        /// <value>The exported by.</value>
        public int ExportedBy { get; set; }
        /// <summary>
        /// ExportedByName
        /// </summary>
        /// <value>The name of the exported by.</value>
        public string ExportedByName { get; set; }
        /// <summary>
        /// ExportDescription
        /// </summary>
        /// <value>The export description.</value>
        public string ExportDescription { get; set; }
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