using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSTRPReportData
    {
        public List<BCSTRPReportFLTFTPInfoData> BCSTRPReportFLTFTPInfoData { get; set; }
        public int BCSTRPReportFLTFTPInfoDataVirtualCount { get; set; }

        public List<BCSTRPReportFLTMissingData> BCSTRPReportFLTMissingData { get; set; }
        public int BCSTRPReportFLTMissingDataVirtualCount { get; set; }

        public List<BCSTRPReportRPCUSIPMissingData> BCSTRPReportRPCUSIPMissingData { get; set; }
        public int BCSTRPReportRPCUSIPMissingDataVirtualCount { get; set; }

        public List<BCSTRPReportBlankFLTCUSIPData> BCSTRPReportBlankFLTCUSIPData { get; set; }
        public int BCSTRPReportBlankFLTCUSIPDataVirtualCount { get; set; }
        
        
    }
}
