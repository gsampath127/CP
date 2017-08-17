// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Class PageNavigationSortDetail.
    /// </summary>
    public class PageNavigationSortDetail : AuditedSortDetail<PageNavigationObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new PageNavigationSortColumn Column
        {
            get { return (PageNavigationSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<PageNavigationObjectModel> Sort(IEnumerable<PageNavigationObjectModel> source)
        {
            switch (this.Column)
            {
                case PageNavigationSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case PageNavigationSortColumn.NavigationKey:
                    return this.Sort(source, entity => entity.NavigationKey);
                case PageNavigationSortColumn.IsProofing:
                    return this.Sort(source, entity => entity.IsProofing);
                case PageNavigationSortColumn.PageName:
                    return this.Sort(source, entity => entity.PageName);
                case PageNavigationSortColumn.PageDescription:
                    return this.Sort(source, entity => entity.PageDescription);
                case PageNavigationSortColumn.ModifiedBy:
                    return this.Sort(source, entity => entity.ModifiedBy);

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
        public override int Compare(PageNavigationObjectModel x, PageNavigationObjectModel y)
        {
            switch (this.Column)
            {
                case PageNavigationSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case PageNavigationSortColumn.NavigationKey:
                    return this.Compare(x.NavigationKey, y.NavigationKey);
                case PageNavigationSortColumn.IsProofing:
                    return this.Compare(x.IsProofing, y.IsProofing);
                case PageNavigationSortColumn.PageName:
                    return this.Compare(x.PageName, y.PageName);
                case PageNavigationSortColumn.PageDescription:
                    return this.Compare(x.PageDescription, y.PageDescription);
                case PageNavigationSortColumn.ModifiedBy:
                    return this.Compare(x.ModifiedBy, y.ModifiedBy);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
