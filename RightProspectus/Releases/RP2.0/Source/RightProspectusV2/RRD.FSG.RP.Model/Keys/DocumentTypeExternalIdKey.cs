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
    /// Class DocumentTypeExternalIdKey.
    /// </summary>
    public class DocumentTypeExternalIdKey : IEquatable<DocumentTypeExternalIdKey>, IComparable<DocumentTypeExternalIdKey>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DocumentTypeExternalIdKey" /> class.
        /// </summary>
        /// <param name="DocumentTypeId">DocumentTypeId of the entity.</param>
        /// <param name="ExternalId">ExternalId of the entity.</param>
        public DocumentTypeExternalIdKey(int DocumentTypeId, string ExternalId)
        {
            this.DocumentTypeId = DocumentTypeId;
            this.ExternalId = ExternalId;
        }

        #region Public Properties

        /// <summary>
        /// Gets the DocumentTypeId of the entity.
        /// </summary>
        /// <value>The document type identifier.</value>
        public int DocumentTypeId { get; internal set; }

        /// <summary>
        /// Gets the ExternalId of the entity.
        /// </summary>
        /// <value>The external identifier.</value>
        public string ExternalId { get; internal set; }

        #endregion


        #region Public Methods

        #region Equals

        /// <summary>
        /// Compares two <see cref="DocumentTypeExternalIdKey" /> entities for equality.
        /// </summary>
        /// <param name="other">The other entity to compare against.</param>
        /// <returns>True if the Level and TaxonomyId are the same.</returns>
        public bool Equals(DocumentTypeExternalIdKey other)
        {
            return this.DocumentTypeId == other.DocumentTypeId
                && this.ExternalId == other.ExternalId;
        }

        /// <summary>
        /// Compares two <see cref="DocumentTypeExternalIdKey" /> entities for equality.
        /// </summary>
        /// <param name="obj">The other entity to compare against.</param>
        /// <returns>True if the Level and TaxonomyId are the same.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as DocumentTypeExternalIdKey);
        }

        #endregion

        /// <summary>
        /// Generates a hash code for the <see cref="T:DocumentTypeExternalIdKey" /> entity by combining the Level and TaxonomyId properties.
        /// </summary>
        /// <returns>A computed hash code based on th Level and TaxonomyId properties.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = hash * 16777619 ^ this.DocumentTypeId;
                hash = hash * this.ExternalId.GetHashCode();
                return hash;
            }
            
        }

        #region CompareTo

        /// <summary>
        /// Compares two <see cref="T:DocumentTypeExternalIdKey" /> entities by Level and Taxonomy Id
        /// </summary>
        /// <param name="other">other key to compare to.</param>
        /// <returns>A negative value if this instance has a lower level or same level and lover taxonomy id
        /// A positive value if this instance has a higher level or same level and higher taxonomy id.
        /// Zero (0) if this instance has the same level and taxonomy id.</returns>
        public int CompareTo(DocumentTypeExternalIdKey other)
        {
            var diff = this.DocumentTypeId.CompareTo(other.DocumentTypeId);
            if (diff == 0)
            {
                diff = this.ExternalId.CompareTo(other.ExternalId);
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
        /// Creates a new instance of DocumentTypeExternalIdKey with the values provided.
        /// </summary>
        /// <param name="DocumentTypeId">DocumentTypeId</param>
        /// <param name="ExternalId">ExternalId key.</param>
        /// <returns>a new DocumentTypeExternalIdKey entity</returns>
        public static DocumentTypeExternalIdKey Create(int DocumentTypeId, string ExternalId)
        {
            return new DocumentTypeExternalIdKey(DocumentTypeId, ExternalId);
        }

        #region Operator Methods

        /// <summary>
        /// Greater than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than key2.</returns>
        public static bool operator >(DocumentTypeExternalIdKey key1, DocumentTypeExternalIdKey key2)
        {
            return key1.CompareTo(key2) > 0;
        }

        /// <summary>
        /// Less than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than key2.</returns>
        public static bool operator <(DocumentTypeExternalIdKey key1, DocumentTypeExternalIdKey key2)
        {
            return key1.CompareTo(key2) < 0;
        }

        /// <summary>
        /// Greater than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than or equal to key2.</returns>
        public static bool operator >=(DocumentTypeExternalIdKey key1, DocumentTypeExternalIdKey key2)
        {
            return key1.CompareTo(key2) >= 0;
        }

        /// <summary>
        /// Less than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than or equal to key2.</returns>
        public static bool operator <=(DocumentTypeExternalIdKey key1, DocumentTypeExternalIdKey key2)
        {
            return key1.CompareTo(key2) <= 0;
        }

        /// <summary>
        /// Equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is equal to key2.</returns>
        public static bool operator ==(DocumentTypeExternalIdKey key1, DocumentTypeExternalIdKey key2)
        {
            return key1.CompareTo(key2) == 0;
        }

        /// <summary>
        /// Not equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is not equal to key2.</returns>
        public static bool operator !=(DocumentTypeExternalIdKey key1, DocumentTypeExternalIdKey key2)
        {
            return key1.CompareTo(key2) != 0;
        }

        #endregion

        #endregion


    }
}
