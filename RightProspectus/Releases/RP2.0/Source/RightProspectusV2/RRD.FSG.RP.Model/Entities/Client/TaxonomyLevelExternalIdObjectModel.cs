// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************

using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class TaxonomyLevelExternalIdObjectModel.
    /// </summary>
    public class TaxonomyLevelExternalIdObjectModel : AuditedBaseModel<TaxonomyLevelExternalIdKey>, IComparable<TaxonomyLevelExternalIdObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override TaxonomyLevelExternalIdKey Key
        {
            get { return new TaxonomyLevelExternalIdKey(this.TaxonomyId,this.Level, this.ExternalId); }
            internal set
            {
                if (value.TaxonomyId != this.TaxonomyId)
                {
                    throw new ArgumentOutOfRangeException("Key",
                        string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}",
                        value.TaxonomyId, this.TaxonomyId));
                }

                this.ExternalId = value.ExternalId;
                this.TaxonomyId = value.TaxonomyId;
                this.Level = value.Level;
            }
        }
        /// <summary>
        /// TaxonomyId
        /// </summary>
        /// <value>The taxonomy identifier.</value>
        public int TaxonomyId { get; set; }
        /// <summary>
        /// TaxonomyName
        /// </summary>
        /// <value>The name of the taxonomy.</value>
        public string TaxonomyName { get; set; }
        /// <summary>
        /// Level
        /// </summary>
        /// <value>The level.</value>
        public int Level { get; set; }
        /// <summary>
        /// ExternalId
        /// </summary>
        /// <value>The external identifier.</value>
        public string ExternalId { get; set; }
        /// <summary>
        /// IsPrimary
        /// </summary>
        /// <value><c>true</c> if this instance is primary; otherwise, <c>false</c>.</value>
        public bool IsPrimary { get; set; }
        /// <summary>
        /// Compares the two TaxonomyLevelExternalIdObjectModel entities by their TaxonomyLevelExternalIdKey identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(TaxonomyLevelExternalIdObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
