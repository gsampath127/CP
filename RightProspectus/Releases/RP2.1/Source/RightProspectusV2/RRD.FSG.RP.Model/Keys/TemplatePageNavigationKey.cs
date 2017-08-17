﻿// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 10-31-2015
// **********************************************************************
using System;

namespace RRD.FSG.RP.Model
{
    /// <summary>
    /// Class TemplatePageNavigationKey.
    /// </summary>
    public class TemplatePageNavigationKey : IEquatable<TemplatePageNavigationKey>, IComparable<TemplatePageNavigationKey>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TemplatePageKey" /> class.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <param name="navigationKey">The navigation key.</param>
        public TemplatePageNavigationKey(int templateId, int pageId, string navigationKey)
        {
            this.TemplateId = templateId;
            this.PageId = pageId;
            this.NavigationKey = navigationKey;
        }

        #region Public Properties

        /// <summary>
        /// Gets the TemplateId of the entity.
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateId { get; internal set; }

        /// <summary>
        /// Gets the page identifier.
        /// </summary>
        /// <value>The page identifier.</value>
        public int PageId { get; internal set; }

        /// <summary>
        /// Gets the navigation key.
        /// </summary>
        /// <value>The navigation key.</value>
        public string NavigationKey { get; internal set; }


        #endregion


        #region Public Methods

        #region Equals

        /// <summary>
        /// Compares two <see cref="SiteTextKey" /> entities for equality.
        /// </summary>
        /// <param name="other">The other entity to compare against.</param>
        /// <returns>True if the TemplateId ,PageId and ResourceKey are the same.</returns>
        public bool Equals(TemplatePageNavigationKey other)
        {
            return this.TemplateId == other.TemplateId
                && this.PageId == other.PageId
            && this.NavigationKey==other.NavigationKey;
        }

        /// <summary>
        /// Compares two <see cref="SiteTextKey" /> entities for equality.
        /// </summary>
        /// <param name="obj">The other entity to compare against.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as TemplatePageNavigationKey);
        }

        #endregion

        /// <summary>
        /// Generates a hash code for the <see cref="T:SiteTextKey" /> entity by combining the TemplateId and PageId properties.
        /// </summary>
        /// <returns>A computed hash code based on the TemplateId , PageId and ResourceKey properties.</returns>
        public override int GetHashCode()
        {
            return this.TemplateId ^ this.PageId; ;
        }

        #region CompareTo

        /// <summary>
        /// Compares two <see cref="T:TemplateNavigationKey" /> entities by Template Id and Navigation key
        /// </summary>
        /// <param name="other">other key to compare to.</param>
        /// <returns>Zero (0) if this instance has the same TemplateId and NavigationKey.</returns>
        public int CompareTo(TemplatePageNavigationKey other)
        {
            var diff = this.TemplateId.CompareTo(other.TemplateId);
            if (diff == 0)
            {
                diff = this.PageId.CompareTo(other.PageId);
            }
            if(diff==0)
            {
                diff = this.NavigationKey.CompareTo(other.NavigationKey);
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
        /// <param name="navigationKey">The navigation key.</param>
        /// <returns>a new TemplatePageKey entity</returns>
        public static TemplatePageNavigationKey Create(int templateId, int pageId,string navigationKey)
        {
            return new TemplatePageNavigationKey(templateId, pageId,navigationKey);
        }

        #region Operator Methods

        /// <summary>
        /// Greater than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than key2.</returns>
        public static bool operator >(TemplatePageNavigationKey key1, TemplatePageNavigationKey key2)
        {
            return key1.CompareTo(key2) > 0;
        }

        /// <summary>
        /// Less than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than key2.</returns>
        public static bool operator <(TemplatePageNavigationKey key1, TemplatePageNavigationKey key2)
        {
            return key1.CompareTo(key2) < 0;
        }

        /// <summary>
        /// Greater than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than or equal to key2.</returns>
        public static bool operator >=(TemplatePageNavigationKey key1, TemplatePageNavigationKey key2)
        {
            return key1.CompareTo(key2) >= 0;
        }

        /// <summary>
        /// Less than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than or equal to key2.</returns>
        public static bool operator <=(TemplatePageNavigationKey key1, TemplatePageNavigationKey key2)
        {
            return key1.CompareTo(key2) <= 0;
        }

        /// <summary>
        /// Equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is equal to key2.</returns>
        public static bool operator ==(TemplatePageNavigationKey key1, TemplatePageNavigationKey key2)
        {
            return key1.CompareTo(key2) == 0;
        }

        /// <summary>
        /// Not equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is not equal to key2.</returns>
        public static bool operator !=(TemplatePageNavigationKey key1, TemplatePageNavigationKey key2)
        {
            return key1.CompareTo(key2) != 0;
        }

        #endregion

        #endregion


    }
}