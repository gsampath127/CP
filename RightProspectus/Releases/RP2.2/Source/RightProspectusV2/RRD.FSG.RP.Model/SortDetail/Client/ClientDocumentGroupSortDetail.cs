// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-29-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using System.Collections.Generic;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.SortDetail.Client
{
    /// <summary>
    /// Class ClientDocumentGroupSortDetail.
    /// </summary>
    public class ClientDocumentGroupSortDetail : AuditedSortDetail<ClientDocumentGroupObjectModel>
    {

        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new ClientDocumentGroupSortColumn Column
        {
            get { return (ClientDocumentGroupSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<ClientDocumentGroupObjectModel> Sort(IEnumerable<ClientDocumentGroupObjectModel> source)
        {
            switch (this.Column)
            {
                case ClientDocumentGroupSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case ClientDocumentGroupSortColumn.Name:
                    return this.Sort(source, entity => entity.Name);
                case ClientDocumentGroupSortColumn.ClientDocumentGroupId:
                    return this.Sort(source, entity => entity.ClientDocumentGroupId);
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
        public override int Compare(ClientDocumentGroupObjectModel x, ClientDocumentGroupObjectModel y)
        {
            switch (this.Column)
            {
                case ClientDocumentGroupSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case ClientDocumentGroupSortColumn.Name:
                    return this.Compare(x.Name, y.Name);
                case ClientDocumentGroupSortColumn.ClientDocumentGroupId:
                    return this.Compare(x.ClientDocumentGroupId, y.ClientDocumentGroupId);
                
                default:
                    return base.Compare(x, y);
            }
        }

    }
}
