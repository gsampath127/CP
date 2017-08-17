using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class UploadDocument
    {
        public int ID { get; set; }

        public string Path { get; set; }

        public string OriginalFileName { get; set; }

        public string Company { get; set; }

        public DateTime Date { get; set; }

        public string CompanyID { get; set; }

        public string RenamedFileName { get; set; }

        public string ProsDocTypeId { get; set; }

        public string ProsID { get; set; }

        public bool SetDocRelation { get; set; }

        public string AWSDate { get; set; }

        public bool AWSEnabled { get; set; }

        public string TimeZone { get; set; }

        public string UploadedUser { get; set; }

        public bool IsDuplicate { get; set; }

        public bool IsDocUploadReady { get; set; }

        public bool IsFTPDocument { get; set; }

        public int? PageCount { get; set; }

        public double? PageHeight { get; set; }

        public double? PageWidth { get; set; }

        public int BookMark { get; set; }

    }
}
