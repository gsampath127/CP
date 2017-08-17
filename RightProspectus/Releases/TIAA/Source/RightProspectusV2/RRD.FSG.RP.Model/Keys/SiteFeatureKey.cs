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
    /// Class SiteFeatureKey.
    /// </summary>
    public class SiteFeatureKey : IEquatable<SiteFeatureKey>, IComparable<SiteFeatureKey>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SiteFeature" /> class.
        /// </summary>
        /// <param name="SiteId">SiteId of the entity.</param>
        /// <param name="Key">The key.</param>
        public SiteFeatureKey(int SiteId, string Key)
        {
            this.SiteId = SiteId;
            this.SiteKey = Key;
        }

        #region Public Properties

        /// <summary>
        /// Gets the SiteId of the entity.
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteId { get; internal set; }

        /// <summary>
        /// Gets the Key of the entity.
        /// </summary>
        /// <value>The site key.</value>
        public string SiteKey { get; internal set; }


        #endregion

        #region Public Methods

        #region Equals
        /// <summary>
        /// Compares two <see cref="SiteFeatureKey" /> entities for equality.
        /// </summary>
        /// <param name="other">The other entity to compare against.</param>
        /// <returns>True if the SiteId and Key are the same.</returns>
        public bool Equals(SiteFeatureKey other)
        {
            return this.SiteId == other.SiteId &&
                 this.SiteKey == other.SiteKey;
        }

        /// <summary>
        /// Compares two <see cref="SiteFeatureKey" /> entities for equality.
        /// </summary>
        /// <param name="obj">The other entity to compare against.</param>
        /// <returns>True if the SiteId and Key are the same.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as SiteFeatureKey);
        }

        /// <summary>
        /// Generates a hash code for the <see cref="T:SiteFeatureKey" /> entity by combining the SiteId and Key properties.
        /// </summary>
        /// <returns>A computed hash code based on th SiteId and Key properties.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = hash * 16777619 ^ this.SiteId;
                hash = hash * this.SiteKey.GetHashCode();
                return hash;
            }

        }

        #endregion

        #region CompareTo
        /// <summary>
        /// Compares two <see cref="T:SiteFeatureKey" /> entities by SiteId and Key
        /// </summary>
        /// <param name="other">other key to compare to.</param>
        /// <returns>A negative value if this instance has a lower level or same level and lover site id
        /// A positive value if this instance has a higher level or same level and higher site id.
        /// Zero (0) if this instance has the same level and site id.</returns>
        public int CompareTo(SiteFeatureKey other)
        {
            var diff = this.SiteId.CompareTo(other.SiteId);
            if (diff == 0)
            {
                diff = this.SiteKey.CompareTo(other.SiteKey);
            }

            return diff;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        int IComparable.CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region Public Static Methods
        /// <summary>
        /// Creates a new instance of SiteFeatureKey with the values provided.
        /// </summary>
        /// <param name="SiteId">SiteId</param>
        /// <param name="SiteKey">The site key.</param>
        /// <returns>a new SiteFeatureKey entity</returns>
        public static SiteFeatureKey Create(int SiteId, string SiteKey)
        {
            return new SiteFeatureKey(SiteId, SiteKey);
        }
       
        #region Operator Methods
        /// <summary>
        /// Greater than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than key2.</returns>
        public static bool operator >(SiteFeatureKey key1, SiteFeatureKey key2)
        {
            return key1.CompareTo(key2) > 0;
        }

        /// <summary>
        /// Less than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than key2.</returns>
        public static bool operator <(SiteFeatureKey key1, SiteFeatureKey key2)
        {
            return key1.CompareTo(key2) < 0;
        }

        /// <summary>
        /// Less than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than key2.</returns>
        public static bool operator >=(SiteFeatureKey key1, SiteFeatureKey key2)
        {
            return key1.CompareTo(key2) >= 0;
        }

        /// <summary>
        /// Less than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than or equal to key2.</returns>
        public static bool operator <=(SiteFeatureKey key1, SiteFeatureKey key2)
        {
            return key1.CompareTo(key2) <= 0;
        }

        /// <summary>
        /// Equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is equal to key2.</returns>
        public static bool operator ==(SiteFeatureKey key1, SiteFeatureKey key2)
        {
            return key1.CompareTo(key2) == 0;
        }

        /// <summary>
        /// Not equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is not equal to key2.</returns>
        public static bool operator !=(SiteFeatureKey key1, SiteFeatureKey key2)
        {
            return key1.CompareTo(key2) != 0;
        }

        #endregion

        #endregion
       
    }
}
