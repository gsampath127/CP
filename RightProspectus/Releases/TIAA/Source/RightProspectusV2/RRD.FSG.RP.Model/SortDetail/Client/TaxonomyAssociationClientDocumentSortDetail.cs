using RRD.FSG.RP.Model.Entities.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.SortDetail.Client
{

    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    public class TaxonomyAssociationClientDocumentSortDetail
        : AuditedSortDetail<TaxonomyAssociationClientDocumentObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new TaxonomyAssociationClientDocumentSortColumn Column
        {
            get { return (TaxonomyAssociationClientDocumentSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<TaxonomyAssociationClientDocumentObjectModel> Sort(IEnumerable<TaxonomyAssociationClientDocumentObjectModel> source)
        {
            switch (this.Column)
            {
                case TaxonomyAssociationClientDocumentSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case TaxonomyAssociationClientDocumentSortColumn.TaxonomyAssociationName:
                    return this.Sort(source, entity => entity.TaxonomyAssociationName);
                case TaxonomyAssociationClientDocumentSortColumn.ClientDocumentTypeName:
                    return this.Sort(source, entity => entity.ClientDocumentTypeName);
                case TaxonomyAssociationClientDocumentSortColumn.ClientDocumentName:
                    return this.Sort(source, entity => entity.ClientDocumentName);
                case TaxonomyAssociationClientDocumentSortColumn.ClientDocumentFileName:
                    return this.Sort(source, entity => entity.ClientDocumentFileName);

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
        public override int Compare(TaxonomyAssociationClientDocumentObjectModel x, TaxonomyAssociationClientDocumentObjectModel y)
        {
            switch (this.Column)
            {
                case TaxonomyAssociationClientDocumentSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case TaxonomyAssociationClientDocumentSortColumn.TaxonomyAssociationName:
                    return this.Compare(x.TaxonomyAssociationName, y.TaxonomyAssociationName);
                case TaxonomyAssociationClientDocumentSortColumn.ClientDocumentTypeName:
                    return this.Compare(x.ClientDocumentTypeName, y.ClientDocumentTypeName);
                case TaxonomyAssociationClientDocumentSortColumn.ClientDocumentName:
                    return this.Compare(x.ClientDocumentName, y.ClientDocumentName);
                case TaxonomyAssociationClientDocumentSortColumn.ClientDocumentFileName:
                    return this.Compare(x.ClientDocumentFileName, y.ClientDocumentFileName);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
