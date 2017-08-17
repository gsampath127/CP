// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************
using System;

namespace RRD.FSG.RP.Model.Keys
{
    /// <summary>
    /// Class SiteNavigationKey.
    /// </summary>
    public class SiteNavigationKey : IEquatable<SiteNavigationKey>, IComparable<SiteNavigationKey>, IComparable
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SiteNavigationKey" /> class.
        /// </summary>
        /// <param name="siteNavigationId">The site navigation identifier.</param>
        /// <param name="version">Version of the SiteNavigation</param>
        public SiteNavigationKey(int siteNavigationId, int version)
        {
            this.SiteNavigationId = siteNavigationId;
            this.Version = version;
        }

        #region Public Properties

        /// <summary>
        /// Get the site navigation id of the entity
        /// </summary>
        /// <value>The site navigation identifier.</value>
        public int SiteNavigationId { get; internal set; }

        /// <summary>
        /// Get the version of the entity
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; internal set; }

        #endregion

        #region Public Methods

        #region Equals

        /// <summary>
        /// Compares two <see cref="SiteNavigationKey" /> entities for equality.
        /// </summary>
        /// <param name="other">The other entity to compare against.</param>
        /// <returns>True if the SiteNavigationId and Version are the same.</returns>
        public bool Equals(SiteNavigationKey other)
        {
            return this.SiteNavigationId == other.SiteNavigationId
                && this.Version == other.Version;
        }

        /// <summary>
        /// Compares two <see cref="SiteNavigationKey" /> entities for equality.
        /// </summary>
        /// <param name="obj">The other entity to compare against.</param>
        /// <returns>True if the SiteNavigationId and version are the same.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as SiteNavigationKey);
        }

        #endregion

        /// <summary>
        /// Generates a hash code for the <see cref="T:SiteNavigationkey" /> entity by combining the version and SiteNavigationId properties.
        /// </summary>
        /// <returns>A computed hash code based on th SiteNavigationId and Version properties.</returns>
        public override int GetHashCode()
        {
            return this.SiteNavigationId ^ this.Version;
        }

        #region CompareTo

        /// <summary>
        /// Compares two <see cref="T:SiteNavigationkey" /> entities by SiteNavigationId and Version
        /// </summary>
        /// <param name="other">other key to compare to.</param>
        /// <returns>Zero (0) if this instance has the same SiteNavigationId and version.</returns>
        public int CompareTo(SiteNavigationKey other)
        {
            var diff = this.SiteNavigationId.CompareTo(other.SiteNavigationId);
            if (diff == 0)
            {
                diff = this.Version.CompareTo(other.Version);
            }

            return diff;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Creates a new instance of SiteNavigationkey with the SiteNavigationId and version provided.
        /// </summary>
        /// <param name="siteNavigationId">The site navigation identifier.</param>
        /// <param name="version">version of the key</param>
        /// <returns>new SiteNavigationkey entity</returns>
        public static SiteNavigationKey Create(int siteNavigationId, int version)
        {
            return new SiteNavigationKey(siteNavigationId, version);
        }

        #region Operator Methods

        /// <summary>
        /// Greater than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than key2.</returns>
        public static bool operator >(SiteNavigationKey key1, SiteNavigationKey key2)
        {
            return key1.CompareTo(key2) > 0;
        }

        /// <summary>
        /// Less than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than key2.</returns>
        public static bool operator <(SiteNavigationKey key1, SiteNavigationKey key2)
        {
            return key1.CompareTo(key2) < 0;
        }

        /// <summary>
        /// Greater than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than or equal to key2.</returns>
        public static bool operator >=(SiteNavigationKey key1, SiteNavigationKey key2)
        {
            return key1.CompareTo(key2) >= 0;
        }

        /// <summary>
        /// Less than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than or equal to key2.</returns>
        public static bool operator <=(SiteNavigationKey key1, SiteNavigationKey key2)
        {
            return key1.CompareTo(key2) <= 0;
        }

        /// <summary>
        /// Equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is equal to key2.</returns>
        public static bool operator ==(SiteNavigationKey key1, SiteNavigationKey key2)
        {
            return key1.CompareTo(key2) == 0;
        }

        /// <summary>
        /// Not equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is not equal to key2.</returns>
        public static bool operator !=(SiteNavigationKey key1, SiteNavigationKey key2)
        {
            return key1.CompareTo(key2) != 0;
        }

        #endregion

        #endregion
    }
}
