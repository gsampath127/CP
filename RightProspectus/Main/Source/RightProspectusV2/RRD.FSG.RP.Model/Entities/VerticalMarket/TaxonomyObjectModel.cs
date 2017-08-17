// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

using System;

/// <summary>
/// The VerticalMarket namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.VerticalMarket
{
    /// <summary>
    /// Class TaxonomyObjectModel.
    /// </summary>
    public class TaxonomyObjectModel : BaseModel<TaxonomyKey>, IComparable<TaxonomyObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override TaxonomyKey Key
        {
            get { return new TaxonomyKey(this.TaxonomyId, this.Level); }
            internal set
            {
                if (value.TaxonomyId != this.TaxonomyId)
                {
                    throw new ArgumentOutOfRangeException("Key",
                        string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}",
                        value.TaxonomyId, this.TaxonomyId));
                }

                this.TaxonomyId = value.TaxonomyId;
                this.Level = value.Level;
            }
        }
        /// <summary>
        /// TaxonomyID
        /// </summary>
        /// <value>The taxonomy identifier.</value>
        public int TaxonomyId { get; set; }

        /// <summary>
        /// Level
        /// </summary>
        /// <value>The level.</value>
        public int Level { get; set; }

        /// <summary>
        /// TaxonomyName
        /// </summary>
        /// <value>The name of the taxonomy.</value>
        public string  TaxonomyName { get; set; }

        /// <summary>
        /// Compares the two Taxonomy entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(TaxonomyObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
