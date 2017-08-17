// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
// <copyright file="TemplatePageTextSortDetail.cs" company="RR Donnelley">
using RRD.FSG.RP.Model.Entities.System;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.SortDetail.System
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    public class TemplatePageTextSortDetail
        : SortDetail<TemplatePageTextObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new TemplatePageTextSortColumn Column
        {
            get { return (TemplatePageTextSortColumn)base.Column; }
            set { base.Column = (SortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<TemplatePageTextObjectModel> Sort(IEnumerable<TemplatePageTextObjectModel> source)
        {
            switch (this.Column)
            {
                case TemplatePageTextSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case TemplatePageTextSortColumn.TemplateId:
                    return this.Sort(source, entity => entity.TemplateID);
                case TemplatePageTextSortColumn.PageId:
                    return this.Sort(source, entity => entity.PageID);
                case TemplatePageTextSortColumn.ResourceKey:
                    return this.Sort(source, entity => entity.ResourceKey);
                case TemplatePageTextSortColumn.IsHtml:
                    return this.Sort(source, entity => entity.IsHTML);
                case TemplatePageTextSortColumn.DefaultText:
                    return this.Sort(source, entity => entity.DefaultText);
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
        public override int Compare(TemplatePageTextObjectModel x, TemplatePageTextObjectModel y)
        {
            switch (this.Column)
            {
                case TemplatePageTextSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case TemplatePageTextSortColumn.TemplateId:
                    return this.Compare(x.TemplateID, y.TemplateID);
                case TemplatePageTextSortColumn.PageId:
                    return this.Compare(x.PageID, y.PageID);
                case TemplatePageTextSortColumn.ResourceKey:
                    return this.Compare(x.ResourceKey, y.ResourceKey);
                case TemplatePageTextSortColumn.IsHtml:
                    return this.Compare(x.IsHTML, y.IsHTML);
                case TemplatePageTextSortColumn.DefaultText:
                    return this.Compare(x.DefaultText, y.DefaultText);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
