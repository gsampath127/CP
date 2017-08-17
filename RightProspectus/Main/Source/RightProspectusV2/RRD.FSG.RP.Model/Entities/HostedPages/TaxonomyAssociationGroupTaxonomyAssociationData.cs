using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    public class TaxonomyAssociationGroupTaxonomyAssociationData
    {
        /// <summary>
        /// Gets or sets the ChildTAGTAData
        /// </summary>
        public List<TaxonomyAssociationGroupTaxonomyAssociationData> ChildTAGTAData { get; set; }

        /// <summary>
        /// Gets or sets the TaxonomyAssociationGroupId
        /// </summary>
        public int TaxonomyAssociationGroupId { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the SiteId
        /// </summary>
        public int? SiteId { get; set; }

        /// <summary>
        /// Gets or sets the ParentTaxonomyAssociationId
        /// </summary>
        public int? ParentTaxonomyAssociationId { get; set; }

        /// <summary>
        /// Gets or sets the ParentTaxonomyAssociationGroupId
        /// </summary>
        public int? ParentTaxonomyAssociationGroupId { get; set; }

        /// <summary>
        /// Gets or sets the CssClass
        /// </summary>
        public string CssClass { get; set; }        

        /// <summary>
        /// Gets or sets the taxonomy association documents data.
        /// </summary>
        /// <value>The taxonomy association documents data.</value>
        public List<TaxonomyAssociationData> TaxonomyAssociationData { get; set; }

        /// <summary>
        /// Gets or sets the GroupLevel
        /// </summary>
        public int GroupLevel { get; set; }

        /// <summary>
        /// Gets or sets the GroupOrder
        /// </summary>
        public int? GroupOrder { get; set; }
        
    }
}
