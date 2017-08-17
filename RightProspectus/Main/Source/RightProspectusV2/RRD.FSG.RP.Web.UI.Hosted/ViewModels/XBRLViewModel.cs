// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-27-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using System;
using System.Collections.Generic;

/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class XBRLViewModel.
    /// </summary>
    public class XBRLViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the XBRL data.
        /// </summary>
        /// <value>The XBRL data.</value>
        public List<XBRLObjectModel> XBRLData { get; set; }
        /// <summary>
        /// Gets or sets the name of the fund.
        /// </summary>
        /// <value>The name of the fund.</value>
        public string FundName { get; set; }
        /// <summary>
        /// Gets or sets the XBRL zip files header text.
        /// </summary>
        /// <value>The XBRL zip files header text.</value>
        public string XBRLZipFilesHeaderText { get; set; }
        /// <summary>
        /// Gets or sets the XBRL viewer header text.
        /// </summary>
        /// <value>The XBRL viewer header text.</value>
        public string XBRLViewerHeaderText { get; set; }
        /// <summary>
        /// Gets or sets the XBRL viewer dated text.
        /// </summary>
        /// <value>The XBRL viewer dated text.</value>
        public string XBRLViewerDatedText { get; set; }
        /// <summary>
        /// Gets or sets the XBRL viewer filed text.
        /// </summary>
        /// <value>The XBRL viewer filed text.</value>
        public string XBRLViewerFiledText { get; set; }
        /// <summary>
        /// Gets or sets the XBRL date format.
        /// </summary>
        /// <value>The XBRL date format.</value>
        public string XBRLDateFormat { get; set; }
        /// <summary>
        /// Gets or sets the XBRL viewer not ready message.
        /// </summary>
        /// <value>The XBRL viewer not ready message.</value>
        public string XBRLViewerNotReadyMessage { get; set; }
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>The external identifier.</value>
        public string ExternalID {get; set;}
        /// <summary>
        /// Gets or sets the is internal taid.
        /// </summary>
        /// <value>The is internal taid.</value>
        public bool IsInternalTAID { get; set; }
        /// <summary>
        /// Gets or sets the request batch identifier.
        /// </summary>
        /// <value>The request batch identifier.</value>
        public Guid RequestBatchId { get; set; }
    }
}