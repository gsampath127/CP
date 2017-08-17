// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Interfaces;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    /// <typeparam name="TAuditedModel">Type of entity the sort details are for.</typeparam>
    public class AuditedSortDetail<TAuditedModel> : SortDetail<TAuditedModel>
        where TAuditedModel : IAuditedModel
    {
        #region Public Properties

        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new AuditedSortColumn Column
        {
            get { return (AuditedSortColumn)base.Column; }
            set { base.Column = (SortColumn)value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<TAuditedModel> Sort(IEnumerable<TAuditedModel> source)
        {
            switch (this.Column)
            {
                case AuditedSortColumn.LastModified:
                    return this.Sort(source, entity => entity.LastModified);
                case AuditedSortColumn.ModifiedBy:
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
        public override int Compare(TAuditedModel x, TAuditedModel y)
        {
            switch (this.Column)
            {
                case AuditedSortColumn.LastModified:
                    return this.Compare(x.LastModified.GetValueOrDefault(), y.LastModified.GetValueOrDefault());
                case AuditedSortColumn.ModifiedBy:
                    return this.Compare(x.ModifiedBy.GetValueOrDefault(), y.ModifiedBy.GetValueOrDefault());
                default:
                    return base.Compare(x, y);
            }
        }

        #endregion
    }
}
