// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    public class UrlRewriteSortDetail
        : AuditedSortDetail<UrlRewriteObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new UrlRewriteSortColumn Column
        {
            get { return (UrlRewriteSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<UrlRewriteObjectModel> Sort(IEnumerable<UrlRewriteObjectModel> source)
        {
            switch (this.Column)
            {
                case UrlRewriteSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case UrlRewriteSortColumn.MatchPattern:
                    return this.Sort(source, entity => entity.MatchPattern);
                case UrlRewriteSortColumn.PatternName:
                    return this.Sort(source, entity => entity.PatternName);
                case UrlRewriteSortColumn.RewriteFormat:
                    return this.Sort(source, entity => entity.RewriteFormat);
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
        public override int Compare(UrlRewriteObjectModel x, UrlRewriteObjectModel y)
        {
            switch (this.Column)
            {
                case UrlRewriteSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case UrlRewriteSortColumn.MatchPattern:
                    return this.Compare(x.MatchPattern, y.MatchPattern);
                case UrlRewriteSortColumn.PatternName:
                    return this.Compare(x.PatternName, y.PatternName);
                case UrlRewriteSortColumn.RewriteFormat:
                    return this.Compare(x.RewriteFormat, y.RewriteFormat);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
