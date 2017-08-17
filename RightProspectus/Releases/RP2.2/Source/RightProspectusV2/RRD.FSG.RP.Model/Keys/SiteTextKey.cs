// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using System;

namespace RRD.FSG.RP.Model
{
    /// <summary>
    /// Class SiteTextKey.
    /// </summary>
    public class SiteTextKey : IEquatable<SiteTextKey>, IComparable<SiteTextKey>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SiteTextKey" /> class.
        /// </summary>
        /// <param name="siteTextId">The site text identifier.</param>
        /// <param name="version">The version.</param>
        public SiteTextKey(int siteTextId, int version)
        {
            this.SiteTextId = siteTextId;
            this.Version = version;
        }

        #region Public Properties

        /// <summary>
        /// Gets the hierarchical taxonomy level of the entity.
        /// </summary>
        /// <value>The site text identifier.</value>
        public int SiteTextId { get; internal set; }

        /// <summary>
        /// Gets the taxonomy level specific identifier of the entity.
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; internal set; }

        #endregion


        #region Public Methods

        #region Equals

        /// <summary>
        /// Compares two <see cref="SiteTextKey" /> entities for equality.
        /// </summary>
        /// <param name="other">The other entity to compare against.</param>
        /// <returns>True if the Level and TaxonomyId are the same.</returns>
        public bool Equals(SiteTextKey other)
        {
            return this.SiteTextId == other.SiteTextId
                && this.Version == other.Version;
        }

        /// <summary>
        /// Compares two <see cref="SiteTextKey" /> entities for equality.
        /// </summary>
        /// <param name="obj">The other entity to compare against.</param>
        /// <returns>True if the Level and TaxonomyId are the same.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as SiteTextKey);
        }

        #endregion

        /// <summary>
        /// Generates a hash code for the <see cref="T:SiteTextKey" /> entity by combining the Level and TaxonomyId properties.
        /// </summary>
        /// <returns>A computed hash code based on th Level and TaxonomyId properties.</returns>
        public override int GetHashCode()
        {
            return this.SiteTextId ^ this.Version;
        }

        #region CompareTo

        /// <summary>
        /// Compares two <see cref="T:SiteTextKey" /> entities by Level and Taxonomy Id
        /// </summary>
        /// <param name="other">other key to compare to.</param>
        /// <returns>A negative value if this instance has a lower level or same level and lover taxonomy id
        /// A positive value if this instance has a higher level or same level and higher taxonomy id.
        /// Zero (0) if this instance has the same level and taxonomy id.</returns>
        public int CompareTo(SiteTextKey other)
        {
            var diff = this.SiteTextId.CompareTo(other.SiteTextId);
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
        /// Creates a new instance of SiteTextKey with the values provided.
        /// </summary>
        /// <param name="sitetextID">The sitetext identifier.</param>
        /// <param name="version">The version.</param>
        /// <returns>a new SiteTextKey entity</returns>
        public static SiteTextKey Create(int sitetextID, int version)
        {
            return new SiteTextKey(sitetextID, version);
        }

        #region Operator Methods

        /// <summary>
        /// Greater than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than key2.</returns>
        public static bool operator >(SiteTextKey key1, SiteTextKey key2)
        {
            return key1.CompareTo(key2) > 0;
        }

        /// <summary>
        /// Less than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than key2.</returns>
        public static bool operator <(SiteTextKey key1, SiteTextKey key2)
        {
            return key1.CompareTo(key2) < 0;
        }

        /// <summary>
        /// Greater than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than or equal to key2.</returns>
        public static bool operator >=(SiteTextKey key1, SiteTextKey key2)
        {
            return key1.CompareTo(key2) >= 0;
        }

        /// <summary>
        /// Less than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than or equal to key2.</returns>
        public static bool operator <=(SiteTextKey key1, SiteTextKey key2)
        {
            return key1.CompareTo(key2) <= 0;
        }

        /// <summary>
        /// Equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is equal to key2.</returns>
        public static bool operator ==(SiteTextKey key1, SiteTextKey key2)
        {
            return key1.CompareTo(key2) == 0;
        }

        /// <summary>
        /// Not equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is not equal to key2.</returns>
        public static bool operator !=(SiteTextKey key1, SiteTextKey key2)
        {
            return key1.CompareTo(key2) != 0;
        }

        #endregion

        #endregion


    }
}
