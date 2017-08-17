// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015

using System.ComponentModel;


namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class AddVerticalXmlExportViewModel.
    /// </summary>
    public class AddVerticalXmlExportViewModel
    {
        /// <summary>
        /// InProgressJobCount
        /// </summary>
        /// <value>The in progress job count.</value>
        public int InProgressJobCount { get; set; }
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
        /// ExportedBy
        /// </summary>
        /// <value>The exported by.</value>
        public int ExportedBy { get; set; }
        /// <summary>
        /// ExportDescription
        /// </summary>
        /// <value>The export description.</value>
        [DisplayName("Description")]
        public string ExportDescription { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }
    }
}