// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    public class TaxonomySortDetail
        : SortDetail<TaxonomyObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new TaxonomySortColumn Column
        {
            get { return (TaxonomySortColumn)base.Column; }
            set { base.Column = (SortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<TaxonomyObjectModel> Sort(IEnumerable<TaxonomyObjectModel> source)
        {
            switch (this.Column)
            {
                case TaxonomySortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case TaxonomySortColumn.TaxonomyId:
                    return this.Sort(source, entity => entity.TaxonomyId);
                case TaxonomySortColumn.TaxonomyName:
                    return this.Sort(source, entity => entity.TaxonomyName);
                case TaxonomySortColumn.Level:
                    return this.Sort(source, entity => entity.Level);
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
        public override int Compare(TaxonomyObjectModel x, TaxonomyObjectModel y)
        {
            switch (this.Column)
            {
                case TaxonomySortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case TaxonomySortColumn.TaxonomyId:
                    return this.Compare(x.TaxonomyId, y.TaxonomyId);
                case TaxonomySortColumn.TaxonomyName:
                    return this.Compare(x.TaxonomyName, y.TaxonomyName);
                case TaxonomySortColumn.Level:
                    return this.Compare(x.Level, y.Level);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
