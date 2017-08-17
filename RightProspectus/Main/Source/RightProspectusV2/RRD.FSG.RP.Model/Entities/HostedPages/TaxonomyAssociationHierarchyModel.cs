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
    /// This object model class will be used for "Taxonomy Association Hieararchy Documents" page
    /// (Scenario 3 in https://docs.google.com/spreadsheets/d/1Kkqb-xvO1NkXoLR8zA8cXDcZyYkyAJthbd4Y0_Ql0nU/edit#gid=454512427 document)
    /// </summary>
    public class TaxonomyAssociationHierarchyModel 
    {

        /// <summary>
        /// Gets or sets the parent headers.
        /// </summary>
        /// <value>The parent headers.</value>
        public List<HostedDocumentTypeHeader> ParentHeaders { get; set; }

        /// <summary>
        /// Gets or sets the child headers.
        /// </summary>
        /// <value>The child headers.</value>
        public List<HostedDocumentTypeHeader> ChildHeaders { get; set; }

        /// <summary>
        /// Gets or sets the parent taxonomy association data.
        /// </summary>
        /// <value>The parent taxonomy association data.</value>
        public List<TaxonomyAssociationData> ParentTaxonomyAssociationData { get; set; }

        /// <summary>
        /// Gets or sets the child taxonomy association data.
        /// </summary>
        /// <value>The child taxonomy association data.</value>
        public List<TaxonomyAssociationData> ChildTaxonomyAssociationData { get; set; }

        /// <summary>
        /// Gets or sets the foot notes.
        /// </summary>
        /// <value>The foot notes.</value>
        public List<HostedSiteFootNotes> FootNotes { get; set; }

    }
}
