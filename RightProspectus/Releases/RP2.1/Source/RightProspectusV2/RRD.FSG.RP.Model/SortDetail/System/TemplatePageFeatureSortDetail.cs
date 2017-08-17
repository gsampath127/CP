// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.System;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.SortDetail.System
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    public class TemplatePageFeatureSortDetail : SortDetail<TemplatePageFeatureObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new TemplatePageFeatureSortColumn Column
        {
            get { return (TemplatePageFeatureSortColumn)base.Column; }
            set { base.Column = (SortColumn)value; }
        }


        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<TemplatePageFeatureObjectModel> Sort(IEnumerable<TemplatePageFeatureObjectModel> source)
        {
            switch (this.Column)
            {
                case TemplatePageFeatureSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case TemplatePageFeatureSortColumn.TemplateId:
                    return this.Sort(source, entity => entity.TemplateId);
                case TemplatePageFeatureSortColumn.PageId:
                    return this.Sort(source, entity => entity.PageId);
                case TemplatePageFeatureSortColumn.FeatureKey:
                    return this.Sort(source, entity => entity.FeatureKey);
                case TemplatePageFeatureSortColumn.FeatureDescription:
                    return this.Sort(source, entity => entity.FeatureDescription);

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
        public override int Compare(TemplatePageFeatureObjectModel x, TemplatePageFeatureObjectModel y)
        {
            switch (this.Column)
            {
                case TemplatePageFeatureSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case TemplatePageFeatureSortColumn.TemplateId:
                    return this.Compare(x.TemplateId, y.TemplateId);
                case TemplatePageFeatureSortColumn.FeatureKey:
                    return this.Compare(x.FeatureKey, y.FeatureKey);
                case TemplatePageFeatureSortColumn.FeatureDescription:
                    return this.Compare(x.FeatureDescription, y.FeatureDescription);

                default:
                    return base.Compare(x, y);
            }
        }
    }
}
