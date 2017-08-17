using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSDocUpdateApprovalCUSIPDetails
    {
        public int BCSDocUpdateId { get; set; }
        public string CUSIP { get; set; }
        public int EdgarID { get; set; }
        public string Accnumber { get; set; }
        public string RRDPDFURL { get; set; }
        public string DocumentType { get; set; }
        public string DocumentDate { get; set; }
        public string DocumentID { get; set; }
        public string FundName { get; set; }
        public string Status { get; set; }
        public string StatusDate { get; set; }
        public string ReportType { get; set; }
    }
}
