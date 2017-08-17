// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-16-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    public class TaxonomyLevelExternalIdSortDetail
        : AuditedSortDetail<TaxonomyLevelExternalIdObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new TaxonomyLevelExternalIdSortColumn Column
        {
            get { return (TaxonomyLevelExternalIdSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<TaxonomyLevelExternalIdObjectModel> Sort(IEnumerable<TaxonomyLevelExternalIdObjectModel> source)
        {
            switch (this.Column)
            {
                case TaxonomyLevelExternalIdSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case TaxonomyLevelExternalIdSortColumn.Level:
                    return this.Sort(source, entity => entity.Level);
                case TaxonomyLevelExternalIdSortColumn.TaxonomyName:
                    return this.Sort(source, entity => entity.TaxonomyName);
                case TaxonomyLevelExternalIdSortColumn.ExternalId:
                    return this.Sort(source, entity => entity.ExternalId);
                case TaxonomyLevelExternalIdSortColumn.IsPrimary:
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
        public override int Compare(TaxonomyLevelExternalIdObjectModel x, TaxonomyLevelExternalIdObjectModel y)
        {
            switch (this.Column)
            {
                case TaxonomyLevelExternalIdSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case TaxonomyLevelExternalIdSortColumn.Level:
                    return this.Compare(x.Level, y.Level);
                case TaxonomyLevelExternalIdSortColumn.TaxonomyName:
                    return this.Compare(x.TaxonomyName, y.TaxonomyName);
                case TaxonomyLevelExternalIdSortColumn.ExternalId:
                    return this.Compare(x.ExternalId, y.ExternalId);
                case TaxonomyLevelExternalIdSortColumn.IsPrimary:
                    return this.Compare(x.IsPrimary, y.IsPrimary);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
