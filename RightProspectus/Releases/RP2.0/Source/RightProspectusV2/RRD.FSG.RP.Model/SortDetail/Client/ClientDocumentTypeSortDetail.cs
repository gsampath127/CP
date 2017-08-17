// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using System.Collections.Generic;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Class ClientDocumentTypeSortDetail.
    /// </summary>
    public class ClientDocumentTypeSortDetail
        : AuditedSortDetail<ClientDocumentTypeObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new ClientDocumentTypeSortColumn Column
        {
            get { return (ClientDocumentTypeSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }
        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<ClientDocumentTypeObjectModel> Sort(IEnumerable<ClientDocumentTypeObjectModel> source)
        {
            switch (this.Column)
            {
                case ClientDocumentTypeSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case ClientDocumentTypeSortColumn.Name:
                    return this.Sort(source, entity => entity.Name);
                case ClientDocumentTypeSortColumn.Description:
                    return this.Sort(source, entity => entity.Description);
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
        public override int Compare(ClientDocumentTypeObjectModel x, ClientDocumentTypeObjectModel y)
        {
            switch (this.Column)
            {
                case ClientDocumentTypeSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case ClientDocumentTypeSortColumn.Name:
                    return this.Compare(x.Name, y.Name);
                case ClientDocumentTypeSortColumn.Description:
                    return this.Compare(x.Description, y.Description);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
