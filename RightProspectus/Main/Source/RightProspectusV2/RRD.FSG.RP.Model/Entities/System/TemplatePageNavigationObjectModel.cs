// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 10-29-2015
// ***********************************************************************

using System;

/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Class TemplatePageNavigationObjectModel.
    /// </summary>
    public class TemplatePageNavigationObjectModel : BaseModel<TemplatePageNavigationKey>, IComparable<TemplatePageNavigationObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override TemplatePageNavigationKey Key
        {
            get { return new TemplatePageNavigationKey(this.TemplateID, this.PageID,this.NavigationKey); }
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
        /// <value>The page identifier.</value>
        public int PageID { get; set; }
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
        /// Gets or sets the page description.
        /// </summary>
        /// <value>The page description.</value>
        public string PageDescription { get; set; }
        /// <summary>
        /// PageName
        /// </summary>
        /// <value>The name of the page.</value>
        public string PageName { get; set; }
        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.</returns>
        public int CompareTo(TemplatePageNavigationObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }

    }
}
