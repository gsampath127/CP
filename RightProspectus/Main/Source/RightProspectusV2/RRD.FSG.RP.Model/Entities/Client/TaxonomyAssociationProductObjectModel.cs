using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.Client
{
    public class TaxonomyAssociationProductObjectModel
    {
        public int ParentTaxonomyAssociationId { get; set; }
        public int? ChildTaxonomyAssociationId { get; set; }
        public int? ChildTaxonomyId { get; set; }
        public int? ParentTaxonmyId { get; set; }
        public string ChildMarketId { get; set; }
        public string ParentMarketId { get; set; }
        public string ChildNameOverride { get; set; }
        public string ParentNameOverride { get; set; }
        public int? RelationshipType { get; set; }
        public int? Order { get; set; }
        public DateTime? UtcModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
