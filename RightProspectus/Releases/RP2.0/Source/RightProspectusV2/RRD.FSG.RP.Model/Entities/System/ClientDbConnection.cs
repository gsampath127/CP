// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015


/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Class ClientDbConnection.
    /// </summary>
    public class ClientDbConnection
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
        /// Gets or sets the client DNS.
        /// </summary>
        /// <value>The client DNS.</value>
        public string ClientDNS { get; set; }

        /// <summary>
        /// Gets or sets the name of the client connection string.
        /// </summary>
        /// <value>The name of the client connection string.</value>
        public string ClientConnectionStringName { get; set; }

        /// <summary>
        /// Gets or sets the name of the client database.
        /// </summary>
        /// <value>The name of the client database.</value>
        public string ClientDatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the name of the vertical market connection string.
        /// </summary>
        /// <value>The name of the vertical market connection string.</value>
        public string VerticalMarketConnectionStringName { get; set; }

        /// <summary>
        /// Gets or sets the name of the vertical markets database.
        /// </summary>
        /// <value>The name of the vertical markets database.</value>
        public string VerticalMarketsDatabaseName { get; set; }
    }
}
