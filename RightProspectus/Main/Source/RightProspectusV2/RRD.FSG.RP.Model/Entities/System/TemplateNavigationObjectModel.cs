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
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Class TemplateNavigationObjectModel.
    /// </summary>
    public class TemplateNavigationObjectModel : BaseModel<TemplateNavigationKey>, IComparable<TemplateNavigationObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key
        /// or
        /// Key</exception>
        public override TemplateNavigationKey Key
        {
            get { return new TemplateNavigationKey(this.TemplateID, this.NavigationKey); }
            internal set
            {
                if (value.TemplateId != this.TemplateID)
                {
                    throw new ArgumentOutOfRangeException("Key",
                            string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", value.TemplateId, this.TemplateID));
                }

                if (value.NavigationKey != this.NavigationKey)
                {
                    throw new ArgumentOutOfRangeException("Key",
                            string.Format("The second part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", value.NavigationKey, this.NavigationKey));
                }

                this.TemplateID = value.TemplateId;
                this.NavigationKey = value.NavigationKey;
            }
        }

        /// <summary>
        /// TemplateID
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateID { get; set; }

        /// <summary>
        /// NavigationKey
        /// </summary>
        /// <value>The navigation key.</value>
        public string NavigationKey { get; set; }

        /// <summary>
        /// XslTransform
        /// </summary>
        /// <value>The XSL transform.</value>
        public string XslTransform { get; set; }

        /// <summary>
        /// DefaultNavigationXml
        /// </summary>
        /// <value>The default navigation XML.</value>
        public string DefaultNavigationXml { get; set; }

        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(TemplateNavigationObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }

        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        /// <value>The page identifier.</value>
        public int PageID { get; set; }
    }
}
