using RRD.FSG.RP.Model.Entities.HostedPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// TaxonomyAssociationGroupViewModel
    /// </summary>
    public class TaxonomyAssociationGroupViewModel : BaseViewModel
    {
        /// </summary> 
        /// Gets or sets the ClientCustomHeader        
        /// <value>ClientCustomHeader</value>
        public string ClientCustomHeader { get; set; }
        /// <summary>
        /// Gets or sets the XBRL not available text.
        /// </summary>
        /// <value>The XBRL not available text.</value>
        public string XBRLNotAvailableText { get; set; }
        /// <summary>
        /// Gets or sets the footnotes header text.
        /// </summary>
        /// <value>The footnotes header text.</value>
        public string FootnotesHeaderText { get; set; }
        /// <summary>
        /// Gets or sets the glossary.
        /// </summary>
        /// <value>The glossary.</value>
        public string Glossary { get; set; }
        /// <summary>
        /// Gets or sets the value to show whether Document Type Header placed above top level category
        /// </summary>
        /// <value>The ShowDocumentTypeTopLevel.</value>
        public bool ShowDocumentTypeHeaderTopLevel { get; set; }
        /// <summary>
        /// Gets or Sets the value to enable group search
        /// </summary>
        /// <value>The EnableGroupSearch.</value>
        public bool EnableGroupSearch { get; set; }

        /// <summary>
        /// Gets or Sets the UnderlayingFundGridFundNameColumnText
        /// </summary>
        /// <value>UnderlayingFundGridFundNameColumnText</value>
        public string UnderlayingFundGridFundNameColumnText { get; set; }
       
        /// <summary>
        /// Gets or sets the taxonomy association group model data.
        /// </summary>
        /// <value>The taxonomy association group model data.</value>
        public TaxonomyAssociationGroupModel TaxonomyAssociationGroupModelData { get; set; }

        // <summary>
        /// Gets or Sets the TAGDDetails
        /// </summary>
        /// <value>TAGDDetails</value>
        public List<object> TAGDDetails { get; set; }

        // <summary>
        /// Gets or Sets the SelectedTAGDId
        /// </summary>
        /// <value>SelectedTAGDId</value>
        public int? SelectedTAGDId { get; set; }
        /// <summary>
        /// Gets or sets GroupHeaderText
        /// </summary>
        public string GroupHeaderText { get; set; }
        /// <summary>
        /// Gets or sets DisplayTAGDFooterTemplate
        /// </summary>
        public bool DisplayTAGDFooterTemplate { get; set; }
        /// <summary>
        /// Gets or sets TAGDLogoText
        /// </summary>
        public string TAGDLogoText { get; set; }
    }
}