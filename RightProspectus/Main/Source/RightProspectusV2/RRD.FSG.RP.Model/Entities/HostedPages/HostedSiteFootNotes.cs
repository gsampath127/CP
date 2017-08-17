// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : NI317175
// Created          : 10-05-2015
//
// Last Modified By : NI317175
// Last Modified On : 11-17-2015
// ***********************************************************************
/// <summary>
/// The HostedPages namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    /// <summary>
    /// Class HostedSiteFootNotes.
    /// </summary>
    public class HostedSiteFootNotes
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        
        public int  Order { get; set; }
        /// <summary>
        /// Gets or sets the taxonomy identifier.
        /// </summary>
        /// <value>The taxonomy identifier.</value>
        
        public int TaxonomyID { get; set; }
        /// <summary>
        /// Gets or sets the TaxonomyName.
        /// </summary>
        /// <value>The tTaxonomyName.</value>

        public string TaxonomyName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is object in vertical market.
        /// </summary>
        /// <value><c>true</c> if this instance is object in vertical market; otherwise, <c>false</c>.</value>
       
        public bool IsObjectinVerticalMarket { get; set; }
    }
}
