using RRD.FSG.RP.Model.Entities.VerticalMarket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    public class EditDocumentSubstitutionViewModel
    {
        /// <summary>
        /// SelectedID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// DocumentType
        /// </summary>
        public List<DocumentTypeObjectModel> DocumentType { get; set; }

        /// <summary>
        /// SelectedDocumentType
        /// </summary>
        public string SelectedDocumentType { get; set; }

        /// <summary>
        /// SubstituteDocumentType
        /// </summary>
        public List<DocumentTypeObjectModel> SubstituteDocumentType { get; set; }

        /// <summary>
        /// SelectedSubstituteDocumentType
        /// </summary>
        public string SelectedSubstituteDocumentType { get; set; }

        /// <summary>
        /// SubstituteDocumentType
        /// </summary>
        public List<DocumentTypeObjectModel> NDocumentType { get; set; }

        /// <summary>
        /// SelectedSubstituteDocumentType
        /// </summary>
        public string SelectedNDocumentType { get; set; }

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
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>The success or failed message.</value>
        public string SuccessOrFailedMessage { get; set; }
    }
}