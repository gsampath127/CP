using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.VerticalMarket
{
    public class CUSIPMergerLiqudationReportObjectModel
    {
        /// <summary>
        /// TaxonomyID
        /// </summary>        
        public string MarketID { get; set; }
        /// <summary>
        /// Status
        /// </summary>        
        public string Status { get; set; }
        /// <summary>
        /// ClientName
        /// </summary>
        public string ClientName { get; set; }
    }
}
