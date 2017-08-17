using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class DocUpdate
    {
        public string RPProcessStep { get; set; }

        public string CUSIP { get; set; }

        public string FundName { get; set; }

        public string PDFName { get; set; }

        public string FTPPath { get; set; }

        public string DocumentType { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime DocumentDate { get; set; }

        public int RRDInternalDocumentID { get; set; }

        public string RRDExternalDocumentID { get; set; }

        public string RRDPDFURL { get; set; }

        public string SECDetails { get; set; }

        public int? PageCount { get; set; }

        public double? PageSizeHeight { get; set; }

        public double? PageSizeWidth { get; set; }

        public string AccNum { get; set; }

        public DateTime FilingDate { get; set; }

        public string SECFormType { get; set; }

        public string SecurityTypeCode { get; set; }
    }
}
