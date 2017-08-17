using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    public class BillingReportModel
    {
        /// <summary>
        /// Gets or sets list of active fund details
        /// </summary>
        public List<BillingReportActiveFundDetails> LstBillingReportActiveFundDetails { get; set; }
        /// <summary>
        /// Gets or sets list of removed fund details
        /// </summary>
        public List<BillingReportRemovedFundDetails> LstBillingReportRemovedFundDetails { get; set; }
        /// <summary>
        /// Gets or sets active fund count
        /// </summary>
        public int ActiveFundCount { get; set; }
        /// <summary>
        /// Gets or sets removed fund count
        /// </summary>
        public int RemovedFundCount { get; set; }
        /// <summary>
        /// Gets or sets summaryprospectusCount
        /// </summary>
        public int SummaryProspectusCount { get; set; }
        /// <summary>
        /// Gets or sets new fund count
        /// </summary>
        public int NewFundCount { get; set; }
       
    }

    public class BillingReportActiveFundDetails
    {
        /// <summary>
        /// Gets or sets the market identifier
        /// </summary>
        /// <value>The market identifier.</value>
        public string MarketID { get; set; }
        /// <summary>
        /// Gets or sets the series identifier
        /// </summary>
        public string SeriesID { get; set; }
        /// <summary>
        /// Gets or sets the fund name
        /// </summary>
        public string FundName { get; set; }
        /// <summary>
        /// flag for checking summary prospectus
        /// </summary>
        public bool IsSummaryProspectus { get; set; }
        /// <summary>
        /// gets or stes the site name
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// flag for checking the object in vertical market
        /// </summary>
        public bool IsObjectinVerticalMarket { get; set; }
        
    }

    public class BillingReportRemovedFundDetails
    {
        /// <summary>
        /// Gets or sets the market identifier
        /// </summary>
        /// <value>The market identifier.</value>
        public string MarketID { get; set; }
        /// <summary>
        /// gets or sets the cusip removed date
        /// </summary>
        public DateTime removedDate { get; set; }
    }
}
