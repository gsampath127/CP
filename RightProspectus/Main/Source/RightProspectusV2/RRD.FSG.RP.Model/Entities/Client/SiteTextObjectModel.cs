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
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class SiteTextObjectModel.
    /// </summary>
    public class SiteTextObjectModel : AuditedBaseModel<SiteTextKey>, IComparable<SiteTextObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override SiteTextKey Key
        {
            get { return new SiteTextKey(this.SiteTextID, this.Version); }
            internal set
            {
                if (value.SiteTextId != this.SiteTextID)
                {
                    throw new ArgumentOutOfRangeException("Key", string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", value.SiteTextId, this.SiteTextID));
                }

                this.Version = value.Version;
            }
        }
        /// <summary>
        /// SiteTextID
        /// </summary>
        /// <value>The site text identifier.</value>
        public int SiteTextID { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; set; }
        /// <summary>
        /// SiteID
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteID { get; set; }
        /// <summary>
        /// Site Name
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
        /// Is ProofingVersion Available for Site textID
        /// </summary>
        /// <value><c>true</c> if this instance is proofing available for site text identifier; otherwise, <c>false</c>.</value>
        public bool IsProofingAvailableForSiteTextId { get; set; }
        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(SiteTextObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
        
    }
}
