// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************

using RRD.FSG.RP.Model.Keys;
using System;

/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Class TemplateFeatureObjectModel.
    /// </summary>
    public class TemplateFeatureObjectModel : BaseModel<TemplateFeatureKey>, IComparable<TemplateFeatureObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override TemplateFeatureKey Key
        {
            get { return new TemplateFeatureKey(this.TemplateId, this.FeatureKey); }
            internal set
            {
                if (value.TemplateId != this.TemplateId)
                {
                    throw new ArgumentOutOfRangeException("Key",
                            string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", value.TemplateId, this.TemplateId));
                }
                this.TemplateId = value.TemplateId;
                this.FeatureKey = value.FeatureKey;
            }
        }

        /// <summary>
        /// TemplateId
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateId { get; set; }
        /// <summary>
        /// TemplateFeatureKey
        /// </summary>
        /// <value>The feature key.</value>
        public string  FeatureKey { get; set; }
        /// <summary>
        /// TemplateFeatureDescription
        /// </summary>
        /// <value>The feature description.</value>
        public string FeatureDescription { get; set; }

        /// <summary>
        /// Compares the two TemplateFeature entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(TemplateFeatureObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
      
       
    }
}
