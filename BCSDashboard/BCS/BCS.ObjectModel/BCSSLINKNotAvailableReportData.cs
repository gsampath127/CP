using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSSLINKNotAvailableReportData
    {
        public int BCSDocUpdateId { get; set; }
        public int EdgarID { get; set; }
        public string CUSIP { get; set; }
        public string FundName { get; set; }
        public string ProcessedDate { get; set; }
    }
}
