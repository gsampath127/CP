// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Class SiteFeatureSortDetail.
    /// </summary>
    public class SiteFeatureSortDetail :   AuditedSortDetail<SiteFeatureObjectModel>
    {

        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new SiteFeatureSortColumn Column
        {
            get { return (SiteFeatureSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<SiteFeatureObjectModel> Sort(IEnumerable<SiteFeatureObjectModel> source)
        {
            switch (this.Column)
            {
                case SiteFeatureSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case SiteFeatureSortColumn.SiteId:
                    return this.Sort(source, entity => entity.SiteId);
                case SiteFeatureSortColumn.SiteKey:
                    return this.Sort(source, entity => entity.SiteKey);
                case SiteFeatureSortColumn.FeatureMode:
                    return this.Sort(source, entity => entity.FeatureMode);
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
        public override int Compare(SiteFeatureObjectModel x, SiteFeatureObjectModel y)
        {
            switch (this.Column)
            {
                case SiteFeatureSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case SiteFeatureSortColumn.SiteId:
                    return this.Compare(x.SiteId, y.SiteId);
                case SiteFeatureSortColumn.SiteKey:
                    return this.Compare(x.SiteKey, y.SiteKey);
                case SiteFeatureSortColumn.FeatureMode:
                    return this.Compare(x.FeatureMode, y.FeatureMode);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
