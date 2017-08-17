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
    public class SiteTextSortDetail
        : AuditedSortDetail<SiteTextObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new SiteTextSortColumn Column
        {
            get { return (SiteTextSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<SiteTextObjectModel> Sort(IEnumerable<SiteTextObjectModel> source)
        {
            switch (this.Column)
            {
                case SiteTextSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case SiteTextSortColumn.SiteID:
                    return this.Sort(source, entity => entity.SiteID);
                case SiteTextSortColumn.SiteName:
                    return this.Sort(source, entity => entity.SiteName);
                case SiteTextSortColumn.ResourceKey:
                    return this.Sort(source, entity => entity.ResourceKey);
                case SiteTextSortColumn.Text:
                    return this.Sort(source, entity => entity.Text);
                case SiteTextSortColumn.IsProofing:
                    return this.Sort(source, entity => entity.IsProofing);
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
        public override int Compare(SiteTextObjectModel x, SiteTextObjectModel y)
        {
            switch (this.Column)
            {
                case SiteTextSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case SiteTextSortColumn.SiteID:
                    return this.Compare(x.SiteID, y.SiteID);
                case SiteTextSortColumn.SiteName:
                    return this.Compare(x.SiteName, y.SiteName);
                case SiteTextSortColumn.ResourceKey:
                    return this.Compare(x.ResourceKey, y.ResourceKey);
                case SiteTextSortColumn.Text:
                    return this.Compare(x.Text, y.Text);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
