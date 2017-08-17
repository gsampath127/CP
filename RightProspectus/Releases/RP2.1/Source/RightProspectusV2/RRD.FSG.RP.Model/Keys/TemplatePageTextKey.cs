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
    /// Class TemplatePageTextKey.
    /// </summary>
    public class TemplatePageTextKey : IEquatable<TemplatePageTextKey>, IComparable<TemplatePageTextKey>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TemplatePageKey" /> class.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <param name="resourceKey">The resource key.</param>
        public TemplatePageTextKey(int templateId, int pageId,string resourceKey)
        {
            this.TemplateId = templateId;
            this.PageId = pageId;
            this.ResourceKey = resourceKey;
        }

        #region Public Properties

        /// <summary>
        /// Gets the TemplateId of the entity.
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateId { get; internal set; }

        /// <summary>
        /// Gets the PageId identifier of the entity.
        /// </summary>
        /// <value>The page identifier.</value>
        public int PageId { get; internal set; }

        /// <summary>
        /// Gets the resource key.
        /// </summary>
        /// <value>The resource key.</value>
        public string ResourceKey { get; internal set; }

        #endregion


        #region Public Methods

        #region Equals

        /// <summary>
        /// Compares two <see cref="SiteTextKey" /> entities for equality.
        /// </summary>
        /// <param name="other">The other entity to compare against.</param>
        /// <returns>True if the TemplateId ,PageId and ResourceKey are the same.</returns>
        public bool Equals(TemplatePageTextKey other)
        {
            return this.TemplateId == other.TemplateId
                && this.PageId == other.PageId
                && this.ResourceKey == other.ResourceKey ;
        }

        /// <summary>
        /// Compares two <see cref="SiteTextKey" /> entities for equality.
        /// </summary>
        /// <param name="obj">The other entity to compare against.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as TemplatePageTextKey);
        }

        #endregion

        /// <summary>
        /// Generates a hash code for the <see cref="T:SiteTextKey" /> entity by combining the TemplateId and PageId properties.
        /// </summary>
        /// <returns>A computed hash code based on the TemplateId , PageId and ResourceKey properties.</returns>
        public override int GetHashCode()
        {
            return this.TemplateId ^ this.PageId;
        }

        #region CompareTo

        /// <summary>
        /// Compares two <see cref="T:TemplatePageKey" /> entities by Level and Taxonomy Id
        /// </summary>
        /// <param name="other">other key to compare to.</param>
        /// <returns>Zero (0) if this instancae has the same TemplateId and PageId.</returns>
        public int CompareTo(TemplatePageTextKey other)
        {
            var diff = this.TemplateId.CompareTo(other.TemplateId);
            if (diff == 0)
            {
                diff = this.PageId.CompareTo(other.PageId);
            }

            if (diff == 0)
            {
                diff = this.ResourceKey.CompareTo(other.ResourceKey);
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
        /// <param name="templateId">The template identifier.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <param name="resourceKey">The resource key.</param>
        /// <returns>a new TemplatePageKey entity</returns>
        public static TemplatePageTextKey Create(int templateId, int pageId,string resourceKey)
        {
            return new TemplatePageTextKey(templateId, pageId,resourceKey);
        }

        #region Operator Methods

        /// <summary>
        /// Greater than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than key2.</returns>
        public static bool operator >(TemplatePageTextKey key1, TemplatePageTextKey key2)
        {
            return key1.CompareTo(key2) > 0;
        }

        /// <summary>
        /// Less than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than key2.</returns>
        public static bool operator <(TemplatePageTextKey key1, TemplatePageTextKey key2)
        {
            return key1.CompareTo(key2) < 0;
        }

        /// <summary>
        /// Greater than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than or equal to key2.</returns>
        public static bool operator >=(TemplatePageTextKey key1, TemplatePageTextKey key2)
        {
            return key1.CompareTo(key2) >= 0;
        }

        /// <summary>
        /// Less than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than or equal to key2.</returns>
        public static bool operator <=(TemplatePageTextKey key1, TemplatePageTextKey key2)
        {
            return key1.CompareTo(key2) <= 0;
        }

        /// <summary>
        /// Equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is equal to key2.</returns>
        public static bool operator ==(TemplatePageTextKey key1, TemplatePageTextKey key2)
        {
            return key1.CompareTo(key2) == 0;
        }

        /// <summary>
        /// Not equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is not equal to key2.</returns>
        public static bool operator !=(TemplatePageTextKey key1, TemplatePageTextKey key2)
        {
            return key1.CompareTo(key2) != 0;
        }

        #endregion

        #endregion


    }
}
