// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015


using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class AddVerticalXmlImportViewModel.
    /// </summary>
    public class AddVerticalXmlImportViewModel
    {
        /// <summary>
        /// InProgressJobCount
        /// </summary>
        /// <value>The in progress job count.</value>
        public int InProgressJobCount { get; set; }
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
        /// ImportedBy
        /// </summary>
        /// <value>The imported by.</value>
        public int ImportedBy { get; set; }
        /// <summary>
        /// ImportDescription
        /// </summary>
        /// <value>The import description.</value>
        [DisplayName("Description")]
        public string ImportDescription { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }
    }
}