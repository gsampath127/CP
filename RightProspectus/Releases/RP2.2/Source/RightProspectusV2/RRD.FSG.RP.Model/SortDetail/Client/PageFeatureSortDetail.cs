// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Class PageFeatureSortDetail.
    /// </summary>
    public class PageFeatureSortDetail : AuditedSortDetail<PageFeatureObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new PageFeatureSortColumn Column
        {
            get { return (PageFeatureSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<PageFeatureObjectModel> Sort(IEnumerable<PageFeatureObjectModel> source)
        {
            switch (this.Column)
            {
                case PageFeatureSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case PageFeatureSortColumn.SiteId:
                    return this.Sort(source, entity => entity.SiteId);
                case PageFeatureSortColumn.PageName:
                    return this.Sort(source, entity => entity.PageName);
                case PageFeatureSortColumn.PageDescription:
                    return this.Sort(source, entity => entity.PageDescription);
                case PageFeatureSortColumn.PageKey:
                    return this.Sort(source, entity => entity.PageKey);
                case PageFeatureSortColumn.FeatureMode:
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
        public override int Compare(PageFeatureObjectModel x, PageFeatureObjectModel y)
        {
            switch (this.Column)
            {
                case PageFeatureSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case PageFeatureSortColumn.SiteId:
                    return this.Compare(x.SiteId, y.SiteId);
                case PageFeatureSortColumn.PageName:
                    return this.Compare(x.PageName, y.PageName);
                case PageFeatureSortColumn.PageDescription:
                    return this.Compare(x.PageDescription, y.PageDescription);
                case PageFeatureSortColumn.PageKey:
                    return this.Compare(x.PageKey, y.PageKey);
                case PageFeatureSortColumn.FeatureMode:
                    return this.Compare(x.FeatureMode, y.FeatureMode);
                default:
                    return base.Compare(x, y);
            }
        }

    }
}
