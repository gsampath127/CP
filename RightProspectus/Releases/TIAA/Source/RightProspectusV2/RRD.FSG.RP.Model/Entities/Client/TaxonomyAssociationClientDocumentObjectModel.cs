using RRD.FSG.RP.Model.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.Client
{
   public class TaxonomyAssociationClientDocumentObjectModel : AuditedBaseModel<TaxonomyAssociationClientDocumentKey>, IComparable<TaxonomyAssociationClientDocumentObjectModel>

    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override TaxonomyAssociationClientDocumentKey Key
        {
            get { return new TaxonomyAssociationClientDocumentKey(this.TaxonomyId, this.ClientDocumentId); }
            internal set
            {
                if (value.TaxonomyId != this.TaxonomyId)
                {
                    throw new ArgumentOutOfRangeException("Key",
                        string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}",
                        value.TaxonomyId, this.TaxonomyId));
                }
                
                this.ClientDocumentId = value.ClientDocumentId;
                //this.ClientDocumentTypeId = value.ClientDocumentTypeId;
                
            }
        }
       
        /// <summary>
        /// TaxonomyId
        /// </summary>
        /// <value>The taxonomy identifier.</value>
        public int TaxonomyId { get; set; }
        /// <summary>
        /// TaxonomyName
        /// </summary>
        /// <value>The name of the taxonomy.</value>
        public string TaxonomyAssociationName { get; set; }
        /// <summary>
        /// ClientDocumentId
        /// </summary>
        /// <value>The level.</value>
        public int ClientDocumentId { get; set; }
        /// <summary>
        /// ClientDocumentName
        /// </summary>
        /// <value>The ClientDocumentName.</value>
        public string ClientDocumentName { get; set; }
        /// <summary>
        /// ClientDocumentId
        /// </summary>
        /// <value>The level.</value>
        public int ClientDocumentTypeId { get; set; }
        /// <summary>
        /// ClientDocumentName
        /// </summary>
        /// <value>The ClientDocumentType.</value>
        public string ClientDocumentTypeName { get; set; }
        /// <summary>
        /// ClientDocumentFileName
        /// </summary>
        /// <value>The FileName.</value>
        public string ClientDocumentFileName { get; set; }
        /// <summary>
        /// ClientDocumentFileName
        /// </summary>
        /// <value>The FileName.</value>
        public int Level { get; set; }
        /// <summary>
        /// Compares the two TaxonomyAssociationClientDocumentObjectModel entities by their TaxonomyAssociationClientDocumentKey identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(TaxonomyAssociationClientDocumentObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }

    }
}
