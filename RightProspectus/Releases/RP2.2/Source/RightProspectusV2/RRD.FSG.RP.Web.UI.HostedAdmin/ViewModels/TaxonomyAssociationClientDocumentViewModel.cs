using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    public class TaxonomyAssociationClientDocumentViewModel
    {
        /// <summary>
        /// Gets or sets the taxonomy.
        /// </summary>
        /// <value>The taxonomy.</value>
        public int TaxonomyAssociationId { get; set; }
        /// <summary>
        /// Gets or sets the taxonomy.
        /// </summary>
        /// <value>The taxonomy.</value>
        public string TaxonomyAssociationName { get; set; }
        /// <summary>
        /// Gets or sets the ClientDocumentID.
        /// </summary>
        /// <value>The ClientDocumentId.</value>
        public int ClientDocumentId { get; set; }
        /// <summary>
        /// Gets or sets the ClientDocumentType.
        /// </summary>
        /// <value>The ClientDocumentType.</value>
        public string ClientDocumentType { get; set; }
        /// <summary>
        /// Gets or sets the ClientDocumentName.
        /// </summary>
        /// <value>The CliemtDocumentName.</value>
        public string ClientDocumentName { get; set; }
        /// <summary>
        /// Gets or sets the FileName.
        /// </summary>
        /// <value>The FileName.</value>
        public string ClientDocumentFileName { get; set; }
        /// <summary>
        /// TaxonomyAssociation Level
        /// </summary>
        /// <value>The level.</value>
        public int Level { get; set; }
        /// <summary>
        /// TaxonomyId
        /// </summary>
        /// <value>The taxonomy identifier.</value>
        public int TaxonomyId { get; set; }
        /// <summary>
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>The success or failed message.</value>
        public string SuccessOrFailedMessage { get; set; }


    }
}