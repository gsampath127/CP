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
    public class TemplateFeatureSortDetail : SortDetail<TemplateFeatureObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new TemplateFeatureSortColumn Column
        {
            get { return (TemplateFeatureSortColumn)base.Column; }
            set { base.Column = (SortColumn)value; }
        }


        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<TemplateFeatureObjectModel> Sort(IEnumerable<TemplateFeatureObjectModel> source)
        {
            switch (this.Column)
            {
                case TemplateFeatureSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case TemplateFeatureSortColumn.TemplateId:
                    return this.Sort(source, entity => entity.TemplateId);
                case TemplateFeatureSortColumn.FeatureKey:
                    return this.Sort(source, entity => entity.FeatureKey);
                case TemplateFeatureSortColumn.FeatureDescription:
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
        public override int Compare(TemplateFeatureObjectModel x, TemplateFeatureObjectModel y)
        {
            switch (this.Column)
            {
                case TemplateFeatureSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case TemplateFeatureSortColumn.TemplateId:
                    return this.Compare(x.TemplateId, y.TemplateId);
                case TemplateFeatureSortColumn.FeatureKey:
                    return this.Compare(x.FeatureKey, y.FeatureKey);
                case TemplateFeatureSortColumn.FeatureDescription:
                    return this.Compare(x.FeatureDescription, y.FeatureDescription);
               
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
