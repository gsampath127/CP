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
    /// Class TaxonomyAssociationHierarchyObjectModel.
    /// </summary>
    public class TaxonomyAssociationHierarchyObjectModel : AuditedBaseModel<TaxonomyAssociationHierarchyKey>, IComparable<TaxonomyAssociationHierarchyObjectModel>
    {
        /// <summary>
        /// Key
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override TaxonomyAssociationHierarchyKey Key
        {
            get
            {
                return new TaxonomyAssociationHierarchyKey(this.ParentTaxonomyAssociationId, this.ChildTaxonomyAssociationId,this.RelationshipType);
            }
            internal set
            {
                if (value.ParentTaxonomyAssociationId != this.ParentTaxonomyAssociationId)
                {
                    throw new ArgumentOutOfRangeException("Key", string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", value.ParentTaxonomyAssociationId, this.ParentTaxonomyAssociationId));
                }

                this.ChildTaxonomyAssociationId = value.ChildTaxonomyAssociationId;

                this.RelationshipType = value.RelationshipType;
            }
        }

        /// <summary>
        /// ParentTaxonomyAssociationId
        /// </summary>
        /// <value>The parent taxonomy association identifier.</value>
        public int ParentTaxonomyAssociationId { get; set; }

        /// <summary>
        /// ChildTaxonomyAssociationId
        /// </summary>
        /// <value>The child taxonomy association identifier.</value>
        public int ChildTaxonomyAssociationId { get; set; }
        /// <summary>
        /// RelationshipType
        /// </summary>
        /// <value>The type of the relationship.</value>
        public int RelationshipType { get; set; }
        /// <summary>
        /// Order
        /// </summary>
        /// <value>The order.</value>
        public int? Order { get; set; }
        /// <summary>
        /// CompareTo
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.</returns>
        public int CompareTo(TaxonomyAssociationHierarchyObjectModel other)
        {
            return this.Key.CompareTo(other.Key);

        }
    }
}
