// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

using System.Collections.Generic;

/// <summary>
/// The HostedPages namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    /// <summary>
    /// Class TaxonomyAssociationDocumentsModel.
    /// </summary>
    public class TaxonomyAssociationDocumentsModel
    {
        /// <summary>
        /// Gets or sets the document type headers.
        /// </summary>
        /// <value>The document type headers.</value>
        public List<HostedDocumentTypeHeader> DocumentTypeHeaders { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy association documents data.
        /// </summary>
        /// <value>The taxonomy association documents data.</value>
        public List<TaxonomyAssociationData> TaxonomyAssociationDocumentsData { get; set; }

        /// <summary>
        /// Gets or sets the foot notes.
        /// </summary>
        /// <value>The foot notes.</value>
        public List<HostedSiteFootNotes> FootNotes { get; set; }
    }
}
