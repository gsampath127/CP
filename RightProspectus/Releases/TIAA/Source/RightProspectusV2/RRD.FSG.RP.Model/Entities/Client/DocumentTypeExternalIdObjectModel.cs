// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************

using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class DocumentTypeExternalIdObjectModel.
    /// </summary>
    public class DocumentTypeExternalIdObjectModel : AuditedBaseModel<DocumentTypeExternalIdKey>, IComparable<DocumentTypeExternalIdObjectModel>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override DocumentTypeExternalIdKey Key
        {
            get { return new DocumentTypeExternalIdKey(this.DocumentTypeId, this.ExternalId); }
            internal set
            {
                if (value.DocumentTypeId != this.DocumentTypeId)
                {
                    throw new ArgumentOutOfRangeException("Key", 
                        string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}", 
                        value.DocumentTypeId, this.DocumentTypeId));
                }

                this.ExternalId = value.ExternalId;
            }
        }
        /// <summary>
        /// DocumentTypeName
        /// </summary>
        /// <value>The name of the document type.</value>
        public string DocumentTypeName { get; set; }
        /// <summary>
        /// DocumentTypeId
        /// </summary>
        /// <value>The document type identifier.</value>
        public int DocumentTypeId { get; set; }
        /// <summary>
        /// ExternalId
        /// </summary>
        /// <value>The external identifier.</value>
        public string ExternalId { get; set; }

        /// <summary>
        /// ModifiedDate
        /// </summary>
        /// <value>The modified date.</value>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// ModifiedBy
        /// </summary>
        /// <value>The modified by.</value>
        public int ModifiedBy { get; set; }

        /// <summary>
        /// ModifiedByName
        /// </summary>
        /// <value>The name of the modified by.</value>
        public string ModifiedByName { get; set; }

        /// <summary>
        /// Is Primary
        /// </summary>
        /// <value><c>true</c> if this instance is primary; otherwise, <c>false</c>.</value>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Compares the two DocumentTypeExternalIdObjectModel entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(DocumentTypeExternalIdObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }

    }
}
