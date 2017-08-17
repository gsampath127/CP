using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSDailyIPReportDetailRecords
    {
        public string DetailRecordType { get; set; }
        public string DetailSystem { get; set; }
        public string DetailClientID { get; set; }
        public string DetailRPProcessStep { get; set; }
        public string DetailCUSIPID { get; set; }
        public string DetailFundName { get; set; }
        public string DetailPDFName { get; set; }
        public string DetailRRDExternalDocID { get; set; }
        public string DetailDocumentType { get; set; }
        public string DetailEffectiveDate { get; set; }
        public string DetailDocumentDate { get; set; }
        public int DetailPageCount { get; set; }
        public double DetailPageSizeheight { get; set; }
        public double DetailPageSizeWidth { get; set; }
        public string DetailField15Reserved { get; set; }
        public int DetailRRDInternalDocID { get; set; }
        public string DetailField17Reserved { get; set; }
        public string DetailAccessionNum { get; set; }
        public string DetailFilingDate { get; set; }
        public string DetailSECFormType { get; set; }  
    }
}
