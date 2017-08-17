using RRD.FSG.RP.Model.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.System
{
    public class TemplateSiteNavigationTextObjectModel : BaseModel<TemplateNavigationKey>, IComparable<TemplateSiteNavigationTextObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        public override TemplateNavigationKey Key
        {
            get { return new TemplateNavigationKey(this.TemplateID, this.PageID, this.NavigationKey); }
            internal set
            {
                if (value.TemplateId != this.TemplateID)
                {
                    throw new ArgumentOutOfRangeException("Key",
                            string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", value.TemplateId, this.TemplateID));
                }

                this.PageID = value.PageId;

                this.NavigationKey = value.NavigationKey;
            }
        }
        /// <summary>
        /// PageID
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// TemplateID
        /// </summary>
        public int TemplateID { get; set; }
        /// <summary>
        /// ResourceKey
        /// </summary>
        public string NavigationKey { get; set; }
        /// <summary>
        /// IsHTML
        /// </summary>
        public bool IsHTML { get; set; }
        /// <summary>
        /// PageName
        /// </summary>
        public string DefaultText { get; set; }
        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(TemplateSiteNavigationTextObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
