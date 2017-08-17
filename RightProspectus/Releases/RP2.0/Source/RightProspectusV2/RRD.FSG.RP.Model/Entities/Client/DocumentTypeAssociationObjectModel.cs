// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************

using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class DocumentTypeAssociationObjectModel.
    /// </summary>
    public class DocumentTypeAssociationObjectModel : AuditedBaseModel<int>, IComparable<DocumentTypeAssociationObjectModel>
    {
        /// <summary>
        /// DocumentTypeAssociationId
        /// </summary>
        /// <value>The document type association identifier.</value>
        public int DocumentTypeAssociationId { get; set; }
        /// <summary>
        /// DocumentTypeId
        /// </summary>
        /// <value>The document type identifier.</value>
        public int DocumentTypeId { get; set; }
        /// <summary>
        /// SiteId
        /// </summary>
        /// <value>The site identifier.</value>
        public int? SiteId { get; set; }
        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        /// <value>The taxonomy association identifier.</value>
        public int? TaxonomyAssociationId { get; set; }
        /// <summary>
        /// Order
        /// </summary>
        /// <value>The order.</value>
        public int? Order { get; set; }
        /// <summary>
        /// HeaderText
        /// </summary>
        /// <value>The header text.</value>
        public string HeaderText { get; set; }
        /// <summary>
        /// LinkText
        /// </summary>
        /// <value>The link text.</value>
        public string LinkText { get; set; }
        /// <summary>
        /// DescriptionOverride
        /// </summary>
        /// <value>The description override.</value>
        public string DescriptionOverride { get; set; }
        /// <summary>
        /// CssClass
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }
        /// <summary>
        /// MarketId
        /// </summary>
        /// <value>The market identifier.</value>
        public string MarketId { get; set; }

        /// <summary>
        /// Compares the two Site entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(DocumentTypeAssociationObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
