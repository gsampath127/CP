using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSMissingReports
    {
        public string CUSIP { get; set; }
        public string CompanyName { get; set; }
        public string FundName { get; set; }
        public string CIK { get; set; }
        public string SeriesID { get; set; }
        public string ClassContractID { get; set; }
        public string Ticker { get; set; }
    }
}
