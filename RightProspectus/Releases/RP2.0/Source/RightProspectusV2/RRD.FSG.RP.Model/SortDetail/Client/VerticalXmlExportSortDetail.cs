// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-16-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using System.Collections.Generic;


namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Class VerticalXmlExportSortDetail.
    /// </summary>
    public class VerticalXmlExportSortDetail : SortDetail<VerticalXmlExportObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new VerticalXmlExportSortColumn Column
        {
            get { return (VerticalXmlExportSortColumn)base.Column; }
            set { base.Column = (SortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<VerticalXmlExportObjectModel> Sort(IEnumerable<VerticalXmlExportObjectModel> source)
        {
            switch (this.Column)
            {
                case VerticalXmlExportSortColumn.ExportDate:
                    return this.Sort(source, entity => entity.ExportDate);
                case VerticalXmlExportSortColumn.ExportDescription:
                    return this.Sort(source, entity => entity.ExportDescription);
                case VerticalXmlExportSortColumn.ExportedByName:
                    return this.Sort(source, entity => entity.ExportedByName);
                case VerticalXmlExportSortColumn.Status:
                    return this.Sort(source, entity => entity.Status);
                case VerticalXmlExportSortColumn.VerticalXmlExportId:
                    return this.Sort(source, entity => entity.VerticalXmlExportId);
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
        public override int Compare(VerticalXmlExportObjectModel x, VerticalXmlExportObjectModel y)
        {
            switch (this.Column)
            {
                case VerticalXmlExportSortColumn.ExportDate:
                    return this.Compare(x.ExportDate, y.ExportDate);
                case VerticalXmlExportSortColumn.ExportDescription:
                    return this.Compare(x.ExportDescription, y.ExportDescription);
                case VerticalXmlExportSortColumn.ExportedByName:
                    return this.Compare(x.ExportedByName, y.ExportedByName);
                case VerticalXmlExportSortColumn.Status:
                    return this.Compare(x.Status, y.Status);
                case VerticalXmlExportSortColumn.VerticalXmlExportId:
                    return this.Compare(x.VerticalXmlExportId, y.VerticalXmlExportId);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
