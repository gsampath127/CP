// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using System.Collections.Generic;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Class CUDHistorySortDetail.
    /// </summary>
    public class CUDHistorySortDetail:SortDetail<CUDHistoryObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new CUDHistorySortColumn Column
        {
            get { return (CUDHistorySortColumn)base.Column; }
            set { base.Column = (SortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<CUDHistoryObjectModel> Sort(IEnumerable<CUDHistoryObjectModel> source)
        {
            switch (this.Column)
            {
                case CUDHistorySortColumn.TableName:
                    return this.Sort(source, entity => entity.TableName);
                case CUDHistorySortColumn.CUDType:
                    return this.Sort(source, entity => entity.CUDType);
                case CUDHistorySortColumn.UtcCUDDate:
                    return this.Sort(source, entity => entity.UtcCUDDate);
                case CUDHistorySortColumn.ColumnName:
                    return this.Sort(source, entity => entity.ColumnName);
               default:
                    return base.Sort(source);
            }
        }
    }
}
