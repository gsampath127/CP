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
    /// Class PageTextObjectModel.
    /// </summary>
    public class PageTextObjectModel : AuditedBaseModel<PageTextKey>, IComparable<PageTextObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override PageTextKey Key
        {
            get { return new PageTextKey(this.PageTextID, this.Version); }
            internal set
            {
                if (value.PageTextId != this.PageTextID)
                {
                    throw new ArgumentOutOfRangeException("Key", string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", value.PageTextId, this.PageTextID));
                }

                this.Version = value.Version;
            }
        }
        /// <summary>
        /// PageTextID
        /// </summary>
        /// <value>The page text identifier.</value>
        public int PageTextID { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; set; }
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
        /// SiteID
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteID { get; set; }
        /// <summary>
        /// SiteName
        /// </summary>
        /// <value>The name of the site.</value>
        public string SiteName { get; set; }
        /// <summary>
        /// ResourceKey
        /// </summary>
        /// <value>The resource key.</value>
        public string ResourceKey { get; set; }
        /// <summary>
        /// Text for the Resource key
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        /// <summary>
        /// IsProofing Version
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
        /// Is ProofingVersion Available for Site textID
        /// </summary>
        /// <value><c>true</c> if this instance is proofing available for page text identifier; otherwise, <c>false</c>.</value>
        public bool IsProofingAvailableForPageTextId { get; set; }
        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(PageTextObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
