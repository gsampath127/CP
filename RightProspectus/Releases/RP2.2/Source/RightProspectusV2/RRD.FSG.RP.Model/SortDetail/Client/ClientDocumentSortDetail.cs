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

namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Class ClientDocumentSortDetail.
    /// </summary>
    public class ClientDocumentSortDetail
   : AuditedSortDetail<ClientDocumentObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new ClientDocumentSortColumn Column
        {
            get { return (ClientDocumentSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<ClientDocumentObjectModel> Sort(IEnumerable<ClientDocumentObjectModel> source)
        {
            switch (this.Column)
            {
                case ClientDocumentSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case ClientDocumentSortColumn.Name:
                    return this.Sort(source, entity => entity.Name);
                case ClientDocumentSortColumn.FileName:
                    return this.Sort(source, entity => entity.FileName);
                case ClientDocumentSortColumn.ClientDocumentTypeId:
                    return this.Sort(source, entity => entity.ClientDocumentTypeId);
                case ClientDocumentSortColumn.MimeType:
                    return this.Sort(source, entity => entity.MimeType);
                case ClientDocumentSortColumn.IsPrivate:
                    return this.Sort(source, entity => entity.IsPrivate);
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
        public override int Compare(ClientDocumentObjectModel x, ClientDocumentObjectModel y)
        {
            switch (this.Column)
            {
                case ClientDocumentSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case ClientDocumentSortColumn.Name:
                    return this.Compare(x.Name, y.Name);
                case ClientDocumentSortColumn.FileName:
                    return this.Compare(x.FileName, y.FileName);
                case ClientDocumentSortColumn.ClientDocumentTypeId:
                    return this.Compare(x.ClientDocumentTypeId, y.ClientDocumentTypeId);
                case ClientDocumentSortColumn.MimeType:
                    return this.Compare(x.MimeType, y.MimeType);
                case ClientDocumentSortColumn.IsPrivate:
                    return this.Compare(x.IsPrivate, y.IsPrivate);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
