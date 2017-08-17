// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// View model for client
    /// </summary>
    public class ClientViewModel
    {
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>The client identifier.</value>
        [DisplayName("Client ID")]
        public int? ClientID { get; set; }
        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        [DisplayName("Client Name")]
        public string ClientName { get; set; }
        /// <summary>
        /// Gets or sets the vertical market.
        /// </summary>
        /// <value>The vertical market.</value>
        [DisplayName("Vertical Market")]
        public List<DisplayValuePair> VerticalMarket { get; set; }
        /// <summary>
        /// Gets or sets the selected vertical market identifier.
        /// </summary>
        /// <value>The selected vertical market identifier.</value>
        public int? SelectedVerticalMarketId { get; set; }
        /// <summary>
        /// Gets or sets the name of the vertical market.
        /// </summary>
        /// <value>The name of the vertical market.</value>
        [DisplayName("Vertical Market")]
        public string VerticalMarketName { get; set; }
        /// <summary>
        /// Gets or sets the name of the client database.
        /// </summary>
        /// <value>The name of the client database.</value>
        [DisplayName("Client Database Name")]
        public string ClientDatabaseName { get; set; }
        /// <summary>
        /// Gets or sets the client connection string names.
        /// </summary>
        /// <value>The client connection string names.</value>
        [DisplayName("Client Connection String Name")]
        public List<DisplayValuePair> ClientConnectionStringNames { get; set; }
        /// <summary>
        /// Gets or sets the name of the selected client connection string.
        /// </summary>
        /// <value>The name of the selected client connection string.</value>
        public string SelectedClientConnectionStringName { get; set; }
        /// <summary>
        /// Gets or sets the client description.
        /// </summary>
        /// <value>The client description.</value>
        [DisplayName("Description")]
        public string ClientDescription { get; set; }
        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>The modified by.</value>
        [DisplayName("Modified By")]
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the UTC last modified date.
        /// </summary>
        /// <value>The UTC last modified date.</value>
        [DisplayName("UTC Last Modified Date")]
        public DateTime? UTCLastModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>The success or failed message.</value>
        public string SuccessOrFailedMessage { get; set; }
    }
}