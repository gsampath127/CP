// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-16-2015
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class EditDocumentTypeExternalIdViewModel.
    /// </summary>
    public class EditDocumentTypeExternalIdViewModel
    {
        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>The type of the document.</value>
        [DisplayName("Document Type")]
        public List<DisplayValuePair> DocumentType { get; set; }

        /// <summary>
        /// Gets or sets the selected document type identifier.
        /// </summary>
        /// <value>The selected document type identifier.</value>
        public int SelectedDocumentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>The external identifier.</value>
        [DisplayName("External ID")]
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>The modified by.</value>
        [DisplayName("Modified By")]
        public int? ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>The name of the modified by.</value>
        [DisplayName("Modified By")]
        public string ModifiedByName { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The modified date.</value>
        [DisplayName("Modified Date")]
        public string ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary.
        /// </summary>
        /// <value><c>true</c> if this instance is primary; otherwise, <c>false</c>.</value>
        [DisplayName("Is Primary")]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>The success or failed message.</value>
        public string SuccessOrFailedMessage { get; set; }

    }
}