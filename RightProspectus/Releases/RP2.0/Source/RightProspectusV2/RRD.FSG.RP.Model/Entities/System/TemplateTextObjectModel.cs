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
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Class TemplateTextObjectModel.
    /// </summary>
    public class TemplateTextObjectModel : BaseModel<TemplateTextKey>, IComparable<TemplateTextObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override TemplateTextKey Key
        {
            get { return new TemplateTextKey(this.TemplateID, this.ResourceKey); }
            internal set
            {
                if (value.TemplateId != this.TemplateID)
                {
                    throw new ArgumentOutOfRangeException("Key",
                            string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", value.TemplateId, this.TemplateID));
                }

                this.ResourceKey = value.ResourceKey;
            }
        }
        /// <summary>
        /// TemplateID
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateID { get; set; }
        /// <summary>
        /// ResourceKey
        /// </summary>
        /// <value>The resource key.</value>
        public string ResourceKey { get; set; }
        /// <summary>
        /// IsHTML
        /// </summary>
        /// <value><c>true</c> if this instance is HTML; otherwise, <c>false</c>.</value>
        public bool IsHTML { get; set; }
        /// <summary>
        /// PageName
        /// </summary>
        /// <value>The default text.</value>
        public string DefaultText { get; set; }


        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(TemplateTextObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
