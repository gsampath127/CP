// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.System;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.SortDetail.System
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    public class TemplatePageSortDetail
        : SortDetail<TemplatePageObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new TemplatePageSortColumn Column
        {
            get { return (TemplatePageSortColumn)base.Column; }
            set { base.Column = (SortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<TemplatePageObjectModel> Sort(IEnumerable<TemplatePageObjectModel> source)
        {
            switch (this.Column)
            {
                case TemplatePageSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case TemplatePageSortColumn.TemplateID:
                    return this.Sort(source, entity => entity.TemplateID);
                case TemplatePageSortColumn.PageId:
                    return this.Sort(source, entity => entity.PageID);
                case TemplatePageSortColumn.TemplateName:
                    return this.Sort(source, entity => entity.TemplateName);
                case TemplatePageSortColumn.PageName:
                    return this.Sort(source, entity => entity.PageName);
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
        public override int Compare(TemplatePageObjectModel x, TemplatePageObjectModel y)
        {
            switch (this.Column)
            {
                case TemplatePageSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case TemplatePageSortColumn.TemplateID:
                    return this.Compare(x.TemplateID, y.TemplateID);
                case TemplatePageSortColumn.PageId:
                    return this.Compare(x.PageID, y.PageID);
                case TemplatePageSortColumn.TemplateName:
                    return this.Compare(x.TemplateName, y.TemplateName);
                case TemplatePageSortColumn.PageName:
                    return this.Compare(x.PageName, y.PageName);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
