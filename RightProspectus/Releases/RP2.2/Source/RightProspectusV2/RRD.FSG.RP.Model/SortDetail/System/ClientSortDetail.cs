// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.System;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.SortDetail.System
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    public class ClientSortDetail
        : AuditedSortDetail<ClientObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        public virtual new ClientSortColumn Column
        {
            get { return (ClientSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<ClientObjectModel> Sort(IEnumerable<ClientObjectModel> source)
        {
            switch (this.Column)
            {
                case ClientSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case ClientSortColumn.ClientName:
                    return this.Sort(source, entity => entity.ClientName);
                case ClientSortColumn.ClientConnectionStringName:
                    return this.Sort(source, entity => entity.ClientConnectionStringName);
                case ClientSortColumn.ClientDatabaseName:
                    return this.Sort(source, entity => entity.ClientDatabaseName);
                case ClientSortColumn.VerticalMarketId:
                    return this.Sort(source, entity => entity.VerticalMarketId);
                case ClientSortColumn.VerticalMarketName:
                    return this.Sort(source, entity => entity.VerticalMarketName);
                case ClientSortColumn.VerticalMarketDatabaseName:
                    return this.Sort(source, entity => entity.VerticalMarketDatabaseName);
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
        public override int Compare(ClientObjectModel x, ClientObjectModel y)
        {
            switch (this.Column)
            {
                case ClientSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case ClientSortColumn.ClientName:
                    return this.Compare(x.ClientName, y.ClientName);
                case ClientSortColumn.ClientConnectionStringName:
                    return this.Compare(x.ClientConnectionStringName, y.ClientConnectionStringName);
                case ClientSortColumn.ClientDatabaseName:
                    return this.Compare(x.ClientDatabaseName, y.ClientDatabaseName);
                case ClientSortColumn.VerticalMarketId:
                    return this.Compare(x.VerticalMarketId.Value, y.VerticalMarketId.Value);
                case ClientSortColumn.VerticalMarketName:
                    return this.Compare(x.VerticalMarketName, y.VerticalMarketName);
                case ClientSortColumn.VerticalMarketDatabaseName:
                    return this.Compare(x.VerticalMarketDatabaseName, y.VerticalMarketDatabaseName);

                default:
                    return base.Compare(x, y);
            }
        }
    }
}
