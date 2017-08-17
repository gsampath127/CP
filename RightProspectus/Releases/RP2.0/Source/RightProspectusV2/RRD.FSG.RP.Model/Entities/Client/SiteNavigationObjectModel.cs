// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************

using RRD.FSG.RP.Model.Keys;
using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class SiteNavigationObjectModel.
    /// </summary>
    public class SiteNavigationObjectModel : AuditedBaseModel<SiteNavigationKey>, IComparable<SiteNavigationObjectModel>
    {

        #region Entity Properties


        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override SiteNavigationKey Key
        {

            get { return new SiteNavigationKey(this.SiteNavigationId, this.Version); }
            internal set
            {
                if (value.SiteNavigationId != this.SiteNavigationId)
                {
                    throw new ArgumentOutOfRangeException("Key", string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", value.SiteNavigationId, this.SiteNavigationId));
                }

                this.Version = value.Version;
            }
        }
        /// <summary>
        /// Gets or sets the SiteNavigationId identifier.
        /// </summary>
        /// <value>The SiteNavigationId identifier.</value>
        public int SiteNavigationId { get; set; }

        /// <summary>
        /// Gets or sets the SiteId identifier.
        /// </summary>
        /// <value>The SiteId identifier.</value>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets the NavigationKey.
        /// </summary>
        /// <value>The NavigationKey.</value>
        public string NavigationKey { get; set; }
        /// <summary>
        /// Gets or sets the NavigationXML.
        /// </summary>
        /// <value>The NavigationXML.</value>
        public string NavigationXML { get; set; }


        /// <summary>
        /// Gets or sets the LanguageCulture.
        /// </summary>
        /// <value>The LanguageCulture.</value>
        public string LanguageCulture { get; set; }

        /// <summary>
        /// Gets or sets the PageId identifier.
        /// </summary>
        /// <value>The PageId identifier.</value>
        public int? PageId { get; set; }

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
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; set; }

        /// <summary>
        /// IsProofing Version
        /// </summary>
        /// <value><c>true</c> if this instance is proofing; otherwise, <c>false</c>.</value>
        public bool IsProofing { get; set; }

        /// <summary>
        /// Is ProofingVersion Available for SiteNavigationId
        /// </summary>
        /// <value><c>true</c> if this instance is proofing available for site navigation identifier; otherwise, <c>false</c>.</value>
        public bool IsProofingAvailableForSiteNavigationId { get; set; }

        //public int Key { get; set; }

        #endregion


        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.</returns>
        public int CompareTo(SiteNavigationObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }

        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateId { get; set; }
    }
}
