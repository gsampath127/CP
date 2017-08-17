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
    /// Class SiteNavigationSortDetail.
    /// </summary>
    public class SiteNavigationSortDetail
          : AuditedSortDetail<SiteNavigationObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new SiteNavigationSortColumn Column
        {
            get { return (SiteNavigationSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<SiteNavigationObjectModel> Sort(IEnumerable<SiteNavigationObjectModel> source)
        {
            switch (this.Column)
            {
                case SiteNavigationSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case SiteNavigationSortColumn.NavigationKey:
                    return this.Sort(source, entity => entity.NavigationKey);
                case SiteNavigationSortColumn.PageName:
                    return this.Sort(source, entity => entity.PageName);
                case SiteNavigationSortColumn.PageDescription:
                    return this.Sort(source, entity => entity.PageDescription);
                case SiteNavigationSortColumn.IsProofing:
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
        public override int Compare(SiteNavigationObjectModel x, SiteNavigationObjectModel y)
        {
            switch (this.Column)
            {
                case SiteNavigationSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case SiteNavigationSortColumn.NavigationKey:
                    return this.Compare(x.NavigationKey, y.NavigationKey);
                case SiteNavigationSortColumn.PageName:
                    return this.Compare(x.PageName, y.PageName);
                case SiteNavigationSortColumn.PageDescription:
                    return this.Compare(x.PageDescription, y.PageDescription);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
