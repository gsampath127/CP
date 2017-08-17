// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

using System;
using System.Collections.Generic;

/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Added to hold the Clients related data
    /// </summary>
    public class ClientDNSObjectModel 
    {
        /// <summary>
        /// Gets or sets the client DNS identifier.
        /// </summary>
        /// <value>The client DNS identifier.</value>
        public int ClientDnsId { get; set; }

        /// <summary>
        /// Gets or sets the DNS.
        /// </summary>
        /// <value>The DNS.</value>
        public string Dns { get; set; }
        /// <summary>
        /// Gets or sets the DNS SiteId.
        /// </summary>
        /// <value>The DNS.</value>
        public int ClientDnsSiteId { get; set; }
    }

    /// <summary>
    /// Class ClientObjectModel.
    /// </summary>
    public class ClientObjectModel : AuditedBaseModel<int>, IComparable<ClientObjectModel>
    {
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>The client identifier.</value>
        public int ClientID { get; set; }
     
        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the name of the connection string.
        /// </summary>
        /// <value>The name of the connection string.</value>
        public string ClientConnectionStringName{ get; set; }
        
        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>The name of the database.</value>
        public string  ClientDatabaseName { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the connection string.
        /// </summary>
        /// <value>The name of the connection string.</value>
        public string VerticalMarketConnectionStringName { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>The name of the database.</value>
        public string VerticalMarketDatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the vertical market identifier.
        /// </summary>
        /// <value>The vertical market identifier.</value>
        public int?  VerticalMarketId { get; set; }
        
        /// <summary>
        /// Gets or sets the client description.
        /// </summary>
        /// <value>The client description.</value>
        public string   ClientDescription{ get; set; }
        
        /// <summary>
        /// Gets or sets the Vertical Market Name.
        /// </summary>
        /// <value>The Vertical Market Name.</value>
        public string VerticalMarketName { get; set; }
        
        /// <summary>
        /// Gets or sets the ClientDnsList.
        /// </summary>
        /// <value>The ClientDnsList.</value>
        public List<ClientDNSObjectModel> ClientDnsList { get; set; }
        
        /// <summary>
        /// Gets or sets the ClientUsers.
        /// </summary>
        /// <value>The UserClients.</value>
        public List<int> Users { get; set; }

        /// <summary>
        /// Compares the two Site entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(ClientObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }

    
}
