// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************

using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{

    /// <summary>
    /// Entity model for Page Navigation
    /// </summary>
    public class PageNavigationObjectModel : AuditedBaseModel<PageNavigationKey>, IComparable<PageNavigationObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override PageNavigationKey Key
        {
            get
            {
                return new PageNavigationKey(this.PageNavigationId, this.Version); 
            }
            internal set
            {
                if (value.PageNavigationId != this.PageNavigationId)
                {
                    throw new ArgumentOutOfRangeException("Key", string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", value.PageNavigationId, this.PageNavigationId));
                }

                this.Version = value.Version;
            }
        }

        /// <summary>
        /// Gets or sets the page navigation identifier.
        /// </summary>
        /// <value>The page navigation identifier.</value>
        public int PageNavigationId { get; set; }
        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        /// <value>The page identifier.</value>
        public int PageId { get; set; }
        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteId { get; set; }
        /// <summary>
        /// Gets the identifier of the user who last modified the entity.
        /// </summary>
        /// <value>The modified by.</value>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the navigation key.
        /// </summary>
        /// <value>The navigation key.</value>
        public string NavigationKey { get; set; }
        /// <summary>
        /// Gets or sets the navigation XML.
        /// </summary>
        /// <value>The navigation XML.</value>
        public string NavigationXML { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is proofing.
        /// </summary>
        /// <value><c>true</c> if this instance is proofing; otherwise, <c>false</c>.</value>
        public bool IsProofing { get; set; }
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
        /// Gets or sets a value indicating whether this instance is proofing available for page navigation identifier.
        /// </summary>
        /// <value><c>true</c> if this instance is proofing available for page navigation identifier; otherwise, <c>false</c>.</value>
        public bool IsProofingAvailableForPageNavigationID { get; set; }
        /// <summary>
        /// Gets or sets the UTC modified date.
        /// </summary>
        /// <value>The UTC modified date.</value>
        public DateTime UtcModifiedDate { get; set; }
        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.</returns>
        public int CompareTo(PageNavigationObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
            
        }
    }
}
