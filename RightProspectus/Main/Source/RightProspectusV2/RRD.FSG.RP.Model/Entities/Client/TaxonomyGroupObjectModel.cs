using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.Client
{
    public class TaxonomyGroupObjectModel
    {
        public List<TaxonomyGroupObjectModel> ChildTAGData { get; set; }
        public int? TaxonomyAssociationGroupId{get;set;}
        public string Name{get;set;}
	    public string Description{get;set;}
	    public int? SiteId {get;set;}
	    public int? ParentTaxonomyAssociationId{get;set;}
	    public int? ParentTaxonomyAssociationGroupId{get;set;}
        public int? Order { get; set; }
	    public string CssClass{get;set;}
	    public DateTime? UtcModifiedDate{get;set;}
        public int? ModifiedBy { get; set; }
        public int Level { get; set; }
        public string ParentName { get; set; }
        public bool HasChildren { get; set; }
        public bool HasFundData { get; set; }
    }
}
