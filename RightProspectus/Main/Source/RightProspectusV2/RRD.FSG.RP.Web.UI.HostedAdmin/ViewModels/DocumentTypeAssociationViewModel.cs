using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    public class DocumentTypeAssociationViewModel
    {
        /// <summary>
        /// DocumentTypeAssociationId
        /// </summary>
        /// <value>The document type association identifier.</value>
        public int DocumentTypeAssociationId { get; set; }
        /// <summary>
        /// DocumentTypeId
        /// </summary>
        /// <value>The document type identifier.</value>
        public int DocumentTypeId { get; set; }
        /// <summary>
        /// SiteId
        /// </summary>
        /// <value>The site identifier.</value>
        public int? SiteId { get; set; }
        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        /// <value>The taxonomy association identifier.</value>
        public int? TaxonomyAssociationId { get; set; }
        /// <summary>
        /// Order
        /// </summary>
        /// <value>The order.</value>
        public int? Order { get; set; }
        /// <summary>
        /// HeaderText
        /// </summary>
        /// <value>The header text.</value>
        public string HeaderText { get; set; }
        /// <summary>
        /// LinkText
        /// </summary>
        /// <value>The link text.</value>
        public string LinkText { get; set; }
        /// <summary>
        /// DescriptionOverride
        /// </summary>
        /// <value>The description override.</value>
        public string DescriptionOverride { get; set; }
        /// <summary>
        /// CssClass
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }
        /// <summary>
        /// MarketId
        /// </summary>
        /// <value>The market identifier.</value>
        public string MarketId { get; set; }
        /// <summary>
        /// MarketId
        /// </summary>
        /// <value>The TaxonomyMarketId identifier.</value>
        public string TaxonomyMarketId { get; set; }
        /// <summary>
        /// IsProofing
        /// </summary>
        /// <value>The IsProofing identifier.</value>
        public bool IsProofing { get; set; }
    }
}