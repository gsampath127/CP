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
    /// Class TemplatePageFeatureObjectModel.
    /// </summary>
    public class TemplatePageFeatureObjectModel : BaseModel<TemplatePageFeatureKey>, IComparable<TemplatePageFeatureObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override TemplatePageFeatureKey Key
        {
            get { return new TemplatePageFeatureKey(this.TemplateId,this.PageId, this.FeatureKey); }
            internal set
            {
                if (value.TemplateId != this.TemplateId)
                {
                    throw new ArgumentOutOfRangeException("Key",
                            string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", value.TemplateId, this.TemplateId));
                }
                this.TemplateId = value.TemplateId;
                this.PageId = value.PageId;
                this.FeatureKey = value.FeatureKey;
            }
        }

        /// <summary>
        /// TemplateId
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateId { get; set; }

        /// <summary>
        /// PageId
        /// </summary>
        /// <value>The page identifier.</value>
        public int PageId { get; set; }
        /// <summary>
        /// TemplateFeatureKey
        /// </summary>
        /// <value>The feature key.</value>
        public string FeatureKey { get; set; }
        /// <summary>
        /// TemplateFeatureDescription
        /// </summary>
        /// <value>The feature description.</value>
        public string FeatureDescription { get; set; }

        /// <summary>
        /// Compares the two TemplatePageFeature entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(TemplatePageFeatureObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }


    }
}
