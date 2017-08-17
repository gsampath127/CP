using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSReports
    {
        public string CUSIP_WL { get; set; }
        public string CUSIP_RP { get; set; }
        public string FundName_WL { get; set; }
        public string FundName_RP { get; set; }
        public string Class_WL { get; set; }
        public string Class_RP { get; set; }
        public DateTime DateModified { get; set; }
        public string Status { get; set; }

    }

    public class SANFileDetails
    {
        public string FileName { get; set; }
        public DateTime ReceivedTime { get; set; }
        public string DirectoryName { get; set; }
    }

    public class Grid
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string SortDirection { get; set; }
        public string SortColumn { get; set; }
        public int Count { get; set; }
    }
    public class SLINKReportDetails
    {
        public string SLINKFileName { get; set; }
        public string ZipFileName { get; set; }
        public string ZipFilePath { get; set; }
        public string Status { get; set; }
        public DateTime ReceivedDate { get; set; }
    }
    public class FullfillmentInfo
    {
        public int TotalRequests { get; set; }
        public int Completed { get; set; }
        public int NotAvailable { get; set; }
        public List<FullfillmentInfoDetails> FullfillmentDetails { get; set; }
    }

    public class FullfillmentInfoDetails
    {
        public string TransID { get; set; }
        public string CUSIP { get; set; }
        public string Status { get; set; }
    }

    public class SlinkHeaderData
    {
        public int TotalCount { get; set; }
        public int ExCount { get; set; }
        public int APCount { get; set; }
        public int OPCount { get; set; }
        public int APCCount { get; set; }
        public int OPCCount { get; set; }

    }

    public class LiveUpdateReport
    {
        public BCSDocUpdateApprovalCUSIPData BCSDocUpdateApprovalCUSIPData { get; set; }
        public List<Cusips> lstInvalidCusips { get; set; }
        public List<LiveUpdateCUSIPDetails> LiveUpdateCUSIPDetails { get; set; }
        public List<DocIds> lstInvalidDocumentIds { get; set; }
        public List<LiveUpdateDocumentIdDetails> LiveUpdateDocumentIdDetails { get; set; }

        public bool Show_GridLiveUpdate { get; set; }
        public bool Show_divGridLiveUpdateInvalidCUSIPs { get; set; }
        public bool Show_divCUSIPCount { get; set; }
        public bool Show_divGridLiveUpdateInvalidDocIds { get; set; }
        public bool Show_divDocIdsCount { get; set; }

        public int TotalCount { get; set; }
    }

    public class LiveUpdateCUSIPDetails
    {
        public int TotalCUSIPs { get; set; }
        public int CUSIPsFound { get; set; }
        public int MissingCUSIPs { get; set; }
    }

    public class LiveUpdateDocumentIdDetails
    {
        public int TotalDocIds { get; set; }
        public int DocIdsFound { get; set; }
        public int MissingDocIds { get; set; }
    }

    public class Cusips
    {
        public string CUSIP { get; set; }
    }
    public class DocIds
    {
        public string DocId { get; set; }
    }

}


