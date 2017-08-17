// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    public class StaticResourceSortDetail
        : AuditedSortDetail<StaticResourceObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new StaticResourceSortColumn Column
        {
            get { return (StaticResourceSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<StaticResourceObjectModel> Sort(IEnumerable<StaticResourceObjectModel> source)
        {
            switch (this.Column)
            {
                case StaticResourceSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case StaticResourceSortColumn.FileName:
                    return this.Sort(source, entity => entity.FileName);
                case StaticResourceSortColumn.MimeType:
                    return this.Sort(source, entity => entity.MimeType);
                case StaticResourceSortColumn.Size:
                    return this.Sort(source, entity => entity.Size);

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
        public override int Compare(StaticResourceObjectModel x, StaticResourceObjectModel y)
        {
            switch (this.Column)
            {
                case StaticResourceSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case StaticResourceSortColumn.FileName:
                    return this.Compare(x.FileName, y.FileName);
                case StaticResourceSortColumn.MimeType:
                    return this.Compare(x.MimeType, y.MimeType);
                case StaticResourceSortColumn.Size:
                    return this.Compare(x.Size, y.Size);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
