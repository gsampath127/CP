// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************
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
    public class PageTextSortDetail
        : AuditedSortDetail<PageTextObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new PageTextSortColumn Column
        {
            get { return (PageTextSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<PageTextObjectModel> Sort(IEnumerable<PageTextObjectModel> source)
        {
            switch (this.Column)
            {
                case PageTextSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case PageTextSortColumn.SiteID:
                    return this.Sort(source, entity => entity.SiteID);
                case PageTextSortColumn.PageName:
                    return this.Sort(source, entity => entity.PageName);
                case PageTextSortColumn.PageDescription:
                    return this.Sort(source, entity => entity.PageDescription);
                case PageTextSortColumn.SiteName:
                    return this.Sort(source, entity => entity.SiteName);
                case PageTextSortColumn.ResourceKey:
                    return this.Sort(source, entity => entity.ResourceKey);
                case PageTextSortColumn.Text:
                    return this.Sort(source, entity => entity.Text);
                case PageTextSortColumn.IsProofing:
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
        public override int Compare(PageTextObjectModel x, PageTextObjectModel y)
        {
            switch (this.Column)
            {
                case PageTextSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case PageTextSortColumn.SiteID:
                    return this.Compare(x.SiteID, y.SiteID);
                case PageTextSortColumn.PageName:
                    return this.Compare(x.PageName, y.PageName);
                case PageTextSortColumn.PageDescription:
                    return this.Compare(x.PageDescription, y.PageDescription);
                case PageTextSortColumn.SiteName:
                    return this.Compare(x.SiteName, y.SiteName);
                case PageTextSortColumn.ResourceKey:
                    return this.Compare(x.ResourceKey, y.ResourceKey);
                case PageTextSortColumn.Text:
                    return this.Compare(x.Text, y.Text);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
