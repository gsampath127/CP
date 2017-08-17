using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSDailyUpdateReport
    {
        public string CUSIP { get; set; }
        public string CompanyName { get; set; }
        public string FundName { get; set; }
        public string CompanyCIK { get; set; }
        public string SeriesID { get; set; }
        public string Class { get; set; }
        public string Ticker { get; set; }
        public string OldSecurityType { get; set; }
        public string SecurityType { get; set; }


    }
}
