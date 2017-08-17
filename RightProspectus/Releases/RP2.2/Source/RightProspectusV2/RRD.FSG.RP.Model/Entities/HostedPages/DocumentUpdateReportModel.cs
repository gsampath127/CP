using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    public class DocumentUpdateReportModel
    {
        public string MarketID { get; set; }
        public int DocumentTypeID { get; set; }
        public string TaxonomyName { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? DocumentUpdatedDate { get; set; }        
        public string DocumentTypeMarketId { get; set; }
        public string DocumentTypeName { get; set; }
        public string IsDocUpdated { get; set; }
    }
}
