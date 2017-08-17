using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    public class TaxonomyAssociationGroupModel
    {

        /// <summary>
        /// Gets or sets the document type headers.
        /// </summary>
        /// <value>The document type headers.</value>
        public List<object> TAGDetails { get; set; }

        /// <summary>
        /// Gets or sets the document type headers.
        /// </summary>
        /// <value>The document type headers.</value>
        public List<HostedDocumentTypeHeader> DocumentTypeHeaders { get; set; }

        /// <summary>
        /// Gets or sets the TaxonomyAssociationGroupTaxonomyAssociationData
        /// </summary>
        /// <value>The taxonomy association documents data.</value>
        public List<TaxonomyAssociationGroupTaxonomyAssociationData> TAGTAData { get; set; }

        /// <summary>
        /// Gets or sets the foot notes.
        /// </summary>
        /// <value>The foot notes.</value>
        public List<HostedSiteFootNotes> FootNotes { get; set; }

    }
}
