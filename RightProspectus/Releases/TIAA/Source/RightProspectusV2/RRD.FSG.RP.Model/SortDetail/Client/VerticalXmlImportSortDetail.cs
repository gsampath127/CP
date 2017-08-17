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
    /// Class VerticalXmlImportSortDetail.
    /// </summary>
    public class VerticalXmlImportSortDetail : SortDetail<VerticalXmlImportObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new VerticalXmlImportSortColumn Column
        {
            get { return (VerticalXmlImportSortColumn)base.Column; }
            set { base.Column = (SortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<VerticalXmlImportObjectModel> Sort(IEnumerable<VerticalXmlImportObjectModel> source)
        {
            switch (this.Column)
            {
                case VerticalXmlImportSortColumn.ImportDate:
                    return this.Sort(source, entity => entity.ImportDate);
                case VerticalXmlImportSortColumn.ImportDescription:
                    return this.Sort(source, entity => entity.ImportDescription);
                case VerticalXmlImportSortColumn.ImportedByName:
                    return this.Sort(source, entity => entity.ImportedByName);
                case VerticalXmlImportSortColumn.Status:
                    return this.Sort(source, entity => entity.Status);
                case VerticalXmlImportSortColumn.VerticalXmlImportId:
                    return this.Sort(source, entity => entity.VerticalXmlImportId);
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
        public override int Compare(VerticalXmlImportObjectModel x, VerticalXmlImportObjectModel y)
        {
            switch (this.Column)
            {
                case VerticalXmlImportSortColumn.ImportDate:
                    return this.Compare(x.ImportDate, y.ImportDate);
                case VerticalXmlImportSortColumn.ImportDescription:
                    return this.Compare(x.ImportDescription, y.ImportDescription);
                case VerticalXmlImportSortColumn.ImportedByName:
                    return this.Compare(x.ImportedByName, y.ImportedByName);
                case VerticalXmlImportSortColumn.Status:
                    return this.Compare(x.Status, y.Status);
                case VerticalXmlImportSortColumn.VerticalXmlImportId:
                    return this.Compare(x.VerticalXmlImportId, y.VerticalXmlImportId);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
