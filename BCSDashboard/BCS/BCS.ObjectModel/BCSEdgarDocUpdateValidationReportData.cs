using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSEdgarDocUpdateValidationReportData
    {
        public int BCSDocUpdateId { get; set; }
        public string CUSIP { get; set; }
        public int BCSDocUpdateEdgarID { get; set; }
        public int BCSURLDownloadQueueEdgarID { get; set; }
        public int ProsID { get; set; }
        public string FundName { get; set; }
    }
}
