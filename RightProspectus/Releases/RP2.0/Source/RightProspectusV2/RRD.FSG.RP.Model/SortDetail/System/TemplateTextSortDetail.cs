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
    public class TemplateTextSortDetail
        : SortDetail<TemplateTextObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new TemplateTextSortColumn Column
        {
            get { return (TemplateTextSortColumn)base.Column; }
            set { base.Column = (SortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<TemplateTextObjectModel> Sort(IEnumerable<TemplateTextObjectModel> source)
        {
            switch (this.Column)
            {
                case TemplateTextSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case TemplateTextSortColumn.TemplateId:
                    return this.Sort(source, entity => entity.TemplateID);
                case TemplateTextSortColumn.ResourceKey:
                    return this.Sort(source, entity => entity.ResourceKey);
                case TemplateTextSortColumn.IsHtml:
                    return this.Sort(source, entity => entity.IsHTML);
                case TemplateTextSortColumn.DefaultText:
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
        public override int Compare(TemplateTextObjectModel x, TemplateTextObjectModel y)
        {
            switch (this.Column)
            {
                case TemplateTextSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case TemplateTextSortColumn.TemplateId:
                    return this.Compare(x.TemplateID, y.TemplateID);
                case TemplateTextSortColumn.ResourceKey:
                    return this.Compare(x.ResourceKey, y.ResourceKey);
                case TemplateTextSortColumn.IsHtml:
                    return this.Compare(x.IsHTML, y.IsHTML);
                case TemplateTextSortColumn.DefaultText:
                    return this.Compare(x.DefaultText, y.DefaultText);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
