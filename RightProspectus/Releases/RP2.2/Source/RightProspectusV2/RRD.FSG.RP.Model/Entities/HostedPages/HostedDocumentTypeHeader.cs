// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-17-2015
// ***********************************************************************

/// <summary>
/// The HostedPages namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    /// <summary>
    /// Class HostedDocumentTypeHeader.
    /// </summary>
    public class HostedDocumentTypeHeader
    {
        /// <summary>
        /// Gets or sets the name of the header.
        /// </summary>
        /// <value>The name of the header.</value>
        public string HeaderName { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the document type identifier.
        /// </summary>
        /// <value>The document type identifier.</value>
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the vertical market identifier.
        /// </summary>
        /// <value>The vertical market identifier.</value>
        public string VerticalMarketID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is object in vertical market.
        /// </summary>
        /// <value><c>true</c> if this instance is object in vertical market; otherwise, <c>false</c>.</value>
        public bool IsObjectinVerticalMarket { get; set; }
    }
}
