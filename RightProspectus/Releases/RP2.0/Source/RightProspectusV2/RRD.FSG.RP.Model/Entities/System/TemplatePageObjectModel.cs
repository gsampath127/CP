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
    /// Class TemplatePageObjectModel.
    /// </summary>
    public class TemplatePageObjectModel : BaseModel<TemplatePageKey>, IComparable<TemplatePageObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override TemplatePageKey Key
        {
            get { return new TemplatePageKey(this.TemplateID, this.PageID); }
            internal set
            {
                if (value.TemplateId != this.TemplateID)
                {
                    throw new ArgumentOutOfRangeException("Key", 
                            string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", value.TemplateId, this.TemplateID));
                }

                this.PageID = value.PageId;
            }
        }
        /// <summary>
        /// PageID
        /// </summary>
        /// <value>The page identifier.</value>
        public int PageID { get; set; }
        /// <summary>
        /// TemplateID
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateID { get; set; }
        /// <summary>
        /// TemplateName
        /// </summary>
        /// <value>The name of the template.</value>
        public string TemplateName { get; set; }
        /// <summary>
        /// PageName
        /// </summary>
        /// <value>The name of the page.</value>
        public string PageName { get; set; }
        /// <summary>
        /// PageDescription
        /// </summary>
        /// <value>The page description.</value>
        public string PageDescription { get; set; }


        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(TemplatePageObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
