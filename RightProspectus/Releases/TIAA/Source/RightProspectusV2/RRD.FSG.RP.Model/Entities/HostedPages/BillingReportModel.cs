using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    public class BillingReportModel
    {
        /// <summary>
        /// Gets or sets list of active fund details
        /// </summary>
        public List<BillingReportActiveFundDetails> LstBillingReportActiveFundDetails { get; set; }
        /// <summary>
        /// Gets or sets list of removed fund details
        /// </summary>
        public List<BillingReportRemovedFundDetails> LstBillingReportRemovedFundDetails { get; set; }
        /// <summary>
        /// DocumentTypeMaketIds
        /// </summary>
        public List<string> DocumentTypeMaketIds { get; set; }
        /// <summary>
        /// Gets or sets active fund count
        /// </summary>
        public int ActiveFundCount { get; set; }
        /// <summary>
        /// Gets or sets removed fund count
        /// </summary>
        public int RemovedFundCount { get; set; }
        /// <summary>
        /// Gets or sets summaryprospectusCount
        /// </summary>
        public int SummaryProspectusCount { get; set; }
        /// <summary>
        /// Gets or sets new fund count
        /// </summary>
        public int NewFundCount { get; set; }

        /// <summary>
        /// UniqueSeriesIdCount
        /// </summary>
        public int UniqueSeriesIdCount { get; set; }
        /// <summary>
        /// UniqueCUSIPcount
        /// </summary>
        public int UniqueCUSIPcount { get; set; }
        /// <summary>
        /// UniqueSPPdfNamesCount
        /// </summary>
        public int UniqueSummaryProspectusPdfNamesCount { get; set; }
        /// <summary>
        /// UniquePPdfNamesCount
        /// </summary>
        public int UniqueProspectusPdfNamesCount { get; set; }
        /// <summary>
        /// UniqueARPdfNamesCount
        /// </summary>
        public int UniqueAnnualReportPdfNamesCount { get; set; }
        /// <summary>
        /// UniqueSARPdfNamesCount
        /// </summary>
        public int UniqueSemiAnnualReportPdfNamesCount { get; set; }
        /// <summary>
        /// UniqueSAIPdfNamesCount
        /// </summary>
        public int UniqueSAIPdfNamesCount { get; set; }
        /// <summary>
        /// Gets or sets summaryprospectusCount
        /// </summary>
        public int ProspectusCount { get; set; }
        /// <summary>
        /// Gets or sets summaryprospectusCount
        /// </summary>
        public int SAICount { get; set; }
        /// <summary>
        /// Gets or sets summaryprospectusCount
        /// </summary>
        public int AnnualReportCount { get; set; }
        /// <summary>
        /// Gets or sets summaryprospectusCount
        /// </summary>
        public int SemiAnnualReportCount { get; set; }
        /// <summary>
        /// UniquePVRPdfNamesCount
        /// </summary>
        public int UniquePVRPdfNamesCount { get; set; }
        /// <summary>
        /// UniqueFSPdfNamesCount
        /// </summary>
        public int UniqueFSPdfNamesCount { get; set; }

        /// <summary>
        /// flag for checking XBRL
        /// </summary>
        public bool IsXBRLEnabled { get; set; }
        // <summary>
        /// flag for checking XBRL
        /// </summary>
        public bool IsPVREnabled { get; set; }
        // <summary>
        /// flag for checking XBRL
        /// </summary>
        public bool IsFSEnabled { get; set; }

        /// <summary>
        /// SPDocumentUpdatedCount
        /// </summary>
        public int SPDocumentUpdatedCount { get; set; }
        /// <summary>
        /// PDocumentUpdatedCount
        /// </summary>
        public int PDocumentUpdatedCount { get; set; }
        /// <summary>
        /// SAIDocumentUpdatedCount
        /// </summary>
        public int SAIDocumentUpdatedCount { get; set; }
        /// <summary>
        /// ARDocumentUpdatedCount
        /// </summary>
        public int ARDocumentUpdatedCount { get; set; }
        /// <summary>
        /// SARDocumentUpdatedCount
        /// </summary>
        public int SARDocumentUpdatedCount { get; set; }
        /// <summary>
        /// PVRDocumentUpdatedCount
        /// </summary>
        public int PVRDocumentUpdatedCount { get; set; }
        /// <summary>
        /// FSDocumentUpdatedCount
        /// </summary>
        public int FSDocumentUpdatedCount { get; set; }
       


    }

    public class BillingReportActiveFundDetails
    {
        public BillingReportActiveFundDetails()
        {
            IsSummaryProspectus = "N";
            IsProspectus = "N";
            IsSAI = "N";
            IsAnnualReport = "N";
            IsSemiAnnualReport = "N";
            IsXBRL = "N";
            IsPVR = "N";
            IsFactSheet = "N";

            SPPDFName = "NA";
            PPDFName = "NA";
            SAIPDFName = "NA";
            ARPDFName = "NA";
            SARPDFName = "NA";
            PVRPDFName = "NA";
            FactSheetPDFName = "NA";

            SPDocumentUpdatedDate = "N";
            PDocumentUpdatedDate = "N";
            SAIDocumentUpdatedDate = "N";
            ARDocumentUpdatedDate = "N";
            SARDocumentUpdatedDate = "N";
            FSDocumentUpdatedDate = "N";
            PVRDocumentUpdatedDate = "N";

        }
        /// <summary>
        /// Gets or sets the market identifier
        /// </summary>
        /// <value>The market identifier.</value>
        public string MarketID { get; set; }
        /// <summary>
        /// Gets or sets the series identifier
        /// </summary>
        public string SeriesID { get; set; }
        /// <summary>
        /// Gets or sets the fund name
        /// </summary>
        public string FundName { get; set; }
        /// <summary>
        /// flag for checking summary prospectus
        /// </summary>
        public string IsSummaryProspectus { get; set; }
        /// <summary>
        /// gets or stes the site name
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// flag for checking the object in vertical market
        /// </summary>
        public bool IsObjectinVerticalMarket { get; set; }

        /// <summary>
        /// SECFundName
        /// </summary>
        public string SECFundName { get; set; }
        /// <summary>
        /// P PDFName
        /// </summary>
        public string PPDFName { get; set; }
        /// <summary>
        /// AR PDFName
        /// </summary>
        public string ARPDFName { get; set; }
        /// <summary>
        /// SAI PDFName
        /// </summary>
        public string SAIPDFName { get; set; }
        /// <summary>
        /// SP PDFName
        /// </summary>
        public string SPPDFName { get; set; }
        /// <summary>
        /// SAR PDFName
        /// </summary>
        public string SARPDFName { get; set; }
        /// <summary>
        /// FactSheet PDFName
        /// </summary>
        public string FactSheetPDFName { get; set; }
        /// <summary>
        /// PVR PDFName
        /// </summary>
        public string PVRPDFName { get; set; }
        /// <summary>
        /// flag for checking summary prospectus
        /// </summary>
        public string IsProspectus { get; set; }
        /// <summary>
        /// flag for checking SAI
        /// </summary>
        public string IsSAI { get; set; }
        /// <summary>
        /// flag for checking Annual Report 
        /// </summary>
        public string IsAnnualReport { get; set; }
        /// <summary>
        /// flag for checking Semi Annual Report
        /// </summary>
        public string IsSemiAnnualReport { get; set; }
        /// <summary>
        /// flag for checking FactSheet
        /// </summary>
        public string IsFactSheet { get; set; }
        /// <summary>
        /// flag for checking PVR
        /// </summary>
        public string IsPVR { get; set; }
        /// <summary>
        /// flag for checking XBRL
        /// </summary>
        public string IsXBRL { get; set; }       

        /// <summary>
        /// flag for DocumentTypeMarketId
        /// </summary>
        public bool DocumentTypeMarketId { get; set; }

        /// <summary>
        /// SPDocumentUpdatedDate
        /// </summary>
        public string SPDocumentUpdatedDate { get; set; }
        /// <summary>
        /// PDocumentUpdatedDate
        /// </summary>
        public string PDocumentUpdatedDate { get; set; }
        /// <summary>
        /// SDocumentUpdatedDate
        /// </summary>
        public string SAIDocumentUpdatedDate { get; set; }
        /// <summary>
        /// ARDocumentUpdatedDate
        /// </summary>
        public string ARDocumentUpdatedDate { get; set; }
        /// <summary>
        /// SARDocumentUpdatedDate
        /// </summary>
        public string SARDocumentUpdatedDate { get; set; }
        /// <summary>
        /// PVRDocumentUpdatedDate
        /// </summary>
        public string PVRDocumentUpdatedDate { get; set; }
        /// <summary>
        /// FSDocumentUpdatedDate
        /// </summary>
        public string FSDocumentUpdatedDate { get; set; }
        
    }

    public class BillingReportRemovedFundDetails
    {
        /// <summary>
        /// Gets or sets the market identifier
        /// </summary>
        /// <value>The market identifier.</value>
        public string MarketID { get; set; }
        /// <summary>
        /// gets or sets the cusip removed date
        /// </summary>
        public DateTime removedDate { get; set; }
    }
}
