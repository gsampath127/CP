using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.Client
{
   public class TaxonomyAssociationGroupTaxonomyAssociationObjectModel
    {
       public int? TaxonomyAssociationGroupId { get; set; }
       public string MarketId { get; set; }
       public string NameOverride { get; set; }
       public string Name { get; set; }
       public int? TaxonomyAssociationId { get;set;}
       public int? Order { get; set; }
       public DateTime? UtcModifiedDate { get; set; }
       public int? ModifiedBy { get; set; }
    }
}
