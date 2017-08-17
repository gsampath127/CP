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

namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    public class SiteSortDetail
        : AuditedSortDetail<SiteObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new SiteSortColumn Column
        {
            get { return (SiteSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<SiteObjectModel> Sort(IEnumerable<SiteObjectModel> source)
        {
            switch (this.Column)
            {
                case SiteSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case SiteSortColumn.SiteId:
                    return this.Sort(source, entity => entity.SiteID);
                case SiteSortColumn.TemplateName:
                    return this.Sort(source, entity => entity.TemplateName);
                case SiteSortColumn.DefaultPageName:
                    return this.Sort(source, entity => entity.DefaultPageName);
                case SiteSortColumn.PageDescription:
                    return this.Sort(source, entity => entity.PageDescription);
                case SiteSortColumn.ParentSiteId:
                    return this.Sort(source, entity => entity.ParentSiteId);
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
        public override int Compare(SiteObjectModel x, SiteObjectModel y)
        {
            switch (this.Column)
            {
                case SiteSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case SiteSortColumn.SiteId:
                    return this.Compare(x.SiteID, y.SiteID);
                case SiteSortColumn.TemplateName:
                    return this.Compare(x.TemplateName, y.TemplateName);
                case SiteSortColumn.DefaultPageName:
                    return this.Compare(x.DefaultPageName, y.DefaultPageName);
                case SiteSortColumn.PageDescription:
                    return this.Compare(x.PageDescription, y.PageDescription);
                case SiteSortColumn.ParentSiteId:
                    return this.Compare(x.ParentSiteId.Value, y.ParentSiteId.Value);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
