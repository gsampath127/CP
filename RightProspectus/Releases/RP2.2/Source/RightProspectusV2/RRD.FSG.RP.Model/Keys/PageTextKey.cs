// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************
using System;

namespace RRD.FSG.RP.Model
{
    /// <summary>
    /// Class PageTextKey.
    /// </summary>
    public class PageTextKey : IEquatable<PageTextKey>, IComparable<PageTextKey>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:PageTextKey" /> class.
        /// </summary>
        /// <param name="pageTextId">The page text identifier.</param>
        /// <param name="version">The version.</param>
        public PageTextKey(int pageTextId, int version)
        {
            this.PageTextId = pageTextId;
            this.Version = version;
        }

        #region Public Properties

        /// <summary>
        /// Gets the Page Text Id of the entity.
        /// </summary>
        /// <value>The page text identifier.</value>
        public int PageTextId { get; internal set; }

        /// <summary>
        /// Gets the version of the entity.
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; internal set; }

        #endregion


        #region Public Methods

        #region Equals

        /// <summary>
        /// Compares two <see cref="PageTextKey" /> entities for equality.
        /// </summary>
        /// <param name="other">The other entity to compare against.</param>
        /// <returns>True if the PageTextId and Version are the same.</returns>
        public bool Equals(PageTextKey other)
        {
            return this.PageTextId == other.PageTextId
                && this.Version == other.Version;
        }

        /// <summary>
        /// Compares two <see cref="PageTextKey" /> entities for equality.
        /// </summary>
        /// <param name="obj">The other entity to compare against.</param>
        /// <returns>True if the pagetextid and version are the same.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as PageTextKey);
        }

        #endregion

        /// <summary>
        /// Generates a hash code for the <see cref="T:PageTextKey" /> entity by combining the version and pagetextid properties.
        /// </summary>
        /// <returns>A computed hash code based on th PageTextId and Version properties.</returns>
        public override int GetHashCode()
        {
            return this.PageTextId ^ this.Version;
        }

        #region CompareTo

        /// <summary>
        /// Compares two <see cref="T:PageTextKey" /> entities by PageTextId and Version
        /// </summary>
        /// <param name="other">other key to compare to.</param>
        /// <returns>Zero (0) if this instance has the same pagetextid and version.</returns>
        public int CompareTo(PageTextKey other)
        {
            var diff = this.PageTextId.CompareTo(other.PageTextId);
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
        /// Creates a new instance of PageTextKey with the values provided.
        /// </summary>
        /// <param name="pagetextID">The pagetext identifier.</param>
        /// <param name="version">The version.</param>
        /// <returns>a new PageTextKey entity</returns>
        public static PageTextKey Create(int pagetextID, int version)
        {
            return new PageTextKey(pagetextID, version);
        }

        #region Operator Methods

        /// <summary>
        /// Greater than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than key2.</returns>
        public static bool operator >(PageTextKey key1, PageTextKey key2)
        {
            return key1.CompareTo(key2) > 0;
        }

        /// <summary>
        /// Less than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than key2.</returns>
        public static bool operator <(PageTextKey key1, PageTextKey key2)
        {
            return key1.CompareTo(key2) < 0;
        }

        /// <summary>
        /// Greater than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than or equal to key2.</returns>
        public static bool operator >=(PageTextKey key1, PageTextKey key2)
        {
            return key1.CompareTo(key2) >= 0;
        }

        /// <summary>
        /// Less than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than or equal to key2.</returns>
        public static bool operator <=(PageTextKey key1, PageTextKey key2)
        {
            return key1.CompareTo(key2) <= 0;
        }

        /// <summary>
        /// Equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is equal to key2.</returns>
        public static bool operator ==(PageTextKey key1, PageTextKey key2)
        {
            return key1.CompareTo(key2) == 0;
        }

        /// <summary>
        /// Not equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is not equal to key2.</returns>
        public static bool operator !=(PageTextKey key1, PageTextKey key2)
        {
            return key1.CompareTo(key2) != 0;
        }

        #endregion

        #endregion


    }
}
