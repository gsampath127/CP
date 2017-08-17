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
    /// Class TaxonomyAssociationObjectModel.
    /// </summary>
    public class TaxonomyAssociationObjectModel : AuditedBaseModel<int>, IComparable<TaxonomyAssociationObjectModel>
    {
        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        /// <value>The taxonomy association identifier.</value>
        public int TaxonomyAssociationId { get; set; }
        /// <summary>
        /// Level
        /// </summary>
        /// <value>The level.</value>
        public int Level { get; set; }
        /// <summary>
        /// TaxonomyId
        /// </summary>
        /// <value>The taxonomy identifier.</value>
        public int TaxonomyId { get; set; }
        /// <summary>
        /// SiteId
        /// </summary>
        /// <value>The site identifier.</value>
        public int? SiteId { get; set; }
        /// <summary>
        /// ParentTaxonomyAssociationId
        /// </summary>
        /// <value>The parent taxonomy association identifier.</value>
        public int? ParentTaxonomyAssociationId { get; set; }
        /// <summary>
        /// NameOverride
        /// </summary>
        /// <value>The name override.</value>
        public string NameOverride { get; set; }
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
        /// IsProofing
        /// </summary>
        /// <value>The IsProofing identifier.</value>
        public bool IsProofing { get; set; }
        ///<summary>
        /// Order
        /// </summary>
        /// <value>Order</value>
        public int? Order { get; set; }
        ///<summary>
        /// TabbedPageNameOverride
        /// </summary>
        /// <value>Order</value>
        public string TabbedPageNameOverride { get; set; }
        /// <summary>
        /// IsObjectinVerticalMarket
        /// </summary>
        /// <value>IsObjectinVerticalMarket.</value> 
        
        public bool IsObjectinVerticalMarket { get; set; }
        ///<summary>
        /// RP Fund Name
        /// </summary>
        /// <value>Order</value>
        public string RPFundName { get; set; }
        /// <summary>
        /// Compares the two Site entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(TaxonomyAssociationObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
