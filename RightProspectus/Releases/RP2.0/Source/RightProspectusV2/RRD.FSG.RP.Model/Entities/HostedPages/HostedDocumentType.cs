// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************

using System.Collections.Generic;

/// <summary>
/// The HostedPages namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    /// <summary>
    /// Class HostedDocumentType.
    /// </summary>
    public class HostedDocumentType
    {
        /// <summary>
        /// Gets or sets the document type link text.
        /// </summary>
        /// <value>The document type link text.</value>
        public string DocumentTypeLinkText { get; set; }

        /// <summary>
        /// Gets or sets the document type description override.
        /// </summary>
        /// <value>The document type description override.</value>
        public string DocumentTypeDescriptionOverride { get; set; }

        /// <summary>
        /// Gets or sets the document type CSS class.
        /// </summary>
        /// <value>The document type CSS class.</value>
        public string DocumentTypeCssClass { get; set; }

        /// <summary>
        /// Gets or sets the document type order.
        /// </summary>
        /// <value>The document type order.</value>
        public int DocumentTypeOrder { get; set; }

        /// <summary>
        /// Gets or sets the document type identifier.
        /// </summary>
        /// <value>The document type identifier.</value>
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the document type external identifier.
        /// </summary>
        /// <value>The document type external identifier.</value>
        public List<string> DocumentTypeExternalID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is object in vertical market.
        /// </summary>
        /// <value><c>true</c> if this instance is object in vertical market; otherwise, <c>false</c>.</value>
        public bool IsObjectinVerticalMarket { get; set; }

        /// <summary>
        /// Gets or sets the content URI.
        /// </summary>
        /// <value>The content URI.</value>
        public string ContentURI { get; set; }

        /// <summary>
        /// Gets or sets the vertical market identifier.
        /// </summary>
        /// <value>The vertical market identifier.</value>
        public string VerticalMarketID { get; set; }

        /// <summary>
        /// Gets or sets the name of the sku.
        /// </summary>
        /// <value>The name of the sku.</value>
        public string SKUName { get; set; }
    }
}
