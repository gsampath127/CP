// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class ClientDocumentViewModel.
    /// </summary>
    public class ClientDocumentViewModel
    {
        /// <summary>
        /// ClientDocumentId
        /// </summary>
        /// <value>The client document identifier.</value>
        public int ClientDocumentId { get; set; }
        /// <summary>
        /// ClientDocumentTypeId
        /// </summary>
        /// <value>The client document type identifier.</value>
        public int ClientDocumentTypeId { get; set; }
        /// <summary>
        /// ClientDocumentTypeName
        /// </summary>
        /// <value>The name of the client document type.</value>
        public string ClientDocumentTypeName { get; set; }
        /// <summary>
        /// Gets or sets the selected level identifier.
        /// </summary>
        /// <value>The selected level identifier.</value>
        public int SelectedClientDocumentTypeId { get; set; }
        /// <summary>
        /// Gets or sets the ClientDocumentTypeName.
        /// </summary>
        /// <value>The ClientDocumentTypeName.</value>
        [DisplayName("Document Type")]
        public List<DisplayValuePair> DocumentType { get; set; }
        /// <summary>
        /// FileName
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// FileName
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }
        /// <summary>
        /// MimeType
        /// </summary>
        /// <value>The type of the MIME.</value>
        public string MimeType { get; set; }
        /// <summary>
        /// IsPrivate
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        public bool IsPrivate { get; set; } //For edit Page
         /// <summary>
        /// IsPrivate
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        public string IsPrivateString { get; set; } //For List Page To display the value as "True/False" instead of "true/false".
        /// <summary>
        /// ContentUri
        /// </summary>
        /// <value>The content URI.</value>
        public string ContentUri { get; set; }
        /// <summary>
        /// Description
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
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }
}