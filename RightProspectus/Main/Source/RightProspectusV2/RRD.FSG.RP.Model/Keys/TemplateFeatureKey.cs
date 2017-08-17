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
    /// Class TemplateFeatureKey.
    /// </summary>
    public class TemplateFeatureKey : IEquatable<TemplateFeatureKey>, IComparable<TemplateFeatureKey>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TemplateFeatureKey" /> class.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <param name="featureKey">The feature key.</param>

        public TemplateFeatureKey(int templateId, string featureKey)
        {
            this.TemplateId = templateId;
            this.FeatureKey = featureKey;

        }

        #region Public Properties
        /// <summary>
        /// Gets the TemplateId of the entity.
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateId { get; internal set; }

        /// <summary>
        /// Gets the Key of the entity.
        /// </summary>
        /// <value>The feature key.</value>
        public string FeatureKey { get; internal set; }
        #endregion


        #region Public Methods

        #region Equals
        /// <summary>
        /// Compares two <see cref="TemplateFeatureKey" /> entities for equality.
        /// </summary>
        /// <param name="other">The other entity to compare against.</param>
        /// <returns>True if the TemplateId and TemplateFeatureKey are the same.</returns>
        public bool Equals(TemplateFeatureKey other)
        {
            return this.TemplateId == other.TemplateId
                && this.FeatureKey == other.FeatureKey;
        }
        /// <summary>
        /// Compares two <see cref="TemplateFeatureKey" /> entities for equality.
        /// </summary>
        /// <param name="obj">The other entity to compare against.</param>
        /// <returns>True if the TempalteId and  TemplateFeatureKey are the same.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as TemplateFeatureKey);
        }

        /// <summary>
        /// Generates a hash code for the <see cref="T:TemplateFeatureKey" /> entity by combining the Te and TemplatePageid properties.
        /// </summary>
        /// <returns>A computed hash code based on th TemplateId and PageId properties.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = hash * 16777619 ^ this.TemplateId;
                hash = hash * this.FeatureKey.GetHashCode();
                return hash;
            }
        }
        #endregion

        #region CompareTo
        /// <summary>
        /// Compares two <see cref="T:TemplateFeatureKey" /> entities by Level and Taxonomy Id
        /// </summary>
        /// <param name="other">other key to compare to.</param>
        /// <returns>Zero (0) if this instancae has the same TemplateId and PageId.</returns>
        public int CompareTo(TemplateFeatureKey other)
        {
            var diff = this.TemplateId.CompareTo(other.TemplateId);
            if (diff == 0)
            {
                diff = this.FeatureKey.CompareTo(other.FeatureKey);
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
        /// <param name="templateId">Hierarhcial level of the key.</param>
        /// <param name="featureKey">The feature key.</param>
        /// <returns>a new TemplateFeatureKey entity</returns>
        public static TemplateFeatureKey Create(int templateId, string  featureKey)
        {
            return new TemplateFeatureKey(templateId, featureKey);
        }

        #region Operator Methods

        /// <summary>
        /// Greater than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than key2.</returns>
        public static bool operator >(TemplateFeatureKey key1, TemplateFeatureKey key2)
        {
            return key1.CompareTo(key2) > 0;
        }

        /// <summary>
        /// Less than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than key2.</returns>
        public static bool operator <(TemplateFeatureKey key1, TemplateFeatureKey key2)
        {
            return key1.CompareTo(key2) < 0;
        }

        /// <summary>
        /// Greater than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than or equal to key2.</returns>
        public static bool operator >=(TemplateFeatureKey key1, TemplateFeatureKey key2)
        {
            return key1.CompareTo(key2) >= 0;
        }

        /// <summary>
        /// Less than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than or equal to key2.</returns>
        public static bool operator <=(TemplateFeatureKey key1, TemplateFeatureKey key2)
        {
            return key1.CompareTo(key2) <= 0;
        }

        /// <summary>
        /// Equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is equal to key2.</returns>
        public static bool operator ==(TemplateFeatureKey key1, TemplateFeatureKey key2)
        {
            return key1.CompareTo(key2) == 0;
        }

        /// <summary>
        /// Not equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is not equal to key2.</returns>
        public static bool operator !=(TemplateFeatureKey key1, TemplateFeatureKey key2)
        {
            return key1.CompareTo(key2) != 0;
        }
        #endregion
        #endregion




       
    }
}
