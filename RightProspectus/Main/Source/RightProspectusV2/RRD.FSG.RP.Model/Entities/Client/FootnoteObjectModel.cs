// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015

using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class FootnoteObjectModel.
    /// </summary>
    public class FootnoteObjectModel : AuditedBaseModel<int>, IComparable<FootnoteObjectModel>
    {
        /// <summary>
        /// FootnoteId
        /// </summary>
        /// <value>The footnote identifier.</value>
        public int FootnoteId { get; set; }
        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        /// <value>The taxonomy association identifier.</value>
        public int? TaxonomyAssociationId { get; set; }
        /// <summary>
        /// TaxonomyAssociationGroupId
        /// </summary>
        /// <value>The taxonomy association group identifier.</value>
        public int? TaxonomyAssociationGroupId { get; set; }
        /// <summary>
        /// LanguageCulture
        /// </summary>
        /// <value>The language culture.</value>
        public string LanguageCulture { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        /// <summary>
        /// Order
        /// </summary>
        /// <value>The order.</value>
        public int? Order { get; set; }

        /// <summary>
        /// MarketId
        /// </summary>
        /// <value>The market identifier.</value>
        public string TaxonomyMarketId { get; set; }

        /// <summary>
        /// Compares the two Footnote entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(FootnoteObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }

    }
}
