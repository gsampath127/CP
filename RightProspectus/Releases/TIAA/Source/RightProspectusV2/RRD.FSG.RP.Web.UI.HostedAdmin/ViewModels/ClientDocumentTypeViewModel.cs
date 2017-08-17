// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************

using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class ClientDocumentTypeViewModel.
    /// </summary>
    public class ClientDocumentTypeViewModel
    {
        /// <summary>
        /// Gets or sets the client document type identifier.
        /// </summary>
        /// <value>The client document type identifier.</value>
        public int ClientDocumentTypeId { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>The modified by.</value>
        [DisplayName("Modified By")]
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the UTC last modified date.
        /// </summary>
        /// <value>The UTC last modified date.</value>
        [DisplayName("Modified Date")]
        public string UTCLastModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>The success or failed message.</value>
        public string SuccessOrFailedMessage { get; set; }
        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>The name of the modified by.</value>
        public string ModifiedByName { get; set; }

        /// <summary>
        /// Gets or sets the HostedDocumentsDisplayCount .
        /// </summary>
        /// <value>The HostedDocumentsDisplayCount .</value>
        public int HostedDocumentsDisplayCount { get; set; }

        /// <summary>
        /// Gets or sets the FTPName .
        /// </summary>
        /// <value>The FTPName .</value>
        public string FTPName { get; set; }

        /// <summary>
        /// Gets or sets the FTPUsername .
        /// </summary>
        /// <value>The FTPUsername .</value>
        public string FTPUsername { get; set; }

        /// <summary>
        /// Gets or sets the FTPPassword .
        /// </summary>
        /// <value>The FTPPassword .</value>
        public string FTPPassword { get; set; }
        /// <summary>
        /// Gets or sets the IsSFTP .
        /// </summary>
        /// <value>The IsSFTP .</value>
        public bool IsSFTP { get; set; }



    }
}
