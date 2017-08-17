using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    public class EditTaxonomyAssociationClientDocumentViewModel
    {
        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>The type of the document.</value>
        [DisplayName("Taxonomy")]
        public List<DisplayValuePair> TaxonomyAssociation { get; set; }

       

        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>The type of the document.</value>
        [DisplayName("Client Document Type")]
        public List<DisplayValuePair> ClientDocumentType { get; set; }

        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>The type of the document.</value>
        [DisplayName("Client Document")]
        public List<DisplayValuePair> ClientDocument { get; set; }



        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>The external identifier.</value>
        [DisplayName("FileName")]
        public string FileName { get; set; }

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


        public int SelectedTaxonomyId { get; set; }
        public int SelectedClientDocumentTypeId { get; set; }
        public int SelectedClientDocumentId { get; set; }
    }
}