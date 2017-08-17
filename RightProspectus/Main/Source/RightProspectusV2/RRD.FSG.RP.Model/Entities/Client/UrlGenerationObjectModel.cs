using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.Client
{
    public class UrlGenerationObjectModel
    {
        /// <summary>
        /// Site Name
        /// </summary>
        /// <value>The Site Name identifier.</value>
        public string SiteName { get; set; }
        /// <summary>
        /// Fund Name
        /// </summary>
        /// <value>The Fund Name identifier.</value>
        public string FundName { get; set; }
        /// <summary>
        /// CUSIP
        /// </summary>
        /// <value>The CUSIP identifier.</value>
        public string MarketID { get; set; }
        /// <summary>
        /// TaxonomyId
        /// </summary>
        /// <value>TaxonomyId</value>
        public int TaxonomyId { get; set; }
        /// <summary>
        /// TLEExternalID
        /// </summary>
        /// <value>TLEExternalID</value>
        public string TLEExternalID { get; set; }
        /// <summary>
        /// Document Type
        /// </summary>
        /// <value>The document type identifier.</value>
        public string DocumentType { get; set; }
        /// <summary>
        /// Document Type
        /// </summary>
        /// <value>The document type identifier.</value>
        public int DocumentTypeOrder { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        /// <value>The Public URL identifier.</value>
        public string PublicUrl { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        /// <value>The Private URL identifier.</value>
        public string PrivateUrl { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is objectin vertical market.
        /// </summary>
        /// <value><c>true</c> if this instance is objectin vertical market; otherwise, <c>false</c>.</value>
        public bool IsObjectinVerticalMarket { get; set; }
    }
}
