﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Keys
{
    public class TaxonomyAssociationClientDocumentKey : IEquatable<TaxonomyAssociationClientDocumentKey>, IComparable<TaxonomyAssociationClientDocumentKey>, IComparable
    {

         /// <summary>
        /// Initializes a new instance of the <see cref="T:TaxonomyAssociationClientDocumentKey" /> class.
        /// </summary>
        /// <param name="Level">The level.</param>
        /// <param name="TaxonomyId">TaxonomyId of the entity.</param>
        /// <param name="ClientDocumentId">ExternalId of the entity.</param>
        public TaxonomyAssociationClientDocumentKey(int TaxonomyId, int ClientDocumentId)
        {

            this.TaxonomyId = TaxonomyId;
            this.ClientDocumentId = ClientDocumentId;
        }

        #region Public Properties

        /// <summary>
        /// Gets the TaxonomyId of the entity.
        /// </summary>
        /// <value>The taxonomy identifier.</value>
        public int TaxonomyId { get; internal set; }

       
        /// <summary>
        /// Gets the ClientDocumentId of the entity.
        /// </summary>
        /// <value>The ClientDocument identifier.</value>
        public int ClientDocumentId { get; internal set; }

        #endregion


        #region Public Methods

        #region Equals

        /// <summary>
        /// Compares two <see cref="TaxonomyAssociationClientDocumentKey" /> entities for equality.
        /// </summary>
        /// <param name="other">The other entity to compare against.</param>
        /// <returns>True if the Level and TaxonomyId are the same.</returns>
        public bool Equals(TaxonomyAssociationClientDocumentKey other)
        {
            return this.TaxonomyId == other.TaxonomyId
                && this.ClientDocumentId == other.ClientDocumentId;
        }

        /// <summary>
        /// Compares two <see cref="TaxonomyAssociationClientDocumentKey" /> entities for equality.
        /// </summary>
        /// <param name="obj">The other entity to compare against.</param>
        /// <returns>True if the Level and TaxonomyId are the same.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as TaxonomyAssociationClientDocumentKey);
        }

        #endregion

        /// <summary>
        /// Generates a hash code for the <see cref="T:TaxonomyAssociationClientDocumentKey" /> entity by combining the Level and TaxonomyId properties.
        /// </summary>
        /// <returns>A computed hash code based on th Level and TaxonomyId properties.</returns>
        public override int GetHashCode()
        {
            return this.TaxonomyId ^ this.ClientDocumentId;
        }

        #region CompareTo

        /// <summary>
        /// Compares two <see cref="T:TaxonomyAssociationClientDocumentKey" /> entities by Level and Taxonomy Id
        /// </summary>
        /// <param name="other">other key to compare to.</param>
        /// <returns>A negative value if this instance has a lower level or same level and lover taxonomy id
        /// A positive value if this instance has a higher level or same level and higher taxonomy id.
        /// Zero (0) if this instance has the same level and taxonomy id.</returns>
        public int CompareTo(TaxonomyAssociationClientDocumentKey other)
        {
            var diff = this.TaxonomyId.CompareTo(other.TaxonomyId);
            if (diff == 0)
            {
                diff = this.ClientDocumentId.CompareTo(other.ClientDocumentId);
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
        /// Creates a new instance of TaxonomyAssociationClientDocumentKey with the values provided.
        /// </summary>
        /// <param name="sitetextID">The taxonomyAssociation identifier.</param>
        /// <param name="version">The clientDocument identifier.</param>
        /// <returns>a new TaxonomyAssociationClientDocumentKey entity</returns>
        public static TaxonomyAssociationClientDocumentKey Create(int taxonomyAssociationId, int clientDocumentId)
        {
            return new TaxonomyAssociationClientDocumentKey(taxonomyAssociationId, clientDocumentId);
        }

        #region Operator Methods

        /// <summary>
        /// Greater than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than key2.</returns>
        public static bool operator >(TaxonomyAssociationClientDocumentKey key1, TaxonomyAssociationClientDocumentKey key2)
        {
            return key1.CompareTo(key2) > 0;
        }

        /// <summary>
        /// Less than comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than key2.</returns>
        public static bool operator <(TaxonomyAssociationClientDocumentKey key1, TaxonomyAssociationClientDocumentKey key2)
        {
            return key1.CompareTo(key2) < 0;
        }

        /// <summary>
        /// Greater than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is greater than or equal to key2.</returns>
        public static bool operator >=(TaxonomyAssociationClientDocumentKey key1, TaxonomyAssociationClientDocumentKey key2)
        {
            return key1.CompareTo(key2) >= 0;
        }

        /// <summary>
        /// Less than or equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is less than or equal to key2.</returns>
        public static bool operator <=(TaxonomyAssociationClientDocumentKey key1, TaxonomyAssociationClientDocumentKey key2)
        {
            return key1.CompareTo(key2) <= 0;
        }

        /// <summary>
        /// Equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is equal to key2.</returns>
        public static bool operator ==(TaxonomyAssociationClientDocumentKey key1, TaxonomyAssociationClientDocumentKey key2)
        {
            return key1.CompareTo(key2) == 0;
        }

        /// <summary>
        /// Not equal comparison between two items.
        /// </summary>
        /// <param name="key1">Key instance on the left side of the comparison.</param>
        /// <param name="key2">Key instance on the right side of the comparison.</param>
        /// <returns>True if key1 is not equal to key2.</returns>
        public static bool operator !=(TaxonomyAssociationClientDocumentKey key1, TaxonomyAssociationClientDocumentKey key2)
        {
            return key1.CompareTo(key2) != 0;
        }

        #endregion

        #endregion

    }
}
