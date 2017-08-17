using RRD.FSG.RP.Model.Entities.Client;
using System.Collections.Generic;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    public class DocumentTypeExternalIdSortDetail
        : AuditedSortDetail<DocumentTypeExternalIdObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new DocumentTypeExternalIdSortColumn Column
        {
            get { return (DocumentTypeExternalIdSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<DocumentTypeExternalIdObjectModel> Sort(IEnumerable<DocumentTypeExternalIdObjectModel> source)
        {
            switch (this.Column)
            {
                case DocumentTypeExternalIdSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case DocumentTypeExternalIdSortColumn.DocumentTypeId:
                    return this.Sort(source, entity => entity.DocumentTypeId);
                case DocumentTypeExternalIdSortColumn.DocumentTypeName:
                    return this.Sort(source, entity => entity.DocumentTypeName);
                case DocumentTypeExternalIdSortColumn.ExternalId:
                    return this.Sort(source, entity => entity.ExternalId);

                case DocumentTypeExternalIdSortColumn.ModifiedByName:
                    return this.Sort(source, entity => entity.ModifiedByName);
                case DocumentTypeExternalIdSortColumn.IsPrimary:
                    return this.Sort(source, entity => entity.IsPrimary);
                default:
                    return base.Sort(source);
            }
        }

        /// <summary>
        /// Compares two entities using teh sort properties of this instance.
        /// </summary>
        /// <param name="x">First entity to compare.</param>
        /// <param name="y">Second entity to compare.</param>
        /// <returns>A negative number if x is less than y, a positive number if x is greater than y, and 0 if they are the same.</returns>
        public override int Compare(DocumentTypeExternalIdObjectModel x, DocumentTypeExternalIdObjectModel y)
        {
            switch (this.Column)
            {
                case DocumentTypeExternalIdSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case DocumentTypeExternalIdSortColumn.DocumentTypeId:
                    return this.Compare(x.DocumentTypeId, y.DocumentTypeId);
                case DocumentTypeExternalIdSortColumn.DocumentTypeName:
                    return this.Compare(x.DocumentTypeName, y.DocumentTypeName);
                case DocumentTypeExternalIdSortColumn.ExternalId:
                    return this.Compare(x.ExternalId, y.ExternalId);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
