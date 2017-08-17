// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************
using System;

namespace RRD.FSG.RP.Model
{
    /// <summary>
    /// Class PageNavigationKey.
    /// </summary>
    public class PageNavigationKey : IEquatable<PageNavigationKey>, IComparable<PageNavigationKey>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:PageNavigationKey" /> class.
        /// </summary>
        /// <param name="pageNavigationId">PageNavigationId of the entity</param>
        /// <param name="version">Version of the page navigation</param>
        public PageNavigationKey(int pageNavigationId, int version)
        {
            this.PageNavigationId = pageNavigationId;
            this.Version = version;
        }

        #region Public Properties

        /// <summary>
        /// Get the page navigation id of the entity
        /// </summary>
        /// <value>The page navigation identifier.</value>
        public int PageNavigationId { get; internal set; }

        /// <summary>
        /// Get the version of the entity
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; internal set; }

        #endregion

        #region Public Methods

        #region Equals

        /// <summary>
        /// Compares two <see cref="PageNavigationKey" /> entities for equality.
        /// </summary>
        /// <param name="other">The other entity to compare against.</param>
        /// <returns>True if the PageNavigationId and Version are the same.</returns>
        public bool Equals(PageNavigationKey other)
        {
            return this.PageNavigationId == other.PageNavigationId
                && this.Version == other.Version;
        }

        /// <summary>
        /// Compares two <see cref="PageNavigationKey" /> entities for equality.
        /// </summary>
        /// <param name="obj">The other entity to compare against.</param>
        /// <returns>True if the page  navigation id and version are the same.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as PageNavigationKey);
        }

        #endregion

        /// <summary>
        /// Generates a hash code for the <see cref="T:PageNavigationKey" /> entity by combining the version and pagenavigationid properties.
        /// </summary>
        /// <returns>A computed hash code based on th PageNavigationId and Version properties.</returns>
        public override int GetHashCode()
        {
            return this.PageNavigationId ^ this.Version;
        }

        #region CompareTo

        /// <summary>
        /// Compares two <see cref="T:PageNavigationKey" /> entities by PageNavigationId and Version
        /// </summary>
        /// <param name="other">other key to compare to.</param>
        /// <returns>Zero (0) if this instance has the same pagenavigationid and version.</returns>
        public int CompareTo(PageNavigationKey other)
        {
            var diff = this.PageNavigationId.CompareTo(other.PageNavigationId);
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
        /// Creates a new instance of PageNavigationKey with the pageNavigationId and version provided.
        /// </summary>
        /// <param name="pageNavigationId">identifier of the Key</param>
        /// <param name="version">version of the key</param>
        /// <returns>new PagenavigationKey entity</returns>
        public static PageNavigationKey Create(int pageNavigationId, int version)
        {
            return new PageNavigationKey(pageNavigationId, version);
        }

        #region Operator Methods

        /// <summary>
        /// Greater than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than key2.</returns>
        public static bool operator >(PageNavigationKey key1, PageNavigationKey key2)
        {
            return key1.CompareTo(key2) > 0;
        }

        /// <summary>
        /// Less than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than key2.</returns>
        public static bool operator <(PageNavigationKey key1, PageNavigationKey key2)
        {
            return key1.CompareTo(key2) < 0;
        }

        /// <summary>
        /// Greater than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than or equal to key2.</returns>
        public static bool operator >=(PageNavigationKey key1, PageNavigationKey key2)
        {
            return key1.CompareTo(key2) >= 0;
        }

        /// <summary>
        /// Less than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than or equal to key2.</returns>
        public static bool operator <=(PageNavigationKey key1, PageNavigationKey key2)
        {
            return key1.CompareTo(key2) <= 0;
        }

        /// <summary>
        /// Equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is equal to key2.</returns>
        public static bool operator ==(PageNavigationKey key1, PageNavigationKey key2)
        {
            return key1.CompareTo(key2) == 0;
        }

        /// <summary>
        /// Not equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is not equal to key2.</returns>
        public static bool operator !=(PageNavigationKey key1, PageNavigationKey key2)
        {
            return key1.CompareTo(key2) != 0;
        }

        #endregion

        #endregion
    }
}
