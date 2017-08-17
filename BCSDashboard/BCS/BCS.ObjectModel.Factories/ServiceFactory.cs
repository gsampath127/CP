using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using BCS.ObjectModel.Factories.PreflightLinkWCF;
using BCS.ObjectModel;
using BCS.Core.DAL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Net;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using HiQPdf;
using iTextSharp;
using System.Configuration;

namespace BCS.ObjectModel.Factories
{
    public class ServiceFactory
    {

        private readonly string DB1029ConnectionString;

        private readonly string ReadOnlyDB1029ConnectionString;

        private readonly IDataAccess dataAccess;

        private int ErrorSleepTime = ConfigValues.ErrorSleepTime;

        private const string SPGetInitialLoadDocs = "BCS_GetInitialLoadProspectusDocs";

        private const string SPGetEdgarDocUpdateValidationReportData = "BCS_GetEdgarDocUpdateValidationReportData";

        private const string SPGetBCSDocUpdateSECValidationReportData = "BCS_GetBCSDocUpdateSECValidationReportData";

        private const string SPGetSLINKNotAvailableReportData = "BCS_GetSLINKNotAvailableReportData";

        private const string SPGetBCSDocUpdateSecDetailsNotMatchingReport = "BCS_DocUpdateSecDetailsNotMatchingReport";

        private const string SPPreflightUpdatesFromLink = "PreflightUpdatesFromLink";

        private const string SPPreFlightDetailsFromDBForClientID = "PreFlightDetailsFromDBForClientID";

        private const string SPGetSECDocumentsForProsIDandSummaryProspectus = "BCS_GetSECDocumentsForProsIDandSummaryProspectus";

        private const string SPGetSECDocumentsForProsIDandProspectus = "BCS_GetSECDocumentsForProsIDandProspectus";




        private const string SPSaveDocUpdateSECAndSlinkForGIM = "BCS_SaveDocUpdateSECAndSlinkForGIM";

        private const string SPSaveDocUpdateSLinkAndSECDetails = "BCS_SaveDocUpdateSLinkAndSECDetails";



        private const string SPGetClientConfigs = "BCS_GetClientConfig";

        private const string SPGetFLTClientConfigs = "BCS_GetFLTClientConfig";

        private const string SPGetWatchListClientConfigs = "BCS_GetWatchListClientConfig";

        private const string SPGetDocUpdateClientConfigs = "BCS_GetDocUpdateClientConfig";

        private const string SPGetALLClientConfig = "BCS_GetALLClientConfig";

        private const string SPUpdateGIMSLinkStatus = "BCS_UpdateGIMSLinkStatus";

        private const string SPCheckBCSDocUpdateValidation = "BCS_CheckBCSDocUpdateValidation";

        private const string SPGetNewlyAddedorModifiedCUSIPDetails = "BCS_GetNewlyAddedorModifiedCUSIPDetails";

        private const string SPGetAllOlderSPInFLMode = "BCS_GetAllOlderSPInFLMode";

        private const string SPAddNewCUSIPDetailsForExistingFunds = "BCS_AddNewCUSIPDetailsForExistingFunds";

        private const string SPDequeueURLDownloadQueue = "BCS_DequeueURLDownloadQueue";

        private const string SPUpdateURLToDownloadStatus = "BCS_UpdateURLToDownloadStatus";

        private const string SPSaveDocUpdateSLinkAndSECDetailsWithoutEdgarID = "BCS_SaveDocUpdateSLinkAndSECDetailsWithoutEdgarID";

        private const string SPGetFilingsPendingToBeProcessed = "BCS_GetFilingsPendingToBeProcessed";

        private const string SPGetARSARFilingsPendingToBeProcessed = "BCS_GetARSARFilingsPendingToBeProcessed";

        private const string SPSaveDocUpdateFilingsPendingToBeProcessed = "BCS_SaveDocUpdateFilingsPendingToBeProcessed";

        private const string SPSaveARSARFilingsPendingToBeProcessed = "BCS_SaveARSARFilingsPendingToBeProcessed";

        private const string SPSaveBaseAndSupplementsFilingsPendingToBeProcessed = "BCS_SaveBaseAndSupplementsFilingsPendingToBeProcessed";

        private const string SPGetAPCOPCNotReceivedPDFNames = "BCS_GetAPCOPCNotReceivedPDFNames";

        private const string SPUpdateAPCOPCReceivedDate = "BCS_UpdateAPCOPCReceivedDate";

        private const string SPGetBCSSynchronizerRecordsToArchive = "BCS_GetBCSSynchronizerRecordsToArchive";

        private const string SPSaveBCSSynchronizerArchive = "BCS_SaveBCSSynchronizerArchive";

        private const string SPGetTodaysDistinctBCSPDFURLs = "BCS_GetTodaysDistinctBCSPDFURLs";

        private const string SPCheckBCSTRPCUSIPMissingDetails = "BCS_CheckBCSTRPCUSIPMissingDetails";

        private const string SPCheckBCSTRPFLTFTPDataDiscrepancy = "BCS_CheckBCSTRPFLTFTPDataDiscrepancy";

        private const string SPBCSGetBCSSLINKReportData = "BCS_GetBCSSLINKReportData";

        private const string SPUpdateDequeueStatusForNewAddedCUSIPS = "BCS_UpdateDequeueStatusForNewAddedCUSIPS";

        private const string SPGetClientsForIPFileToBeSent = "BCS_GetClientsForIPFileToBeSent";

        private const string SPGetClientsForWatchlistFailureNotification = "BCS_GetClientsForWatchlistFailureNotification";

        private const string SPGetClientsForDocUPDTFailureNotification = "BCS_GetClientsForDocUPDTFailureNotification";

        private const string BCSSaveBCSDocUpdateFileHistory = "BCS_SaveBCSDocUpdateFileHistory";

        private const string DBCProsDocumentType = "ProsDocumentType";

        private const string DBCDocumentType = "DocumentType";

        private const string DBCRPProcessStep = "RPProcessStep";

        private const string DBCDASStatus = "DASStatus";

        private const string DBCDASReportingStatus = "DASReportingStatus";

        private const string DBCDASStatusReceivedDate = "DASStatusReceivedDate";

        private const string DBCClientID = "ClientID";

        private const string DBCDealNumber = "DealNumber";

        private const string DBCJobNumber = "JobNumber";

        private const string DBCCycleNumber = "CycleNumber";

        private const string DBCSECDetails = "SECDetails";

        private const string DBCRRDPDFURL = "RRDPDFURL";

        private const string DBCPDFName = "PDFName";

        private const string DBCIsAPF = "IsAPF";

        private const string DBCTableName = "TableName";

        private const string DBCCUSIP = "CUSIP";

        private const string DBCTickerID = "TickerID";

        private const string DBCProsID = "ProsID";

        private const string DBCEdgarID = "EdgarID";

        private const string DBCFundName = "FundName";

        private const string DBCProsDocID = "ProsDocID";

        private const string DBCProcessedDate = "ProcessedDate";

        private const string DBCBCSDocUpdateID = "BCSDocUpdateID";

        private const string DBCIsSlinkExists = "IsSlinkExists";

        private const string DBCZipFileName = "ZipFileName";

        private const string DBCZipFilePath = "ZipFilePath";

        private const string DBCExportedDate = "ExportedDate";

        private const string DBCSlinkFileName = "SlinkFileName";

        private const string DBCCompletedDate = "CompletedDate";

        private const string DBCPassedorFailed = "PassedorFailed";

        private const string DBCIsReprocessed = "IsReprocessed";

        private const string DBCIsProcessed = "IsProcessed";

        private const string DBCBCSURLDownloadQueueID = "BCSURLDownloadQueueID";

        private const string DBCIsErrored = "IsErrored";

        private const string DBCIsDownloaded = "IsDownloaded";


        private const string DBCFromDateTime = "FromDateTime";

        private const string DBCToDateTime = "ToDateTime";

        private const string DBCFilingAddedDate = "FilingAddedDate";

        private const string DBCFromProcess = "FromProcess";

        private const string DBCCurrentdate = "CurrentDate";

        private const string DBCYesterdaysdate = "YesterdaysDate";

        private const string DBCIPDocUpdateFileName = "IPDocUpdateFileName";

        private const string DBCHeaderDate = "HeaderDate";

        private const string DBCCreatedDate = "CreatedDate";

        private const string DBCClientName = "ClientName";

        private const string DBCSortColumn = "sortColumn";

        private const string DBCSortOrder = "sortDirection";

        private const string SPBCSGetDailyIPReportDetails = "BCS_GetDailyIPReportDetails";


        public ServiceFactory()
        {
            this.DB1029ConnectionString = DBConnectionString.DB1029ConnectionString();
            this.ReadOnlyDB1029ConnectionString = DBConnectionString.ReadOnlyDB1029Connection();
            this.dataAccess = new DataAccess();
        }

        public void ProcessInitialLoad(BCSClient bcsclient)
        {

            string CurrentUrlToDownload = string.Empty;

            ZipFileCounter zipfilecounter = new ZipFileCounter();

            zipfilecounter.Counter = 0;

            Slink slink = null;



            int CurrentProsID = -1;

            DataTable SECDetails = null;

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetInitialLoadDocs
                ))
            {


                while (reader.Read())
                {
                    //if (zipfilecounter.Counter >= 200)
                    //    return;
                    int ProsID = Convert.ToInt32(reader["ProsID"]);
                    string ProsDocTypeId = reader["ProsDocTypeId"].ToString();

                    if (CurrentProsID != ProsID || SECDetails == null)
                    {
                        SECDetails = null;

                        SECDetails = GetSECDetailsForProsID(ProsID, ProsDocTypeId);

                        CurrentProsID = ProsID;
                    }




                    if (SECDetails != null && SECDetails.Rows.Count > 0) // Process only if there are SEC Documents.
                    {

                        //string DocumentTypeCode = SECDetailsType.Select("ORDER BY DocumentDate desc")[0]["DocumentType"].ToString();

                        string URL = reader["URL"].ToString();

                        string CUSIP = reader["CUSIP"].ToString().Trim();

                        int TickerID = Convert.ToInt32(reader["TickerID"]);

                        string FundName = reader["FundName"].ToString();

                        int ProsDocID = Convert.ToInt32(reader["ProsDocID"]);

                        if (CurrentUrlToDownload != URL || (!slink.SlinkExists))
                        {
                            CurrentUrlToDownload = URL;

                            slink = CreateSLINKForURL(CurrentUrlToDownload, "7");

                            bcsclient.CurrentZipFileName = string.Empty;
                        }



                        if (bcsclient.CurrentZipFileName == string.Empty)
                        {
                            bcsclient.CurrentZipFileName = CreateSlinkZipFile(slink, bcsclient,
                                                        "7", zipfilecounter, ProsDocTypeId, -1);
                        }





                        this.dataAccess.ExecuteNonQuery
                         (
                              this.DB1029ConnectionString,
                              SPSaveDocUpdateSECAndSlinkForGIM,
                              this.dataAccess.CreateParameter(DBCSECDetails, SqlDbType.Structured, SECDetails),
                              this.dataAccess.CreateParameter(DBCRRDPDFURL, SqlDbType.NVarChar, slink.PDFURL),
                              this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                              this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                              this.dataAccess.CreateParameter(DBCTickerID, SqlDbType.Int, TickerID),
                              this.dataAccess.CreateParameter(DBCProsID, SqlDbType.Int, ProsID),
                              this.dataAccess.CreateParameter(DBCFundName, SqlDbType.NVarChar, FundName),
                              this.dataAccess.CreateParameter(DBCProsDocID, SqlDbType.Int, ProsDocID),
                              this.dataAccess.CreateParameter(DBCZipFileName, SqlDbType.NVarChar, bcsclient.CurrentZipFileName)
                          );

                        slink.SlinkExists = true;
                    }
                }
            }
        }

        public Slink CreateSLINKForURL(string URL, string ThreadNumber)
        {
            Slink slink = new Slink();

            DateTime CurrentDate = DateTime.Now;

            string DestinationPath = ConfigValues.RPBCSSANPath + CurrentDate.Year;

            if (!Directory.Exists(DestinationPath))
                Directory.CreateDirectory(DestinationPath);

            DestinationPath = DestinationPath + @"\" + CurrentDate.Month;

            if (!Directory.Exists(DestinationPath))
                Directory.CreateDirectory(DestinationPath);

            DestinationPath = DestinationPath + @"\" + CurrentDate.Day;

            if (!Directory.Exists(DestinationPath))
                Directory.CreateDirectory(DestinationPath);

            string DestinationPathWithPDFFolder = DestinationPath + @"\PDF";

            if (!Directory.Exists(DestinationPathWithPDFFolder))
                Directory.CreateDirectory(DestinationPathWithPDFFolder);


            string DownloadPathWithFileName = DestinationPathWithPDFFolder + @"\" + ThreadNumber + "download.pdf";

            if (URL.ToLower().StartsWith(ConfigValues.RPSourceURLReplace))
            {
                string SANPath = URL.Replace(ConfigValues.RPSourceURLReplace, ConfigValues.RPDestinationSANReplace)
                    .Replace(@"/", @"\");


                File.Copy(SANPath, DownloadPathWithFileName, true);
            }
            else
            {
                bool IsPDFValidated = UtilityFactory.DownloadAndValidateFile(URL,
                                         DownloadPathWithFileName,
                                         DestinationPathWithPDFFolder,
                                         ThreadNumber + "download.pdf",
                                         ThreadNumber);

                if (!IsPDFValidated)
                    return null;
            }

            slink.SlinkNameWithoutPdf = UtilityFactory.GetMD5HashFromFile(DownloadPathWithFileName);

            slink.SlinkPDFName = slink.SlinkNameWithoutPdf + ".pdf";





            slink.SlinkNameWithFilePath = DestinationPathWithPDFFolder + @"\" + slink.SlinkPDFName;

            //If GUID is new Create SLINK PDF Zip

            string ZipFileNameWithpath = string.Empty;

            slink.SlinkExists = false;

            slink.RecentlyCreated = false;

            if (!File.Exists(slink.SlinkNameWithFilePath))
            {
                File.Move(DownloadPathWithFileName, slink.SlinkNameWithFilePath);
                slink.RecentlyCreated = true;
            }
            else
            {
                slink.SlinkExists = true;
                File.Delete(DownloadPathWithFileName);
            }


            slink.PDFURL = slink.SlinkNameWithFilePath.Replace(ConfigValues.RPBCSSANPath, ConfigValues.RPDestinationURLReplace)
                                 .Replace(@"\", @"/");



            return slink;
        }

        public DataTable GetSECDetailsForProsID(int ProsID, string ProsDocTypeID)
        {

            DataTable SECDetailsType = new DataTable();
            SECDetailsType.Columns.Add("Acc#", typeof(string));
            SECDetailsType.Columns.Add("EdgarID", typeof(Int32));
            SECDetailsType.Columns.Add("DateFiled", typeof(DateTime));
            SECDetailsType.Columns.Add("DocumentDate", typeof(DateTime));
            SECDetailsType.Columns.Add("DocumentType", typeof(string));
            SECDetailsType.Columns.Add("EffectiveDate", typeof(DateTime));
            SECDetailsType.Columns.Add("FormType", typeof(string));


            string SPToExecute = ProsDocTypeID == "SP" ? SPGetSECDocumentsForProsIDandSummaryProspectus : SPGetSECDocumentsForProsIDandProspectus;

            using (IDataReader secreader = this.dataAccess.ExecuteReader
       (
            this.ReadOnlyDB1029ConnectionString,
            SPToExecute,
             this.dataAccess.CreateParameter(DBCProsID, SqlDbType.Int, ProsID)
        ))
            {

                while (secreader.Read())
                {
                    string DocumentType = secreader["DocumentType"].ToString();

                    string DocumentTypeCode = UtilityFactory.ConvertDocumentType(DocumentType);

                    SECDetailsType.Rows.Add(
                                secreader["Acc#"].ToString(),
                                Convert.ToInt32(secreader["EdgarID"]),
                                Convert.ToDateTime(secreader["DateFiled"]),
                                Convert.ToDateTime(secreader["DocumentDate"]),
                                DocumentTypeCode,
                                Convert.ToDateTime(secreader["EffectiveDate"]),
                                secreader["FormType"].ToString()
                                );
                }
            }
            return SECDetailsType;
        }

        public BCSSupplement CreateSlinkZipFileForSupplement(Slink slink, BCSClient bcsclient, string ThreadNumber, ZipFileCounter zipfilecounter
                                            , string supplementDocumentType, int edgarId,
                                            int prosId)
        {



            string RenamedPDFFileName = string.Empty;

            string RenameToPDFFileName = string.Empty;

            string InstructionsFileName = string.Empty;

            string SLINKInstructions = string.Empty;

            DateTime CurrentDate = DateTime.Now;

            BCSSupplement bcssupplement = new BCSSupplement();


            string DestinationPathWithZipFolder = bcsclient.DocUpdateStatusFilesSANPath + @"Zip";

            if (!Directory.Exists(DestinationPathWithZipFolder))
                Directory.CreateDirectory(DestinationPathWithZipFolder);


            DestinationPathWithZipFolder = DestinationPathWithZipFolder + @"\" + CurrentDate.Year;

            if (!Directory.Exists(DestinationPathWithZipFolder))
                Directory.CreateDirectory(DestinationPathWithZipFolder);

            DestinationPathWithZipFolder = DestinationPathWithZipFolder + @"\" + CurrentDate.Month;

            if (!Directory.Exists(DestinationPathWithZipFolder))
                Directory.CreateDirectory(DestinationPathWithZipFolder);

            DestinationPathWithZipFolder = DestinationPathWithZipFolder + @"\" + CurrentDate.Day;

            if (!Directory.Exists(DestinationPathWithZipFolder))
                Directory.CreateDirectory(DestinationPathWithZipFolder);



            string StoredProcedureName = "BCS_GetSupplementsZipFile";

            DataTable datatable = this.dataAccess.ExecuteDataTable
           (
                this.DB1029ConnectionString,
                StoredProcedureName,
                 this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                 this.dataAccess.CreateParameter(DBCEdgarID, SqlDbType.Int, edgarId),
                 this.dataAccess.CreateParameter(DBCProsID, SqlDbType.Int, prosId)
            );

            DataRow row = datatable.Rows[0];

            int? BCSDocUpdateSupplementsSlinkID = row.Field<int?>("BCSDocUpdateSupplementsSlinkID");



            if (BCSDocUpdateSupplementsSlinkID != null)
            {
                bcssupplement.BCSDocUpdateSupplementsSlinkID = BCSDocUpdateSupplementsSlinkID.Value;
                int? PageCount = row.Field<int?>("PageCount");
                if (PageCount != null)
                {
                    bcssupplement.PageCount = PageCount.Value;
                }
            }
            else
            {

                int? SLINKExtractFrom = row.Field<int?>("SLINKExtractFrom");

                int? SLINKExtractTo = row.Field<int?>("SLINKExtractTo");

                string ZipFileNameToString = string.Empty;

                string PDFAndTextIdentifierFileName = Guid.NewGuid().ToString().Replace("-", "");




                if (bcsclient.preflightjob == null) // Get Job Details the first time you need to create a SLINK for a particular day.
                {
                    bcsclient.preflightjob = GetJobDetailsFromPreFlightService(bcsclient);
                }

                PreFlightJob preflightjob = bcsclient.preflightjob;


                zipfilecounter.Counter += 1;

                string ZIPFileNamewithPath = DestinationPathWithZipFolder + @"\" + bcsclient.ClientPrefix + "_SLINK_" + CurrentDate.ToString("yyyyMMddHHmm") + "_" + ThreadNumber + zipfilecounter.Counter + ".zip";



                InstructionsFileName = supplementDocumentType + "_" + PDFAndTextIdentifierFileName + ".txt";

                RenamedPDFFileName = supplementDocumentType + "_" + PDFAndTextIdentifierFileName + ".pdf";

                RenameToPDFFileName = supplementDocumentType + slink.SlinkNameWithoutPdf + edgarId.ToString() + bcsclient.ClientPrefix + ".pdf";

                string SlinkInstructionsHeaderLine = "H|" + preflightjob.ClientID + "|"
                                                        + preflightjob.DealNumber + "|"
                                                        + preflightjob.JobNumber + "|"
                                                        + preflightjob.CycleNumber + "|";




                if ((SLINKExtractFrom.Value == 1 && SLINKExtractTo.Value == 1) || (SLINKExtractFrom.Value == 1 && SLINKExtractTo.Value == 0))
                {
                    bcssupplement.PageCount = 1;
                    SLINKInstructions = SLINKInstructions + "Step 1: Extract page 1 from " + RenamedPDFFileName + " and save that as " + RenameToPDFFileName + ". This new file should be 1 page.";
                }
                else if (SLINKExtractFrom.Value != 0 && SLINKExtractTo.Value == 0)
                {
                    bcssupplement.PageCount = 1;
                    SLINKInstructions = SLINKInstructions + "Step 1: Extract page " + SLINKExtractFrom.Value.ToString() + " from " + RenamedPDFFileName + " and save that as " + RenameToPDFFileName + ". This new file should be 1 page.";
                }
                else if ((SLINKExtractFrom.Value < SLINKExtractTo.Value) & (SLINKExtractTo.Value - SLINKExtractFrom.Value) == 1)
                {
                    bcssupplement.PageCount = 2;
                    SLINKInstructions = SLINKInstructions + "Step 1: Extract pages " + SLINKExtractFrom.Value.ToString() + " and " + SLINKExtractTo.Value.ToString() + " from " + RenamedPDFFileName + " and save that as " + RenameToPDFFileName + ". This new file should be 2 page.";
                }
                else if ((SLINKExtractFrom.Value < SLINKExtractTo.Value) & (SLINKExtractTo.Value - SLINKExtractFrom.Value) > 1)
                {
                    bcssupplement.PageCount = SLINKExtractTo.Value - SLINKExtractFrom.Value + 1;


                    SLINKInstructions = SLINKInstructions + "Step 1: Extract pages " + SLINKExtractFrom.Value.ToString() + " to " + SLINKExtractTo.Value.ToString() + "(including) from " + RenamedPDFFileName + " and save that as " + RenameToPDFFileName + ". This new file should be " + bcssupplement.PageCount.ToString() + " page.";
                }
                else if (SLINKExtractFrom.Value == SLINKExtractTo.Value & SLINKExtractFrom.Value != 1)
                {
                    bcssupplement.PageCount = 1;
                    SLINKInstructions = SLINKInstructions + "Step 1: Extract page " + SLINKExtractFrom.Value.ToString() + " from " + RenamedPDFFileName + " and save that as " + RenameToPDFFileName + ". This new file should be 1 page.";

                }
                else
                {

                    string ErrorEmailBody = "Error message: Unexpected Case for SLINK process. EdgarID = " + edgarId.ToString()
                                   + ", SLINKExtractFrom = " + SLINKExtractFrom.Value.ToString() + " ,SLINKExtractTo="
                                    + SLINKExtractTo.Value.ToString();
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null,
                                                "DequeueAndProcessURLToDownloadJob Create Zip for Supplement Exception", ErrorEmailBody, "support", null);

                    return null; // error return null object..

                }


                SlinkInstructionsHeaderLine += SLINKExtractFrom.Value.ToString() + "|" + SLINKExtractTo.Value.ToString() + "|" + RenamedPDFFileName + "|" + RenameToPDFFileName;

                string strInstructionFile = DestinationPathWithZipFolder + @"\" + InstructionsFileName;

                string strPDFFileToZip = DestinationPathWithZipFolder + @"\" + RenamedPDFFileName;

                using (StreamWriter tws = new StreamWriter(strInstructionFile, true))
                {
                    tws.WriteLine(SlinkInstructionsHeaderLine); //add the header line first.

                    if (SLINKInstructions.Length > 0)
                    {
                        foreach (string strLine in SLINKInstructions.Split('~'))
                        {
                            tws.WriteLine(strLine);
                        }
                    }
                    tws.Close();
                }

                File.Copy(slink.SlinkNameWithFilePath, strPDFFileToZip);


                List<string> filenames = new List<string>();
                filenames.Add(strInstructionFile);
                filenames.Add(strPDFFileToZip);




                using (ZipOutputStream s = new ZipOutputStream(File.Create(ZIPFileNamewithPath)))
                {

                    s.SetLevel(9); // 0-9, 9 being the highest compression

                    byte[] buffer = new byte[4096];

                    foreach (string file in filenames)
                    {

                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                        entry.DateTime = DateTime.Now;
                        entry.Size = new FileInfo(file).Length;
                        s.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file))
                        {

                            int sourceBytes;

                            do
                            {

                                sourceBytes = fs.Read(buffer, 0, buffer.Length);

                                s.Write(buffer, 0, sourceBytes);

                            }

                            while (sourceBytes > 0);

                        }

                    }

                    s.Finish();

                    s.Close();

                }

                foreach (string filename in filenames)
                {
                    File.Delete(filename);
                }

                DbParameterCollection collection = null;



                collection = this.dataAccess.ExecuteNonQueryReturnOutputParams
                         (
                              this.DB1029ConnectionString,
                              "BCS_GIMSaveSupplementSlink",
                              this.dataAccess.CreateParameter("ZipFileName", SqlDbType.NVarChar, ZIPFileNamewithPath),
                              this.dataAccess.CreateParameter("BCSDocUpdateSupplementsSlinkID", SqlDbType.Int, BCSDocUpdateSupplementsSlinkID, ParameterDirection.Output)
                          );



                if (collection != null)
                {
                    BCSDocUpdateSupplementsSlinkID = (int?)collection["BCSDocUpdateSupplementsSlinkID"].Value;
                }

                if (BCSDocUpdateSupplementsSlinkID != null)
                {
                    bcssupplement.BCSDocUpdateSupplementsSlinkID = BCSDocUpdateSupplementsSlinkID.Value;
                }
                else
                {
                    bcssupplement.BCSDocUpdateSupplementsSlinkID = -1;
                }


            }

            return bcssupplement;

        }


        public void CreateSlinkZipFileForARAndSAR(Slink slink, BCSClient bcsclient, string ThreadNumber, ZipFileCounter zipfilecounter
                                            , string ProsDocTypeID)
        {
            string PDFAndTextIdentifierFileName = Guid.NewGuid().ToString().Replace("-", "");

            string RenamedPDFFileName = string.Empty;

            string RenameToPDFFileName = string.Empty;

            string InstructionsFileName = string.Empty;

            string SLINKInstructions = string.Empty;

            DateTime CurrentDate = DateTime.Now;


            string DestinationPathWithZipFolder = bcsclient.DocUpdateStatusFilesSANPath + @"Zip";

            if (!Directory.Exists(DestinationPathWithZipFolder))
                Directory.CreateDirectory(DestinationPathWithZipFolder);


            DestinationPathWithZipFolder = DestinationPathWithZipFolder + @"\" + CurrentDate.Year;

            if (!Directory.Exists(DestinationPathWithZipFolder))
                Directory.CreateDirectory(DestinationPathWithZipFolder);

            DestinationPathWithZipFolder = DestinationPathWithZipFolder + @"\" + CurrentDate.Month;

            if (!Directory.Exists(DestinationPathWithZipFolder))
                Directory.CreateDirectory(DestinationPathWithZipFolder);

            DestinationPathWithZipFolder = DestinationPathWithZipFolder + @"\" + CurrentDate.Day;

            if (!Directory.Exists(DestinationPathWithZipFolder))
                Directory.CreateDirectory(DestinationPathWithZipFolder);








            if (slink.SlinkExists)
            {
                string StoredProcedureName = "BCS_GetARSARZipFile";

                DataTable GetARSARZipFileDT = this.dataAccess.ExecuteDataTable
               (
                    this.DB1029ConnectionString,
                    StoredProcedureName,
                     this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                     this.dataAccess.CreateParameter(DBCZipFilePath, SqlDbType.NVarChar, DestinationPathWithZipFolder)
                );



                if (GetARSARZipFileDT.Rows.Count > 0)
                {
                    bcsclient.BCSDocUpdateARSARSlinkID = GetARSARZipFileDT.Rows[0].Field<int?>("BCSDocUpdateARSARSlinkID");
                    return;
                }
                else
                {
                    slink.SlinkExists = false;
                }

            }



            if (!slink.SlinkExists)
            {
                if (bcsclient.preflightjob == null) // Get Job Details the first time you need to create a SLINK for a particular day.
                {
                    bcsclient.preflightjob = GetJobDetailsFromPreFlightService(bcsclient);
                }

                PreFlightJob preflightjob = bcsclient.preflightjob;


                zipfilecounter.Counter += 1;

                string ZIPFileNamewithPath = DestinationPathWithZipFolder + @"\" + bcsclient.ClientPrefix + "_SLINK_" + CurrentDate.ToString("yyyyMMddHHmm") + "_" + ThreadNumber + zipfilecounter.Counter + ".zip";



                InstructionsFileName = ProsDocTypeID + "_" + PDFAndTextIdentifierFileName + ".txt";

                RenamedPDFFileName = ProsDocTypeID + "_" + PDFAndTextIdentifierFileName + ".pdf";

                RenameToPDFFileName = ProsDocTypeID + slink.SlinkNameWithoutPdf + bcsclient.ClientPrefix + ".pdf";

                string SlinkInstructionsHeaderLine = "H|" + preflightjob.ClientID + "|"
                                                        + preflightjob.DealNumber + "|"
                                                        + preflightjob.JobNumber + "|"
                                                        + preflightjob.CycleNumber + "|";


                SLINKInstructions = "Goal: Preflight and Rename " + RenamedPDFFileName
                                        + ". Rename the document to " + RenameToPDFFileName + ".~ ~";

                SLINKInstructions = SLINKInstructions + " ";

                SlinkInstructionsHeaderLine += "0|" + "0|" + RenamedPDFFileName + "|" + RenameToPDFFileName;

                string strInstructionFile = DestinationPathWithZipFolder + @"\" + InstructionsFileName;

                string strPDFFileToZip = DestinationPathWithZipFolder + @"\" + RenamedPDFFileName;

                using (StreamWriter tws = new StreamWriter(strInstructionFile, true))
                {
                    tws.WriteLine(SlinkInstructionsHeaderLine); //add the header line first.

                    if (SLINKInstructions.Length > 0)
                    {
                        foreach (string strLine in SLINKInstructions.Split('~'))
                        {
                            tws.WriteLine(strLine);
                        }
                    }
                    tws.Close();
                }

                File.Copy(slink.SlinkNameWithFilePath, strPDFFileToZip);


                List<string> filenames = new List<string>();
                filenames.Add(strInstructionFile);
                filenames.Add(strPDFFileToZip);




                using (ZipOutputStream s = new ZipOutputStream(File.Create(ZIPFileNamewithPath)))
                {

                    s.SetLevel(9); // 0-9, 9 being the highest compression

                    byte[] buffer = new byte[4096];

                    foreach (string file in filenames)
                    {

                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                        entry.DateTime = DateTime.Now;
                        entry.Size = new FileInfo(file).Length;
                        s.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file))
                        {

                            int sourceBytes;

                            do
                            {

                                sourceBytes = fs.Read(buffer, 0, buffer.Length);

                                s.Write(buffer, 0, sourceBytes);

                            }

                            while (sourceBytes > 0);

                        }

                    }

                    s.Finish();

                    s.Close();

                }

                foreach (string filename in filenames)
                {
                    File.Delete(filename);
                }

                DbParameterCollection collection = null;



                collection = this.dataAccess.ExecuteNonQueryReturnOutputParams
                         (
                              this.DB1029ConnectionString,
                              "BCS_GIMSaveARSARSlink",
                              this.dataAccess.CreateParameter("ZipFileName", SqlDbType.NVarChar, ZIPFileNamewithPath),
                              this.dataAccess.CreateParameter("BCSDocUpdateARSARSlinkID", SqlDbType.Int, bcsclient.BCSDocUpdateARSARSlinkID, ParameterDirection.Output)
                          );



                if (collection != null)
                {
                    bcsclient.BCSDocUpdateARSARSlinkID = (int?)collection["BCSDocUpdateARSARSlinkID"].Value;
                }



            }


        }

        public string CreateSlinkZipFile(Slink slink, BCSClient bcsclient, string ThreadNumber, ZipFileCounter zipfilecounter
                                            , string ProsDocTypeID, int DocUpdateID)
        {
            string PDFAndTextIdentifierFileName = Guid.NewGuid().ToString().Replace("-", "");

            string RenamedPDFFileName = string.Empty;

            string RenameToPDFFileName = string.Empty;

            string InstructionsFileName = string.Empty;

            string SLINKInstructions = string.Empty;

            DateTime CurrentDate = DateTime.Now;


            string DestinationPathWithZipFolder = bcsclient.DocUpdateStatusFilesSANPath + @"Zip";

            if (!Directory.Exists(DestinationPathWithZipFolder))
                Directory.CreateDirectory(DestinationPathWithZipFolder);


            DestinationPathWithZipFolder = DestinationPathWithZipFolder + @"\" + CurrentDate.Year;

            if (!Directory.Exists(DestinationPathWithZipFolder))
                Directory.CreateDirectory(DestinationPathWithZipFolder);

            DestinationPathWithZipFolder = DestinationPathWithZipFolder + @"\" + CurrentDate.Month;

            if (!Directory.Exists(DestinationPathWithZipFolder))
                Directory.CreateDirectory(DestinationPathWithZipFolder);

            DestinationPathWithZipFolder = DestinationPathWithZipFolder + @"\" + CurrentDate.Day;

            if (!Directory.Exists(DestinationPathWithZipFolder))
                Directory.CreateDirectory(DestinationPathWithZipFolder);








            if (slink.SlinkExists)
            {
                string StoredProcedureName = "BCS_" + bcsclient.ClientPrefix + "GetSlinkZipFileForTodaysDate";

                Object ZipFileName = this.dataAccess.ExecuteScalar
               (
                    this.DB1029ConnectionString,
                    StoredProcedureName,
                     this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                     this.dataAccess.CreateParameter(DBCZipFilePath, SqlDbType.NVarChar, DestinationPathWithZipFolder),
                     this.dataAccess.CreateParameter(DBCBCSDocUpdateID, SqlDbType.NVarChar, DocUpdateID)
                );

                string ZipFileNameToString = ZipFileName.ToString();

                if (ZipFileNameToString != "")
                    return ZipFileNameToString;
                else
                {
                    slink.SlinkExists = false;
                }

            }



            if (!slink.SlinkExists)
            {
                if (bcsclient.preflightjob == null) // Get Job Details the first time you need to create a SLINK for a particular day.
                {
                    bcsclient.preflightjob = GetJobDetailsFromPreFlightService(bcsclient);
                }

                PreFlightJob preflightjob = bcsclient.preflightjob;


                zipfilecounter.Counter += 1;

                string ZIPFileNamewithPath = DestinationPathWithZipFolder + @"\" + bcsclient.ClientPrefix + "_SLINK_" + CurrentDate.ToString("yyyyMMddHHmm") + "_" + ThreadNumber + zipfilecounter.Counter + ".zip";



                InstructionsFileName = ProsDocTypeID + "_" + PDFAndTextIdentifierFileName + ".txt";

                RenamedPDFFileName = ProsDocTypeID + "_" + PDFAndTextIdentifierFileName + ".pdf";

                RenameToPDFFileName = ProsDocTypeID + slink.SlinkNameWithoutPdf + bcsclient.ClientPrefix + ".pdf";

                string SlinkInstructionsHeaderLine = "H|" + preflightjob.ClientID + "|"
                                                        + preflightjob.DealNumber + "|"
                                                        + preflightjob.JobNumber + "|"
                                                        + preflightjob.CycleNumber + "|";


                SLINKInstructions = "Goal: Preflight and Rename " + RenamedPDFFileName
                                        + ". Rename the document to " + RenameToPDFFileName + ".~ ~";

                SLINKInstructions = SLINKInstructions + " ";

                SlinkInstructionsHeaderLine += "0|" + "0|" + RenamedPDFFileName + "|" + RenameToPDFFileName;

                string strInstructionFile = DestinationPathWithZipFolder + @"\" + InstructionsFileName;

                string strPDFFileToZip = DestinationPathWithZipFolder + @"\" + RenamedPDFFileName;

                using (StreamWriter tws = new StreamWriter(strInstructionFile, true))
                {
                    tws.WriteLine(SlinkInstructionsHeaderLine); //add the header line first.

                    if (SLINKInstructions.Length > 0)
                    {
                        foreach (string strLine in SLINKInstructions.Split('~'))
                        {
                            tws.WriteLine(strLine);
                        }
                    }
                    tws.Close();
                }

                File.Copy(slink.SlinkNameWithFilePath, strPDFFileToZip);


                List<string> filenames = new List<string>();
                filenames.Add(strInstructionFile);
                filenames.Add(strPDFFileToZip);




                using (ZipOutputStream s = new ZipOutputStream(File.Create(ZIPFileNamewithPath)))
                {

                    s.SetLevel(9); // 0-9, 9 being the highest compression

                    byte[] buffer = new byte[4096];

                    foreach (string file in filenames)
                    {

                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                        entry.DateTime = DateTime.Now;
                        entry.Size = new FileInfo(file).Length;
                        s.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file))
                        {

                            int sourceBytes;

                            do
                            {

                                sourceBytes = fs.Read(buffer, 0, buffer.Length);

                                s.Write(buffer, 0, sourceBytes);

                            }

                            while (sourceBytes > 0);

                        }

                    }

                    s.Finish();

                    s.Close();

                }

                foreach (string filename in filenames)
                {
                    File.Delete(filename);
                }

                return ZIPFileNamewithPath;

            }
            else
                return "";





        }


        public PreFlightJob GetJobDetailsFromPreFlightService(BCSClient bcsclient)
        {
            PreFlightJob preflightjob = null;

            try
            {
                CustomerDealJobCycle[] customerdealjobcycles = null;

                bool LinkFailed = false;

                string LinkFailedErrorMessage = string.Empty;

                try
                {
                    customerdealjobcycles = new PreflightLinkWCFClient().GetCustomerDealJobCycle(bcsclient.ClientPrefix);
                }
                catch (Exception exception)
                {
                    LinkFailed = true;

                    LinkFailedErrorMessage = "LINK webservice deal/job/cycle is unavailable. " + "Exception: " + exception.Message;
                }

                if (customerdealjobcycles != null && customerdealjobcycles.Length > 0)
                {
                    preflightjob = new PreFlightJob();

                    CustomerDealJobCycle customerdealjobcycle = customerdealjobcycles[0];

                    preflightjob.ClientID = bcsclient.ClientPrefix;

                    preflightjob.JobNumber = customerdealjobcycle.JobNumber.ToString();

                    preflightjob.CycleNumber = customerdealjobcycle.CycleNumber.ToString();

                    preflightjob.DealNumber = customerdealjobcycle.DealNumber.ToString();

                    this.dataAccess.ExecuteNonQuery
                      (
                           this.DB1029ConnectionString,
                           SPPreflightUpdatesFromLink,
                           this.dataAccess.CreateParameter(DBCClientID, SqlDbType.NVarChar, bcsclient.ClientPrefix),
                           this.dataAccess.CreateParameter(DBCDealNumber, SqlDbType.NVarChar, preflightjob.DealNumber),
                           this.dataAccess.CreateParameter(DBCJobNumber, SqlDbType.NVarChar, preflightjob.JobNumber),
                           this.dataAccess.CreateParameter(DBCCycleNumber, SqlDbType.NVarChar, preflightjob.CycleNumber)
                       );

                }
                else
                {
                    if (!LinkFailed)
                    {
                        LinkFailed = true;
                        LinkFailedErrorMessage = " Unable to retrieve deal/job/cycle for client id "
                                                    + bcsclient.ClientPrefix
                                                    + ". Using “deal/job/cycle/ until this issue is corrected.";
                    }
                    preflightjob = GetJobDetailsFromDB(bcsclient.ClientPrefix);
                }

                if (LinkFailed)
                {
                    (new Logging()).SaveExceptionLog("Job Number for " + bcsclient.ClientPrefix + " does not exists in Link System "
                                    , BCSApplicationName.BCSLinkWebService, true);
                }

            }
            catch (Exception exception)
            {
                preflightjob = GetJobDetailsFromDB(bcsclient.ClientPrefix);

                (new Logging()).SaveExceptionLog("BCS Link System failure for Client Prefix" + bcsclient.ClientPrefix
                                            + " " + exception.ToString()
                                    , BCSApplicationName.BCSLinkWebService, true);

                System.Threading.Thread.Sleep(ErrorSleepTime);
            }

            return preflightjob;

        }

        private PreFlightJob GetJobDetailsFromDB(string Prefix)
        {
            PreFlightJob preflightjob = null;

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPPreFlightDetailsFromDBForClientID,
                    this.dataAccess.CreateParameter(DBCClientID, SqlDbType.NVarChar, Prefix)
                ))
            {


                if (reader.Read())
                {
                    preflightjob = new PreFlightJob();

                    preflightjob.ClientID = Prefix;

                    preflightjob.CycleNumber = reader["CycleNumber"].ToString();

                    preflightjob.DealNumber = reader["DealNumber"].ToString();

                    preflightjob.JobNumber = reader["JobNumber"].ToString();

                }
            }


            return preflightjob;
        }

        public void UploadSlinkForPreflightAndUpdateExportedStatus(BCSClient client)
        {
            string PendingForUploadStoredProcedureName = "BCS_" + client.ClientPrefix + "GetSlinkFileListPendingForUpload";

            string UpdateFTPUploadStatusProcedureName = "BCS_" + client.ClientPrefix + "UpdateFTPUploadedStatus";

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    PendingForUploadStoredProcedureName
                ))
            {


                while (reader.Read())
                {
                    string ZIPFileName = reader["ZIPFileName"].ToString();

                    try
                    {

                        if (UtilityFactory.Upload(ZIPFileName,
                                                client.PreFlightDropFTPPath,
                                                client.PreFlightDropFTPUserName,
                                                client.PreFlightDropFTPPassword
                                                )
                            )
                        {
                            this.dataAccess.ExecuteNonQuery
                         (
                              this.DB1029ConnectionString,
                              UpdateFTPUploadStatusProcedureName,
                              this.dataAccess.CreateParameter(DBCZipFileName, SqlDbType.NVarChar, ZIPFileName),
                              this.dataAccess.CreateParameter(DBCExportedDate, SqlDbType.NVarChar, DateTime.Now)
                          );

                        }

                    }
                    catch (Exception exception)
                    {
                        string ErrorEmailBody = "Error has occured during execution of BCSDocUpdateSlinkIntegrationService - UploadSlinkForPreflightAndUpdateExportedStatus. Error message: "
                            + exception.Message
                            + " File " + ZIPFileName + " upload failed";

                        foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                                ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "support", null);
                        }

                        System.Threading.Thread.Sleep(ErrorSleepTime);
                    }

                }

            }
        }


        public string CreateEmptyPipeRecordsForSupplement(int NumberOfSupplements)
        {
            StringBuilder stringbuilder = new StringBuilder();

            for (int counter = 1; counter <= NumberOfSupplements; counter++)
            {

                stringbuilder.Append("||||");

            }

            return stringbuilder.ToString();


        }

        public List<string> GenerateDocUpdateFile(BCSClient client)
        {
            String GetDocUpdateDetailsStoredProcedure = "BCS_" + client.ClientPrefix + "GetDocUpdateDetails";

            DateTime currentdate = DateTime.Now;

            DateTime yesterdaysdate = currentdate.AddDays(-1);

            if (currentdate.DayOfWeek == DayOfWeek.Tuesday)
            {
                yesterdaysdate = currentdate.AddDays(-3);
            }



            List<string> docupdatefiles = new List<string>();

            string currentdateinstringformat = currentdate.ToString("yyyy/MM/dd  HH:mm:ss");

            currentdateinstringformat = currentdateinstringformat.Replace(" ", "").Replace("/", "").Replace(":", "");

            string NUDocUpdateFileName = client.ClientPrefix + "_NU_" + currentdateinstringformat + ".txt";

            docupdatefiles.Add(NUDocUpdateFileName);

            string IPDocUpdateFileName = client.ClientPrefix + "_IP_" + currentdateinstringformat + ".txt";

            docupdatefiles.Add(IPDocUpdateFileName);


            List<DocUpdate> NUDocUpdates = new List<DocUpdate>();

            List<DocUpdate> IPDocUpdates = new List<DocUpdate>();

            DocUpdate docupdate = null;

            string CUSIP = string.Empty;

            string SECDetails = string.Empty;



            int aPCount = 0;
            int oPCount = 0;
            int EXCount = 0;
            int FLCount = 0;
            int NUCount = 0;
            int NUFLCount = 0;

            int TotalSupplements = 0;

            using (IDataReader reader = this.dataAccess.ExecuteReader
              (
                   this.DB1029ConnectionString,
                   GetDocUpdateDetailsStoredProcedure
               ))
            {
                while (reader.Read())
                {
                    string CurrentCUSIP = reader["CUSIP"].ToString().Trim();

                    string FundName = reader["FundName"].ToString();

                    string PDFName = reader["PDFName"].ToString();

                    string DocumentType = reader["DocumentType"].ToString();


                    PDFName = DocumentType.Replace("SPS", "SP").Replace("RSP", "SP").Replace("PS", "P").Replace("RP", "P") + PDFName;

                    PDFName = PDFName.Replace(".pdf", client.ClientPrefix + ".pdf");


                    DateTime EffectiveDate = Convert.ToDateTime(reader["EffectiveDate"]);

                    DateTime DocumentDate = Convert.ToDateTime(reader["DocumentDate"]);

                    var varRRDInternalDocumentID = reader["RRDInternalDocumentID"];

                    int RRDInternalDocumentID = 0;

                    if (varRRDInternalDocumentID != DBNull.Value)
                        RRDInternalDocumentID = Convert.ToInt32(varRRDInternalDocumentID);

                    string RRDPDFURL = String.Empty;
                    if (client.SendRRDPDFURL)
                        RRDPDFURL = reader["RRDPDFURL"].ToString();

                    bool IsFiled = Convert.ToBoolean(reader["IsFiled"]);

                    DateTime FilingStatusAddedDate = Convert.ToDateTime(reader["FilingStatusAddedDate"]);

                    bool IsExported = Convert.ToBoolean(reader["IsExported"]);

                    var ExportedDate = reader["ExportedDate"];

                    bool IsAPF = Convert.ToBoolean(reader["IsAPF"]);

                    var APFReceivedDate = reader["APFReceivedDate"];

                    bool IsOPF = Convert.ToBoolean(reader["IsOPF"]);

                    var OPFReceivedDate = reader["OPFReceivedDate"];

                    var SECAccNUM = reader["SECAcc#"];

                    var PageCount = reader["PageCount"];

                    var PageSizeHeight = reader["PageSizeHeight"];

                    var PageSizeWidth = reader["PageSizeWidth"];


                    if (CurrentCUSIP != CUSIP)
                    {


                        if (docupdate != null)
                        {
                            docupdate.SECDetails = SECDetails;

                            docupdate.SECDetails += CreateEmptyPipeRecordsForSupplement(11 - TotalSupplements);

                            if (docupdate.RPProcessStep == "NUFL")
                            {
                                docupdate.RPProcessStep = "FL";
                                NUDocUpdates.Add(docupdate);
                            }
                            else
                                if (docupdate.RPProcessStep == "NU")
                                    NUDocUpdates.Add(docupdate);
                                else
                                    IPDocUpdates.Add(docupdate);
                        }

                        TotalSupplements = 0;

                        SECDetails = string.Empty;
                        CUSIP = CurrentCUSIP;

                        docupdate = new DocUpdate();


                        docupdate.CUSIP = CUSIP;
                        docupdate.DocumentType = DocumentType;
                        docupdate.EffectiveDate = EffectiveDate;

                        docupdate.DocumentDate = DocumentDate;

                        //SendSecurityType
                        string SecurityTypeCode = String.Empty;
                        if (client.SendSecurityType)
                        {
                            SecurityTypeCode = Convert.ToString(reader["SecurityTypeCode"]);
                            if (string.IsNullOrWhiteSpace(SecurityTypeCode))
                            {
                                SecurityTypeCode = "NA";
                            }
                        }
                        docupdate.SecurityTypeCode = SecurityTypeCode;
                        //SendSecurityType

                        if (PageCount != DBNull.Value)
                            docupdate.PageCount = Convert.ToInt32(PageCount);

                        if (PageSizeHeight != DBNull.Value)
                        {
                            double value = Convert.ToDouble(PageSizeHeight);
                            if (value > 0)
                            {
                                docupdate.PageSizeHeight = value;
                            }
                        }

                        if (PageSizeWidth != DBNull.Value)
                        {
                            double value = Convert.ToDouble(PageSizeWidth);
                            if (value > 0)
                            {
                                docupdate.PageSizeWidth = value;
                            }
                        }


                        if (IsAPF && (Convert.ToDateTime(APFReceivedDate) >= yesterdaysdate
                                            && Convert.ToDateTime(APFReceivedDate) <= currentdate))
                        {

                            docupdate.RPProcessStep = "aP";
                            aPCount++;
                        }
                        else
                            if (IsOPF && (Convert.ToDateTime(OPFReceivedDate) >= yesterdaysdate
                                            && Convert.ToDateTime(OPFReceivedDate) <= currentdate))
                            {

                                docupdate.RPProcessStep = "oP";
                                oPCount++;
                            }
                            else
                                if (IsExported && (Convert.ToDateTime(ExportedDate) >= yesterdaysdate
                                            && Convert.ToDateTime(ExportedDate) <= currentdate))
                                {
                                    docupdate.RPProcessStep = "EX";
                                    EXCount++;
                                }
                                else
                                    if (IsFiled && (FilingStatusAddedDate >= yesterdaysdate
                                                && FilingStatusAddedDate <= currentdate))
                                    {
                                        docupdate.RPProcessStep = "FL";
                                        FLCount++;
                                    }
                                    else
                                    {
                                        if (IsFiled && !IsExported && !IsAPF && !IsOPF)
                                        {
                                            NUFLCount++;
                                            docupdate.RPProcessStep = "NUFL";
                                        }
                                        else
                                        {
                                            docupdate.RPProcessStep = "NU";
                                            NUCount++;
                                        }
                                    }

                        docupdate.FundName = FundName;

                        if (docupdate.RPProcessStep != "FL" && docupdate.RPProcessStep != "NUFL")
                        {

                            docupdate.PDFName = PDFName;

                            docupdate.RRDInternalDocumentID = RRDInternalDocumentID;

                            docupdate.RRDPDFURL = RRDPDFURL;
                        }



                    }


                    if (SECAccNUM != DBNull.Value)
                    {
                        DateTime SECDateFiled = Convert.ToDateTime(reader["SECDateFiled"]);

                        DateTime SECEffectiveDate = Convert.ToDateTime(reader["SECEffectiveDate"]);

                        string SECFormType = reader["SECFormType"].ToString();

                        if (SECDetails != string.Empty)
                            SECDetails += "|";

                        SECDetails += SECAccNUM.ToString()
                                        + "|" + SECEffectiveDate.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                                        + "|" + SECDateFiled.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                                        + "|" + SECFormType.ToString();
                        ;

                        TotalSupplements++;

                    }

                }


                if (docupdate != null)
                {

                    docupdate.SECDetails = SECDetails;

                    docupdate.SECDetails += CreateEmptyPipeRecordsForSupplement(10 - TotalSupplements);

                    if (docupdate.RPProcessStep == "NUFL")
                    {
                        docupdate.RPProcessStep = "FL";
                        NUDocUpdates.Add(docupdate);
                    }
                    else
                        if (docupdate.RPProcessStep == "NU")
                            NUDocUpdates.Add(docupdate);
                        else
                            IPDocUpdates.Add(docupdate);
                }



                int TotalNURecords = NUFLCount + NUCount;

                int TotalIPRecords = FLCount + EXCount + aPCount + oPCount;


                foreach (string docupdatefile in docupdatefiles)
                {
                    string DocUpdateHeader = string.Empty;

                    string path = client.DocUpdateStatusFilesSANPath + @"DocUpdate\" + docupdatefile;

                    if (client.NeedDocUpdate)
                    {
                        if (!Directory.Exists(client.DocUpdateStatusFilesSANPath + @"IPDocUpdate\" + client.ClientName + @"\"))
                        {
                            Directory.CreateDirectory(client.DocUpdateStatusFilesSANPath + @"IPDocUpdate\" + client.ClientName + @"\");
                        }

                        path = client.DocUpdateStatusFilesSANPath + @"IPDocUpdate\" + client.ClientName + @"\" + docupdatefile;
                    }

                    using (StreamWriter tws = new StreamWriter(path, true))
                    {
                        List<DocUpdate> currentdocupdates = null;
                        if (docupdatefile.StartsWith(client.ClientPrefix + "_NU_"))
                        {
                            currentdocupdates = NUDocUpdates;
                            DocUpdateHeader = "H|NU|BCS|"
                                         + docupdatefile
                                         + "|" + currentdateinstringformat
                                         + "|" + TotalNURecords.ToString()
                                         + "|" + NUFLCount.ToString()
                                         + "|0|0|0"
                                         + "|" + NUCount.ToString();
                        }
                        else
                        {
                            DocUpdateHeader = "H|IP|BCS|"
                                         + docupdatefile
                                         + "|" + currentdateinstringformat
                                         + "|" + TotalIPRecords.ToString()
                                         + "|" + FLCount.ToString()
                                         + "|" + EXCount.ToString()
                                         + "|" + aPCount.ToString()
                                         + "|" + oPCount.ToString()
                                         + "|0";
                            currentdocupdates = IPDocUpdates;
                        }

                        tws.WriteLine(DocUpdateHeader);



                        foreach (DocUpdate enumdocupdate in currentdocupdates)
                        {
                            string DocUpdateRecord = "D|BCS|"
                                                        + client.ClientPrefix
                                                        + "|" + enumdocupdate.RPProcessStep
                                                        + "|" + enumdocupdate.CUSIP
                                                        + "|" + enumdocupdate.FundName
                                                        + "|" + enumdocupdate.PDFName
                                                        + "|" + enumdocupdate.SecurityTypeCode
                                                        + "|" + enumdocupdate.DocumentType
                                                        + "|" + enumdocupdate.EffectiveDate.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                                                        + "|" + enumdocupdate.DocumentDate.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                                                        + "|" + enumdocupdate.PageCount.ToString()
                                                        + "|" + enumdocupdate.PageSizeHeight.ToString()
                                                        + "|" + enumdocupdate.PageSizeWidth.ToString()
                                                        + "|"
                                                        + "|" + (enumdocupdate.RRDInternalDocumentID > 0 ? enumdocupdate.RRDInternalDocumentID.ToString() : "")
                                                        + "|" + enumdocupdate.RRDPDFURL;

                            if (enumdocupdate.SECDetails != "")
                                DocUpdateRecord += "|" + enumdocupdate.SECDetails;

                            tws.WriteLine(DocUpdateRecord);



                            tws.Flush();

                        }

                        tws.Close();
                    }
                }


            }

            return docupdatefiles;
        }

        public string GenerateIPDocUpdateFileUsingCUSIPWatchList(BCSClient client, BCSClientIPFileConfigDetails IPFileConfigDetails)
        {
            String GetIPDocUpdateDetailsStoredProcedure = "BCS_" + client.ClientName + "GetIPDocUpdateDetails";


            string currentdateinstringformat = IPFileConfigDetails.ProcessedDate.ToString("yyyy/MM/dd  HH:mm:ss");

            currentdateinstringformat = currentdateinstringformat.Replace(" ", "").Replace("/", "").Replace(":", "");

            string IPDocUpdateFileName = client.ClientPrefix + "_IP_" + currentdateinstringformat + ".txt";

            if (!string.IsNullOrWhiteSpace(client.IPFileNamePrefix))
            {
                IPDocUpdateFileName = client.IPFileNamePrefix + IPDocUpdateFileName;
            }

            List<DocUpdate> IPDocUpdates = new List<DocUpdate>();

            DocUpdate docupdate = null;


            int APCount = 0;
            int OPCount = 0;
            int EXCount = 0;
            int FLCount = 0;
            int APCCount = 0;
            int OPCCount = 0;

            using (IDataReader reader = this.dataAccess.ExecuteReader
              (
                   this.DB1029ConnectionString,
                   GetIPDocUpdateDetailsStoredProcedure,
                   this.dataAccess.CreateParameter(DBCCurrentdate, SqlDbType.DateTime, IPFileConfigDetails.ProcessedDate),
                   this.dataAccess.CreateParameter(DBCYesterdaysdate, SqlDbType.DateTime, IPFileConfigDetails.LastFileSentDate),
                   this.dataAccess.CreateParameter(DBCIPDocUpdateFileName, SqlDbType.NVarChar, IPDocUpdateFileName),
                   this.dataAccess.CreateParameter(DBCHeaderDate, SqlDbType.NVarChar, currentdateinstringformat)
               ))
            {
                while (reader.Read())
                {
                    string RPProcessStep = reader["RPProcessStep"].ToString();

                    // Do not do anything if RPProcessStep is NULL.  It will come only if isProcessed = 1 and isExported = 0
                    if (string.IsNullOrWhiteSpace(RPProcessStep))
                    {
                        continue;
                    }

                    string CUSIP = reader["CUSIP"].ToString().Trim();

                    string PDFName = reader["PDFName"].ToString();

                    int EdgarID = Convert.ToInt32(reader["EdgarID"]);

                    string DocumentType = reader["DocumentType"].ToString();

                    if (DocumentType == "SPS" || DocumentType == "PS")
                    {
                        PDFName = DocumentType + PDFName.Replace(".pdf", "") + EdgarID;
                    }
                    else
                    {
                        PDFName = DocumentType.Replace("RSP", "SP").Replace("RP", "P").Replace("RAR", "AR").Replace("RSAR", "SAR").Replace("RQR", "QR") + PDFName.Replace(".pdf", "");
                    }

                    PDFName = PDFName + "GIM.pdf";

                    var varRRDInternalDocumentID = reader["RRDInternalDocumentID"];

                    int RRDInternalDocumentID = 0;

                    if (varRRDInternalDocumentID != DBNull.Value)
                        RRDInternalDocumentID = Convert.ToInt32(varRRDInternalDocumentID);

                    var PageCount = reader["PageCount"];
                    var PageSizeHeight = reader["PageSizeHeight"];
                    var PageSizeWidth = reader["PageSizeWidth"];


                    docupdate = new DocUpdate();

                    docupdate.CUSIP = CUSIP;
                    docupdate.FundName = reader["FundName"].ToString();
                    docupdate.DocumentType = DocumentType;
                    docupdate.EffectiveDate = Convert.ToDateTime(reader["EffectiveDate"]);
                    docupdate.DocumentDate = Convert.ToDateTime(reader["DocumentDate"]);
                    docupdate.FilingDate = Convert.ToDateTime(reader["SECDateFiled"]);
                    docupdate.AccNum = Convert.ToString(reader["SECAcc#"]);
                    docupdate.SECFormType = Convert.ToString(reader["SECFormType"]);

                    if (PageCount != DBNull.Value)
                        docupdate.PageCount = Convert.ToInt32(PageCount);

                    if (PageSizeHeight != DBNull.Value)
                    {
                        double value = Convert.ToDouble(PageSizeHeight);
                        if (value > 0)
                        {
                            docupdate.PageSizeHeight = value;
                        }
                    }

                    if (PageSizeWidth != DBNull.Value)
                    {
                        double value = Convert.ToDouble(PageSizeWidth);
                        if (value > 0)
                        {
                            docupdate.PageSizeWidth = value;
                        }
                    }

                    docupdate.RPProcessStep = RPProcessStep;

                    switch (RPProcessStep)
                    {
                        case "AP": APCount++; break;
                        case "OP": OPCount++; break;
                        case "EX": EXCount++; break;
                        case "FL": FLCount++; break;
                        case "APC": APCCount++; break;
                        case "OPC": OPCCount++; break;

                    }

                    if (docupdate.RPProcessStep != "FL")
                    {
                        docupdate.PDFName = PDFName;

                        docupdate.RRDInternalDocumentID = RRDInternalDocumentID;

                        docupdate.RRDExternalDocumentID = PDFName.Replace(".pdf", "");
                    }

                    IPDocUpdates.Add(docupdate);


                }

                int TotalIPRecords = FLCount + EXCount + APCount + OPCount + APCCount + OPCCount;


                string DocUpdateHeader = string.Empty;

                if (!Directory.Exists(client.DocUpdateStatusFilesSANPath + @"IPDocUpdate\" + client.ClientPrefix + @"\"))
                {
                    Directory.CreateDirectory(client.DocUpdateStatusFilesSANPath + @"IPDocUpdate\" + client.ClientPrefix + @"\");
                }

                using (StreamWriter tws = new StreamWriter(client.DocUpdateStatusFilesSANPath + @"IPDocUpdate\" + client.ClientPrefix + @"\" + IPDocUpdateFileName, true))
                {
                    DocUpdateHeader = "H|IP|GIM|"
                                    + IPDocUpdateFileName
                                    + "|" + currentdateinstringformat
                                    + "|" + TotalIPRecords.ToString()
                                    + "|" + FLCount.ToString()
                                    + "|" + EXCount.ToString()
                                    + "|" + APCount.ToString()
                                    + "|" + OPCount.ToString()
                                    + "|" + APCCount.ToString()
                                    + "|" + OPCCount.ToString()
                                    + "|";

                    tws.WriteLine(DocUpdateHeader);


                    foreach (DocUpdate enumdocupdate in IPDocUpdates)
                    {
                        string DocUpdateRecord = "D|GIM|"
                                                    + client.ClientPrefix
                                                    + "|" + enumdocupdate.RPProcessStep
                                                    + "|" + enumdocupdate.CUSIP
                                                    + "|" + enumdocupdate.FundName
                                                    + "|" + enumdocupdate.PDFName
                                                    + "|" + enumdocupdate.RRDExternalDocumentID
                                                    + "|" + enumdocupdate.DocumentType
                                                    + "|" + enumdocupdate.EffectiveDate.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                                                    + "|" + enumdocupdate.DocumentDate.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                                                    + "|" + enumdocupdate.PageCount.ToString()
                                                    + "|" + enumdocupdate.PageSizeHeight.ToString()
                                                    + "|" + enumdocupdate.PageSizeWidth.ToString()
                                                    + "|"
                                                    + "|" + enumdocupdate.RRDInternalDocumentID
                                                    + "|"
                                                    + "|" + enumdocupdate.AccNum
                                                    + "|" + enumdocupdate.EffectiveDate.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                                                    + "|" + enumdocupdate.FilingDate.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                                                    + "|" + enumdocupdate.SECFormType;

                        tws.WriteLine(DocUpdateRecord);

                        tws.Flush();
                    }

                    tws.Close();
                }
            }

            return IPDocUpdateFileName;
        }

        public BCSDailyIPReportData GetDailyIPReportDetails(string clientName, DateTime createdDate, string sortField, string sortOrder)
        {
            BCSDailyIPReportData bcsDailyIPReportData = new BCSDailyIPReportData();
            bcsDailyIPReportData.DetailRecords = new List<BCSDailyIPReportDetailRecords>();

            int APCount = 0;
            int OPCount = 0;
            int EXCount = 0;
            int FLCount = 0;
            int APCCount = 0;
            int OPCCount = 0;
            string IPDocUpdateFileName = string.Empty;
            string HeaderDate = string.Empty;

            using (IDataReader reader = this.dataAccess.ExecuteReader
              (
                   this.DB1029ConnectionString,
                   SPBCSGetDailyIPReportDetails,
                   this.dataAccess.CreateParameter(DBCClientName, SqlDbType.NVarChar, clientName),
                   this.dataAccess.CreateParameter(DBCCreatedDate, SqlDbType.DateTime, createdDate),
                   this.dataAccess.CreateParameter(DBCSortColumn, SqlDbType.NVarChar, sortField),
                   this.dataAccess.CreateParameter(DBCSortOrder, SqlDbType.NVarChar, sortOrder)
               ))
            {
                while (reader.Read())
                {
                    BCSDailyIPReportDetailRecords detailsRecord = new BCSDailyIPReportDetailRecords();
                    detailsRecord.DetailRecordType = "D";
                    detailsRecord.DetailSystem = "GIM";

                    switch (clientName.ToLower())
                    {
                        case "transamerica":
                            detailsRecord.DetailClientID = "AEG";
                            break;
                        case "alliancebernstein":
                            detailsRecord.DetailClientID = "AB";
                            break;
                    }
                    detailsRecord.DetailField15Reserved = string.Empty;
                    detailsRecord.DetailField17Reserved = string.Empty;


                    string RPProcessStep = reader["RPProcessStep"].ToString();

                    // Do not do anything if RPProcessStep is NULL.  It will come only if isProcessed = 1 and isExported = 0
                    if (string.IsNullOrWhiteSpace(RPProcessStep))
                    {
                        continue;
                    }

                    IPDocUpdateFileName = reader["IPDocUpdateFileName"].ToString();
                    HeaderDate = reader["HeaderDate"].ToString();

                    detailsRecord.DetailCUSIPID = reader["CUSIP"].ToString().Trim();

                    string PDFName = reader["PDFName"].ToString();

                    int EdgarID = Convert.ToInt32(reader["EdgarID"]);

                    detailsRecord.DetailDocumentType = reader["DocumentType"].ToString();

                    if (detailsRecord.DetailDocumentType == "SPS" || detailsRecord.DetailDocumentType == "PS")
                    {
                        PDFName = detailsRecord.DetailDocumentType + PDFName.Replace(".pdf", "") + EdgarID;
                    }
                    else
                    {
                        PDFName = detailsRecord.DetailDocumentType.Replace("RSP", "SP").Replace("RP", "P").Replace("RAR", "AR").Replace("RSAR", "SAR").Replace("RQR", "QR") + PDFName.Replace(".pdf", "");
                    }

                    PDFName = PDFName + "GIM.pdf";

                    detailsRecord.DetailFundName = reader["FundName"].ToString();

                    detailsRecord.DetailEffectiveDate = Convert.ToDateTime(reader["EffectiveDate"]).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    detailsRecord.DetailDocumentDate = Convert.ToDateTime(reader["DocumentDate"]).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    detailsRecord.DetailFilingDate = Convert.ToDateTime(reader["SECDateFiled"]).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    detailsRecord.DetailAccessionNum = Convert.ToString(reader["SECAcc#"]);
                    detailsRecord.DetailSECFormType = Convert.ToString(reader["SECFormType"]);


                    var PageCount = reader["PageCount"];
                    var PageSizeHeight = reader["PageSizeHeight"];
                    var PageSizeWidth = reader["PageSizeWidth"];

                    if (PageCount != DBNull.Value)
                        detailsRecord.DetailPageCount = Convert.ToInt32(PageCount);

                    if (PageSizeHeight != DBNull.Value)
                        detailsRecord.DetailPageSizeheight = Convert.ToDouble(PageSizeHeight);

                    if (PageSizeWidth != DBNull.Value)
                        detailsRecord.DetailPageSizeWidth = Convert.ToDouble(PageSizeWidth);

                    detailsRecord.DetailRPProcessStep = RPProcessStep;

                    switch (RPProcessStep)
                    {
                        case "AP": APCount++; break;
                        case "OP": OPCount++; break;
                        case "EX": EXCount++; break;
                        case "FL": FLCount++; break;
                        case "APC": APCCount++; break;
                        case "OPC": OPCCount++; break;

                    }

                    if (detailsRecord.DetailRPProcessStep != "FL")
                    {
                        detailsRecord.DetailPDFName = PDFName;

                        if (reader["RRDInternalDocumentID"] != DBNull.Value)
                            detailsRecord.DetailRRDInternalDocID = Convert.ToInt32(reader["RRDInternalDocumentID"]);

                        detailsRecord.DetailRRDExternalDocID = PDFName.Replace(".pdf", "");
                    }

                    bcsDailyIPReportData.DetailRecords.Add(detailsRecord);

                }


                bcsDailyIPReportData.HeaderRecordType = "H";
                bcsDailyIPReportData.HeaderDataType = "IP";
                bcsDailyIPReportData.HeaderSystem = "GIM";
                bcsDailyIPReportData.HeaderFileName = IPDocUpdateFileName;
                bcsDailyIPReportData.HeaderDateTime = HeaderDate;
                bcsDailyIPReportData.HeaderTotalRecordCount = FLCount + EXCount + APCount + OPCount + APCCount + OPCCount;
                bcsDailyIPReportData.HeaderFLRecordCount = FLCount;
                bcsDailyIPReportData.HeaderEXRecordCount = EXCount;
                bcsDailyIPReportData.HeaderAPRecordCount = APCount;
                bcsDailyIPReportData.HeaderOPRecordCount = OPCount;
                bcsDailyIPReportData.HeaderAPCRecordCount = APCCount;
                bcsDailyIPReportData.HeaderOPCRecordCount = OPCCount;
                bcsDailyIPReportData.HeaderField13Reserved = string.Empty;


            }

            return bcsDailyIPReportData;
        }

        public BCSClient GetGIMClientConfigs()
        {

            return GetALLClientConfig().Find(p => p.ClientPrefix == "GIM");

        }


        public List<BCSClient> GetFLTClientConfigs()
        {
            List<BCSClient> bcsclients = new List<BCSClient>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetFLTClientConfigs
                ))
            {


                while (reader.Read())
                {
                    bcsclients.Add(
                        new BCSClient()
                        {
                            ClientName = reader["ClientName"].ToString(),
                            ClientPrefix = reader["ClientPrefix"].ToString(),
                            ClientDocsFTPPath = reader["ClientDocsFTPPath"].ToString(),
                            ClientDocsFTPUserName = reader["ClientDocsFTPUserName"].ToString(),
                            ClientDocsFTPPassword = reader["ClientDocsFTPPassword"].ToString(),
                            DocUploadWorkingFolderName = reader["DocUploadWorkingFolderName"].ToString(),
                            IsFLTRequired = Convert.ToBoolean(reader["IsFLTRequired"]),
                            FLTodaysTableName = reader["FLTodaysTableName"].ToString(),
                            FLTTomorrowsTableName = reader["FLTTomorrowsTableName"].ToString(),
                            FLTPickupFTPPath = reader["FLTPickupFTPPath"].ToString(),
                            FLTPickupFTPUserName = reader["FLTPickupFTPUserName"].ToString(),
                            FLTPickupFTPPassword = reader["FLTPickupFTPPassword"].ToString(),
                            PreFlightDropFTPPath = reader["PreFlightDropFTPPath"].ToString(),
                            PreFlightDropFTPUserName = reader["PreFlightDropFTPUserName"].ToString(),
                            PreFlightDropFTPPassword = reader["PreFlightDropFTPPassword"].ToString(),
                            DocUpdateMetadataDropFTPPath = reader["DocUpdateMetadataDropFTPPath"].ToString(),
                            DocUpdateMetaDataUserName = reader["DocUpdateMetaDataUserName"].ToString(),
                            DocUpdateMetaDataPassword = reader["DocUpdateMetaDataPassword"].ToString(),
                            PreFlightStatusPickupFTP = reader["PreFlightStatusPickupFTP"].ToString(),
                            PreFlightStatusFTPUserName = reader["PreFlightStatusFTPUserName"].ToString(),
                            PreFlightStatusFTPPassword = reader["PreFlightStatusFTPPassword"].ToString(),
                            DASStatusFTPPickupPath = reader["DASStatusFTPPickupPath"].ToString(),
                            DASStatusFTPUserName = reader["DASStatusFTPUserName"].ToString(),
                            DASStatusFTPPassword = reader["DASStatusFTPPassword"].ToString(),
                            APFFTPPath = reader["APFFTPPath"].ToString(),
                            OPFFTPPath = reader["OPFFTPPath"].ToString(),
                            DocUpdateStatusFilesSANPath = reader["DocUpdateStatusFilesSANPath"].ToString(),
                            FLTArchiveDropPath = reader["FLTArchiveDropPath"].ToString(),
                            FTPDocArchiveDropPath = reader["FTPDocArchiveDropPath"].ToString(),
                        }
                    );


                }

            }

            return bcsclients;
        }


        public List<BCSClient> GetWatchListClientConfigs()
        {
            List<BCSClient> bcsclients = new List<BCSClient>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetWatchListClientConfigs
                ))
            {


                while (reader.Read())
                {
                    bcsclients.Add(
                        new BCSClient()
                        {
                            ClientId = int.Parse(reader["BCSClientConfigId"].ToString()),
                            ClientName = reader["ClientName"].ToString(),
                            ClientPrefix = reader["ClientPrefix"].ToString(),
                            ClientDocsFTPPath = reader["ClientDocsFTPPath"].ToString(),
                            ClientDocsFTPUserName = reader["ClientDocsFTPUserName"].ToString(),
                            ClientDocsFTPPassword = reader["ClientDocsFTPPassword"].ToString(),
                            DocUploadWorkingFolderName = reader["DocUploadWorkingFolderName"].ToString(),
                            IsFLTRequired = Convert.ToBoolean(reader["IsFLTRequired"]),
                            FLTodaysTableName = reader["FLTodaysTableName"].ToString(),
                            FLTTomorrowsTableName = reader["FLTTomorrowsTableName"].ToString(),
                            FLTPickupFTPPath = reader["FLTPickupFTPPath"].ToString(),
                            FLTPickupFTPUserName = reader["FLTPickupFTPUserName"].ToString(),
                            FLTPickupFTPPassword = reader["FLTPickupFTPPassword"].ToString(),
                            PreFlightDropFTPPath = reader["PreFlightDropFTPPath"].ToString(),
                            PreFlightDropFTPUserName = reader["PreFlightDropFTPUserName"].ToString(),
                            PreFlightDropFTPPassword = reader["PreFlightDropFTPPassword"].ToString(),
                            DocUpdateMetadataDropFTPPath = reader["DocUpdateMetadataDropFTPPath"].ToString(),
                            DocUpdateMetaDataUserName = reader["DocUpdateMetaDataUserName"].ToString(),
                            DocUpdateMetaDataPassword = reader["DocUpdateMetaDataPassword"].ToString(),
                            PreFlightStatusPickupFTP = reader["PreFlightStatusPickupFTP"].ToString(),
                            PreFlightStatusFTPUserName = reader["PreFlightStatusFTPUserName"].ToString(),
                            PreFlightStatusFTPPassword = reader["PreFlightStatusFTPPassword"].ToString(),
                            DASStatusFTPPickupPath = reader["DASStatusFTPPickupPath"].ToString(),
                            DASStatusFTPUserName = reader["DASStatusFTPUserName"].ToString(),
                            DASStatusFTPPassword = reader["DASStatusFTPPassword"].ToString(),
                            APFFTPPath = reader["APFFTPPath"].ToString(),
                            OPFFTPPath = reader["OPFFTPPath"].ToString(),
                            DocUpdateStatusFilesSANPath = reader["DocUpdateStatusFilesSANPath"].ToString(),
                            FLTArchiveDropPath = reader["FLTArchiveDropPath"].ToString(),
                            FTPDocArchiveDropPath = reader["FTPDocArchiveDropPath"].ToString(),
                            CUSIPWatchlistPickupFTPLocation = reader["CUSIPWatchlistPickupFTPLocation"].ToString(),
                            CUSIPWatchlistPickupFTPUserName = reader["CUSIPWatchlistPickupFTPUserName"].ToString(),
                            CUSIPWatchlistPickupFTPPassword = reader["CUSIPWatchlistPickupFTPPassword"].ToString(),
                            CUSIPWatchListArchiveDropPath = reader["CUSIPWatchListArchiveDropPath"].ToString()
                        }
                    );


                }

            }

            return bcsclients;
        }


        public List<BCSClient> GetDocUpdateClientConfigs()
        {
            List<BCSClient> bcsclients = new List<BCSClient>();

            //using (IDataReader reader = this.dataAccess.ExecuteReader
            //   (
            //        this.DB1029ConnectionString,
            //        SPGetDocUpdateClientConfigs
            //    ))
            //{


            //    while (reader.Read())
            //    {
            //        bcsclients.Add(
            //            new BCSClient()
            //            {
            //                ClientName = reader["ClientName"].ToString(),
            //                ClientPrefix = reader["ClientPrefix"].ToString(),
            //                ClientDocsFTPPath = reader["ClientDocsFTPPath"].ToString(),
            //                ClientDocsFTPUserName = reader["ClientDocsFTPUserName"].ToString(),
            //                ClientDocsFTPPassword = reader["ClientDocsFTPPassword"].ToString(),
            //                DocUploadWorkingFolderName = reader["DocUploadWorkingFolderName"].ToString(),
            //                IsFLTRequired = Convert.ToBoolean(reader["IsFLTRequired"]),
            //                FLTodaysTableName = reader["FLTodaysTableName"].ToString(),
            //                FLTTomorrowsTableName = reader["FLTTomorrowsTableName"].ToString(),
            //                FLTPickupFTPPath = reader["FLTPickupFTPPath"].ToString(),
            //                FLTPickupFTPUserName = reader["FLTPickupFTPUserName"].ToString(),
            //                FLTPickupFTPPassword = reader["FLTPickupFTPPassword"].ToString(),
            //                PreFlightDropFTPPath = reader["PreFlightDropFTPPath"].ToString(),
            //                PreFlightDropFTPUserName = reader["PreFlightDropFTPUserName"].ToString(),
            //                PreFlightDropFTPPassword = reader["PreFlightDropFTPPassword"].ToString(),
            //                DocUpdateMetadataDropFTPPath = reader["DocUpdateMetadataDropFTPPath"].ToString(),
            //                DocUpdateMetaDataUserName = reader["DocUpdateMetaDataUserName"].ToString(),
            //                DocUpdateMetaDataPassword = reader["DocUpdateMetaDataPassword"].ToString(),
            //                PreFlightStatusPickupFTP = reader["PreFlightStatusPickupFTP"].ToString(),
            //                PreFlightStatusFTPUserName = reader["PreFlightStatusFTPUserName"].ToString(),
            //                PreFlightStatusFTPPassword = reader["PreFlightStatusFTPPassword"].ToString(),
            //                DASStatusFTPPickupPath = reader["DASStatusFTPPickupPath"].ToString(),
            //                DASStatusFTPUserName = reader["DASStatusFTPUserName"].ToString(),
            //                DASStatusFTPPassword = reader["DASStatusFTPPassword"].ToString(),
            //                APFFTPPath = reader["APFFTPPath"].ToString(),
            //                OPFFTPPath = reader["OPFFTPPath"].ToString(),
            //                DocUpdateStatusFilesSANPath = reader["DocUpdateStatusFilesSANPath"].ToString(),
            //                FLTArchiveDropPath = reader["FLTArchiveDropPath"].ToString(),
            //                FTPDocArchiveDropPath = reader["FTPDocArchiveDropPath"].ToString(),
            //                SendRRDPDFURL = Convert.ToBoolean(reader["SendRRDPDFURL"]),
            //                NeedDocUpdate = Convert.ToBoolean(reader["NeedDocUpdate"])
            //            }
            //        );


            //    }

            //}

            bcsclients = GetALLClientConfig().FindAll(p => p.NeedDocUpdate);
            return bcsclients;
        }


        public List<BCSClient> GetALLClientConfig()
        {
            List<BCSClient> bcsclients = new List<BCSClient>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetALLClientConfig
                ))
            {


                while (reader.Read())
                {
                    bcsclients.Add(
                        new BCSClient()
                        {
                            ClientId = int.Parse(reader["BCSClientConfigId"].ToString()),
                            ClientName = reader["ClientName"].ToString(),
                            ClientPrefix = reader["ClientPrefix"].ToString(),
                            ClientDocsFTPPath = reader["ClientDocsFTPPath"].ToString(),
                            ClientDocsFTPUserName = reader["ClientDocsFTPUserName"].ToString(),
                            ClientDocsFTPPassword = reader["ClientDocsFTPPassword"].ToString(),
                            DocUploadWorkingFolderName = reader["DocUploadWorkingFolderName"].ToString(),
                            IsFLTRequired = Convert.ToBoolean(reader["IsFLTRequired"]),
                            FLTodaysTableName = reader["FLTodaysTableName"].ToString(),
                            FLTTomorrowsTableName = reader["FLTTomorrowsTableName"].ToString(),
                            FLTPickupFTPPath = reader["FLTPickupFTPPath"].ToString(),
                            FLTPickupFTPUserName = reader["FLTPickupFTPUserName"].ToString(),
                            FLTPickupFTPPassword = reader["FLTPickupFTPPassword"].ToString(),
                            PreFlightDropFTPPath = reader["PreFlightDropFTPPath"].ToString(),
                            PreFlightDropFTPUserName = reader["PreFlightDropFTPUserName"].ToString(),
                            PreFlightDropFTPPassword = reader["PreFlightDropFTPPassword"].ToString(),
                            DocUpdateMetadataDropFTPPath = reader["DocUpdateMetadataDropFTPPath"].ToString(),
                            DocUpdateMetaDataUserName = reader["DocUpdateMetaDataUserName"].ToString(),
                            DocUpdateMetaDataPassword = reader["DocUpdateMetaDataPassword"].ToString(),
                            PreFlightStatusPickupFTP = reader["PreFlightStatusPickupFTP"].ToString(),
                            PreFlightStatusFTPUserName = reader["PreFlightStatusFTPUserName"].ToString(),
                            PreFlightStatusFTPPassword = reader["PreFlightStatusFTPPassword"].ToString(),
                            DASStatusFTPPickupPath = reader["DASStatusFTPPickupPath"].ToString(),
                            DASStatusFTPUserName = reader["DASStatusFTPUserName"].ToString(),
                            DASStatusFTPPassword = reader["DASStatusFTPPassword"].ToString(),
                            APFFTPPath = reader["APFFTPPath"].ToString(),
                            OPFFTPPath = reader["OPFFTPPath"].ToString(),
                            DocUpdateStatusFilesSANPath = reader["DocUpdateStatusFilesSANPath"].ToString(),
                            FLTArchiveDropPath = reader["FLTArchiveDropPath"].ToString(),
                            FTPDocArchiveDropPath = reader["FTPDocArchiveDropPath"].ToString(),
                            SendRRDPDFURL = Convert.ToBoolean(reader["SendRRDPDFURL"]),
                            NeedDocUpdate = Convert.ToBoolean(reader["NeedDocUpdate"]),
                            SendIPDocUpdate = Convert.ToBoolean(reader["SendIPDocUpdate"]),
                            IsCUSIPWatchListProvided = Convert.ToBoolean(reader["IsCUSIPWatchListProvided"]),
                            CUSIPWatchlistPickupFTPLocation = reader["CUSIPWatchlistPickupFTPLocation"].ToString(),
                            CUSIPWatchlistPickupFTPUserName = reader["CUSIPWatchlistPickupFTPUserName"].ToString(),
                            CUSIPWatchlistPickupFTPPassword = reader["CUSIPWatchlistPickupFTPPassword"].ToString(),
                            CUSIPWatchListArchiveDropPath = reader["CUSIPWatchListArchiveDropPath"].ToString(),
                            IncludeRemovedWatchListCUSIPInIPDocUpdate = Convert.ToBoolean(reader["IncludeRemovedWatchListCUSIPInIPDocUpdate"]),
                            IPDeliveryMethod = reader["IPDeliveryMethod"].ToString(),
                            IPFileNamePrefix = reader["IPFileNamePrefix"].ToString(),
                            ShowClientInDashboard = Convert.ToBoolean(reader["ShowClientInDashboard"]),
                            SendSecurityType = Convert.ToBoolean(reader["SendSecurityType"])
                        }
                    );


                }

            }

            return bcsclients;
        }


        public List<BCSClientIPFileConfigDetails> GetClientsForIPFileToBeSent()
        {
            List<BCSClientIPFileConfigDetails> bcsclients = new List<BCSClientIPFileConfigDetails>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetClientsForIPFileToBeSent
                ))
            {
                while (reader.Read())
                {
                    bcsclients.Add(new BCSClientIPFileConfigDetails
                    {
                        ClientPrefix = reader["ClientPrefix"].ToString(),
                        NeedIPConfirmationFile = Convert.ToBoolean(reader["NeedIPConfirmationFile"]),
                        IPConfirmationFileDropFTPPath = reader["IPConfirmationFileDropFTPPath"].ToString(),
                        IPConfirmationFileDropFTPUserName = reader["IPConfirmationFileDropFTPUserName"].ToString(),
                        IPConfirmationFileDropFTPPassword = reader["IPConfirmationFileDropFTPPassword"].ToString(),
                        LastFileSentDate = Convert.ToDateTime(reader["LastFileSentDate"]),
                        ProcessedDate = Convert.ToDateTime(reader["ProcessedDate"])
                    });
                }

            }

            return bcsclients;
        }

        public void CheckAndprocessPreflightStatusFiles(BCSClient client)
        {

            string UpdateSlinkStatusStoredProcedure = "BCS_" + client.ClientPrefix + "UpdateSLinkStatus";


            List<string> strFiles = UtilityFactory.GetFileList(client.PreFlightStatusPickupFTP
                                                                , client.PreFlightStatusFTPUserName,
                                                                client.PreFlightStatusFTPPassword);

            string PDFDirectDropDirectory = client.DocUpdateStatusFilesSANPath
                                                + @"PreflightStatus\";

            if (!Directory.Exists(PDFDirectDropDirectory))
                Directory.CreateDirectory(PDFDirectDropDirectory);

            foreach (string strFile in strFiles)
            {
                Uri uri = new Uri("ftp://" + client.PreFlightStatusPickupFTP + "/" + strFile);

                try
                {
                    string dropPathWithFileName = PDFDirectDropDirectory + strFile;

                    UtilityFactory.DownloadFileFromFTP(uri, dropPathWithFileName, client.PreFlightStatusFTPUserName,
                                                                client.PreFlightStatusFTPPassword);

                    UtilityFactory.DeleteFTPFileRRDInt(uri, client.PreFlightStatusFTPUserName,
                                                                client.PreFlightStatusFTPPassword);


                    using (StreamReader streamreader = new StreamReader(dropPathWithFileName))
                    {
                        bool IsFirstLine = true;

                        while (streamreader.Peek() >= 0)
                        {
                            string readline = streamreader.ReadLine();
                            if (IsFirstLine)
                            {
                                IsFirstLine = false;
                            }
                            else
                            {
                                string[] record = readline.Split(new char[] { '|' });

                                if (record.Length >= 9)
                                {
                                    string ZipFileName = record[9];



                                    string CompletedDatetime = record[7];

                                    DateTime? completeddatetime = null;



                                    if (CompletedDatetime != "")
                                    {
                                        completeddatetime = DateTime.ParseExact(CompletedDatetime,
                                                                "yyyyMMdd HH:mm:ss",
                                                                CultureInfo.InvariantCulture);

                                        completeddatetime = completeddatetime.Value;
                                    }

                                    string PDFDirectStatus = record[8];

                                    bool PDFDirectFailedPass = false;

                                    if (PDFDirectStatus == "Success")
                                        PDFDirectFailedPass = true;

                                    if (ZipFileName != "")
                                    {

                                        this.dataAccess.ExecuteNonQuery
                     (
                          this.DB1029ConnectionString,
                          UpdateSlinkStatusStoredProcedure,
                          this.dataAccess.CreateParameter(DBCZipFileName, SqlDbType.NVarChar, ZipFileName),
                          this.dataAccess.CreateParameter(DBCCompletedDate, SqlDbType.DateTime, completeddatetime),
                          this.dataAccess.CreateParameter(DBCPassedorFailed, SqlDbType.Bit, PDFDirectFailedPass)

                      );


                                    }
                                }
                            }
                        }


                    }

                }
                catch (Exception exception)
                {

                    string ErrorEmailBody = "Error has occured during Process of Preflight Status File  " + strFile + " For Client " + client.ClientName + " . Error message: "
                                        + exception.Message;

                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null,
                            "DAS Status Processing Exception", ErrorEmailBody, "support", null);

                    System.Threading.Thread.Sleep(ErrorSleepTime);
                }
            }

        }

        public void CheckAndprocessDASStatusFiles(BCSClient client)
        {

            string UpdateSlinkStatusStoredProcedure = "BCS_" + client.ClientPrefix + "UpdateDASStatus";


            List<string> strFiles = UtilityFactory.GetFileList(client.DASStatusFTPPickupPath
                                                                , client.DASStatusFTPUserName,
                                                                client.DASStatusFTPPassword);

            string DASStatusDropDirectory = client.DocUpdateStatusFilesSANPath
                                                + @"DASStatus\";

            if (!Directory.Exists(DASStatusDropDirectory))
                Directory.CreateDirectory(DASStatusDropDirectory);



            foreach (string strFile in strFiles)
            {
                Uri uri = new Uri("ftp://" + client.DASStatusFTPPickupPath + "/" + strFile);

                try
                {
                    string dropPathWithFileName = DASStatusDropDirectory + strFile;

                    UtilityFactory.DownloadFileFromFTP(uri, dropPathWithFileName, client.DASStatusFTPUserName,
                                                                client.DASStatusFTPPassword);

                    UtilityFactory.DeleteFTPFileRRDInt(uri, client.DASStatusFTPUserName,
                                                                client.DASStatusFTPPassword);


                    using (StreamReader streamreader = new StreamReader(dropPathWithFileName))
                    {
                        bool IsFirstLine = true;

                        DateTime? DASStatusReceivedDate = null;

                        while (streamreader.Peek() >= 0)
                        {
                            string readline = streamreader.ReadLine();
                            if (IsFirstLine)
                            {
                                IsFirstLine = false;

                                string[] headerrecord = readline.Split(new char[] { '|' });



                                if (headerrecord[0] == "H")
                                {
                                    string CompletedDatetime = headerrecord[4];

                                    if (CompletedDatetime != "")
                                        DASStatusReceivedDate = DateTime.ParseExact(CompletedDatetime,
                                                                "yyyyMMddHHmmss",
                                                                CultureInfo.InvariantCulture);

                                }
                            }
                            else
                            {
                                string[] record = readline.Split(new char[] { '|' });

                                if (record.Length >= 11)
                                {
                                    string RPProcessStep = record[3];

                                    string CUSIP = record[4];


                                    string PDFName = record[5];

                                    PDFName = PDFName.Replace("SP", "").Replace(client.ClientPrefix, "");

                                    string DocumentType = record[6];

                                    string DASStatus = record[8];

                                    string DASReportingStatus = record[9];

                                    if (DASStatus != "NU") // we don't care about NU status
                                    {
                                        if (PDFName != "")
                                        {

                                            this.dataAccess.ExecuteNonQuery
                                            (
                                                 this.DB1029ConnectionString,
                                                 UpdateSlinkStatusStoredProcedure,
                                                 this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                                                 this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, PDFName),
                                                 this.dataAccess.CreateParameter(DBCDocumentType, SqlDbType.NVarChar, DocumentType),
                                                 this.dataAccess.CreateParameter(DBCRPProcessStep, SqlDbType.NVarChar, RPProcessStep),
                                                 this.dataAccess.CreateParameter(DBCDASStatus, SqlDbType.NVarChar, DASStatus),
                                                 this.dataAccess.CreateParameter(DBCDASReportingStatus, SqlDbType.NVarChar, DASReportingStatus),
                                                 this.dataAccess.CreateParameter(DBCDASStatusReceivedDate, SqlDbType.DateTime, DASStatusReceivedDate)

                                             );

                                        }
                                    }
                                }
                            }
                        }


                    }

                }
                catch (Exception exception)
                {
                    string ErrorEmailBody = "Error has occured during Process of DAS Status File  " + strFile + " For Client " + client.ClientName + ". Error message: "
                                        + exception.Message;

                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null,
                            "DAS Status Processing Exception", ErrorEmailBody, "support", null);

                    System.Threading.Thread.Sleep(ErrorSleepTime);
                }
            }

        }


        public void DequeueAndProcessURLToDownloadJob(int ThreadNumber)
        {
            ZipFileCounter zipfilecounter = new ZipFileCounter();

            zipfilecounter.Counter = 0;

            BCSClient bcsclient = null;

            int SleepTimeAfterResettingClients = Convert.ToInt32(ConfigValues.SleepTimeAfterResettingClients);

            TimeSpan TimeFromResetClients = System.TimeSpan.Parse(ConfigValues.TimeFromResetClients);

            TimeSpan TimeToResetClients = System.TimeSpan.Parse(ConfigValues.TimeToResetClients);



            while (true)
            {
                try
                {



                    DateTime currenttime = DateTime.Now;

                    if (currenttime.TimeOfDay >= TimeFromResetClients && currenttime.TimeOfDay <= TimeToResetClients)
                    {
                        zipfilecounter.Counter = 0;

                        if (bcsclient != null)
                            bcsclient = null;

                        GetMasterDBAndUpdateInWorkflowDB();

                        System.Threading.Thread.Sleep(SleepTimeAfterResettingClients);
                    }


                    DequeueAndProcessURLToDownload(ThreadNumber.ToString(), zipfilecounter, ref bcsclient);

                    System.Threading.Thread.Sleep(Convert.ToInt32(ConfigValues.TimerInterval));


                }
                catch (Exception exception)
                {
                    string ErrorEmailBody = "Error has occured during execution of DequeueAndProcessURLToDownloadJob.Thread number" + ThreadNumber.ToString() + ". Error message: "
                        + exception.Message;

                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                            "DequeueAndProcessURLToDownloadJob Exception", ErrorEmailBody, "support", null);
                    }

                    System.Threading.Thread.Sleep(ErrorSleepTime);
                }
            }

        }

        public void CompareProsDocURLwithDownloadedSLINKFiles()
        {
            StringBuilder sb = new StringBuilder();

            using (IDataReader reader = this.dataAccess.ExecuteReader
              (
                   this.DB1029ConnectionString,
                   SPGetTodaysDistinctBCSPDFURLs
               ))
            {
                int counter = 0;



                while (reader.Read())
                {
                    string Prosdocurl = reader["Prosdocurl"].ToString();
                    counter++;
                    if (Prosdocurl.ToLower().StartsWith(ConfigValues.RPSourceURLReplace))
                    {
                        string SANPath = Prosdocurl.Replace(ConfigValues.RPSourceURLReplace, ConfigValues.RPDestinationSANReplace)
                            .Replace(@"/", @"\");


                        File.Copy(SANPath, ConfigValues.SourceFileWithPathToCompare, true);

                        string SourceMD5Hash = UtilityFactory.GetMD5HashFromFile(ConfigValues.SourceFileWithPathToCompare);

                        string RRDPDFURL = reader["RRDPDFURL"].ToString();

                        string DestinationSANPath = RRDPDFURL.Replace(ConfigValues.RPSourceURLReplace, ConfigValues.RPDestinationSANReplace)
                            .Replace(@"/", @"\");

                        File.Copy(DestinationSANPath, ConfigValues.DestinationFileWithPathToCompare, true);

                        string DestinationMD5Hash = UtilityFactory.GetMD5HashFromFile(ConfigValues.DestinationFileWithPathToCompare);

                        if (SourceMD5Hash != DestinationMD5Hash)
                        {
                            sb.Append(Prosdocurl + " not equal " + RRDPDFURL);
                        }

                    }


                }

                string finallist = sb.ToString();

                sb = null;

                if (finallist == "")
                {
                    finallist = "No records with Comparison Issues.";
                }

                UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.ConfirmationEmailListTo,
                                           "", "",
                                           "BCS Compare Pros Doc URL Check",
                                           finallist,
                                           "", "");


            }

        }


        public void ArchiveCompletedBCSDocSynchronizerQueues()
        {

            int SleepTimeAfterResettingClients = Convert.ToInt32(ConfigValues.SleepTimeAfterResettingClients);

            TimeSpan TimeFromResetClients = System.TimeSpan.Parse(ConfigValues.TimeFromResetClients);

            TimeSpan TimeToResetClients = System.TimeSpan.Parse(ConfigValues.TimeToResetClients);


            while (true)
            {
                try
                {
                    DateTime currenttime = DateTime.Now;

                    if (currenttime.TimeOfDay >= TimeFromResetClients && currenttime.TimeOfDay <= TimeToResetClients)
                    {


                        using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetBCSSynchronizerRecordsToArchive
                ))
                        {


                            while (reader.Read())
                            {
                                int BCSURLDownloadQueueID = Convert.ToInt32(reader["BCSURLDownloadQueueID"]);

                                this.dataAccess.ExecuteNonQuery
                                  (
                                       this.DB1029ConnectionString,
                                       SPSaveBCSSynchronizerArchive,
                                       this.dataAccess.CreateParameter(DBCBCSURLDownloadQueueID, SqlDbType.Int, BCSURLDownloadQueueID)
                                   );

                            }
                        }

                        System.Threading.Thread.Sleep(SleepTimeAfterResettingClients);
                    }




                    System.Threading.Thread.Sleep(Convert.ToInt32(ConfigValues.TimerInterval));
                }
                catch (Exception exception)
                {
                    string ErrorEmailBody = "Error has occured during execution of ArchiveCompletedBCSDocSynchronizerQueues." + ". Error message: "
                        + exception.Message;

                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                            "ArchiveCompletedBCSDocSynchronizerQueues Exception", ErrorEmailBody, "support", null);
                    }

                    System.Threading.Thread.Sleep(ErrorSleepTime);
                }
            }
        }

        public void DequeueAndProcessURLToDownload(string ThreadNumber, ZipFileCounter zipfilecounter, ref BCSClient bcsclient)
        {


            string CurrentUrlToDownload = string.Empty;



            Slink slink = null;

            int CurrentBCSURLDownloadQueueID = -1;

            int CurrentProsID = -1;

            string CurrentProsDocTypeID = string.Empty;

            bool IsErrored = false;


            DataTable SECDetails = null;

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPDequeueURLDownloadQueue
                ))
            {


                while (reader.Read())
                {


                    int BCSURLDownloadQueueID = Convert.ToInt32(reader["BCSURLDownloadQueueID"]);



                    if (CurrentBCSURLDownloadQueueID == -1)
                        CurrentBCSURLDownloadQueueID = BCSURLDownloadQueueID;
                    else
                        if (CurrentBCSURLDownloadQueueID != BCSURLDownloadQueueID)
                        {
                            IsErrored = false;
                            this.dataAccess.ExecuteNonQuery
                                 (
                                      this.DB1029ConnectionString,
                                      SPUpdateURLToDownloadStatus,
                                      this.dataAccess.CreateParameter(DBCBCSURLDownloadQueueID, SqlDbType.Int, CurrentBCSURLDownloadQueueID),
                                      this.dataAccess.CreateParameter(DBCIsDownloaded, SqlDbType.Bit, 1)
                                  );

                            CurrentBCSURLDownloadQueueID = BCSURLDownloadQueueID;
                        }


                    if (bcsclient == null) // First time there is some activity for the day.get the clients.
                        bcsclient = GetGIMClientConfigs();

                    string URLToDownload = reader["URLToDownload"].ToString();

                    int ProsID = Convert.ToInt32(reader["ProsID"]);

                    int TickerID = Convert.ToInt32(reader["TickerID"]);

                    int EdgarID = Convert.ToInt32(reader["EdgarID"]);

                    int ProsDocID = Convert.ToInt32(reader["ProsDocID"]);

                    string CUSIP = reader["CUSIP"].ToString();

                    string FundName = reader["FundName"].ToString();

                    string ProsDocTypeId = reader["ProsDocTypeId"].ToString();

                    string DocumentType = reader["DocumentType"].ToString();

                    bool IsWatchListCUSIP = Convert.ToBoolean(reader["IsWatchListCUSIP"]);

                    bool IsFLWatchListCUSIP = Convert.ToBoolean(reader["IsFLWatchListCUSIP"]);

                    if (!IsWatchListCUSIP)
                    {
                        IsWatchListCUSIP = IsFLWatchListCUSIP;
                    }

                    DateTime ProcessedDate = Convert.ToDateTime(reader["ProcessedDate"]);

                    try
                    {
                        if (ProsDocTypeId != "AR" && ProsDocTypeId != "SAR" && ProsDocTypeId != "QR")
                        {

                            if (CurrentProsID != ProsID || SECDetails == null || CurrentProsDocTypeID != ProsDocTypeId)
                            {
                                SECDetails = null;

                                SECDetails = GetSECDetailsForProsID(ProsID, ProsDocTypeId);

                                CurrentProsID = ProsID;

                                if (CurrentProsDocTypeID != ProsDocTypeId)
                                {
                                    bcsclient.CurrentZipFileName = string.Empty;

                                    bcsclient.BCSDocUpdateARSARSlinkID = null;

                                    CurrentProsDocTypeID = ProsDocTypeId;

                                }

                            }

                            if (SECDetails != null && SECDetails.Rows.Count > 0) // Process only if there are SEC Documents.
                            {

                                int BCSDocUpdateID = 0;

                                bool IsReprocessed = false;


                                if (CurrentUrlToDownload != URLToDownload || (!slink.SlinkExists))
                                {
                                    CurrentUrlToDownload = URLToDownload;

                                    slink = CreateSLINKForURL(CurrentUrlToDownload, ThreadNumber);


                                    bcsclient.CurrentZipFileName = string.Empty;




                                }

                                DbParameterCollection collection = null;

                                collection = this.dataAccess.ExecuteNonQueryReturnOutputParams
                                         (
                                              this.DB1029ConnectionString,
                                              SPSaveDocUpdateSLinkAndSECDetails,
                                              this.dataAccess.CreateParameter(DBCSECDetails, SqlDbType.Structured, SECDetails),
                                              this.dataAccess.CreateParameter(DBCRRDPDFURL, SqlDbType.NVarChar, slink.PDFURL),
                                              this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                                              this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                                              this.dataAccess.CreateParameter(DBCTickerID, SqlDbType.Int, TickerID),
                                              this.dataAccess.CreateParameter(DBCProsID, SqlDbType.Int, ProsID),
                                              this.dataAccess.CreateParameter(DBCEdgarID, SqlDbType.Int, EdgarID),
                                              this.dataAccess.CreateParameter(DBCFundName, SqlDbType.NVarChar, FundName),
                                              this.dataAccess.CreateParameter(DBCProsDocID, SqlDbType.Int, ProsDocID),
                                              this.dataAccess.CreateParameter(DBCProcessedDate, SqlDbType.DateTime, ProcessedDate),
                                              this.dataAccess.CreateParameter(DBCBCSDocUpdateID, SqlDbType.Int, BCSDocUpdateID, ParameterDirection.Output),
                                              this.dataAccess.CreateParameter(DBCIsReprocessed, SqlDbType.Bit, IsReprocessed, ParameterDirection.Output)
                                          );

                                BCSDocUpdateID = -1;

                                if (collection != null)
                                {
                                    if (collection["BCSDocUpdateID"].Value != DBNull.Value)
                                    {
                                        BCSDocUpdateID = Convert.ToInt32(collection["BCSDocUpdateID"].Value);
                                    }

                                    if (collection["IsReprocessed"].Value != DBNull.Value)
                                    {
                                        IsReprocessed = Convert.ToBoolean(collection["IsReprocessed"].Value);
                                    }

                                }






                                string StoredProcedureForSLINK = "BCS_" + bcsclient.ClientPrefix + "SaveSlinkZipDetails";


                                bool CreateSlink = false;


                                if (IsReprocessed || BCSDocUpdateID == -1) //Is Reprocessed or If Prospsectus was passed and Summary already Exists in BCS Doc Update then no need to go to SLINK ZIP creation process.
                                {
                                    CreateSlink = false;
                                }
                                else
                                    CreateSlink = true; // GIM looks at universe so slink zip needs to be created


                                if (CreateSlink)
                                {
                                    try
                                    {
                                        if (bcsclient.CurrentZipFileName == string.Empty && BCSDocUpdateID != -1)
                                        {
                                            bcsclient.CurrentZipFileName = CreateSlinkZipFile(slink, bcsclient,
                                                                            ThreadNumber, zipfilecounter,
                                                                            ProsDocTypeId, BCSDocUpdateID);
                                        }

                                        this.dataAccess.ExecuteNonQuery
                                     (
                                          this.DB1029ConnectionString,
                                          StoredProcedureForSLINK,
                                          this.dataAccess.CreateParameter(DBCBCSDocUpdateID, SqlDbType.Int, BCSDocUpdateID),
                                          this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                                          this.dataAccess.CreateParameter(DBCZipFileName, SqlDbType.NVarChar, bcsclient.CurrentZipFileName),
                                          this.dataAccess.CreateParameter(DBCIsSlinkExists, SqlDbType.Bit, slink.SlinkExists)
                                      );

                                        if (IsWatchListCUSIP && !DocumentType.Contains("Supplement"))
                                        {

                                            this.dataAccess.ExecuteNonQuery
                                            (
                                                 this.DB1029ConnectionString,
                                                 "BCS_GIMSaveBaseWatchListDocument",
                                                 this.dataAccess.CreateParameter(DBCEdgarID, SqlDbType.Int, EdgarID),
                                                 this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                                                 this.dataAccess.CreateParameter(DBCDocumentType, SqlDbType.NVarChar, UtilityFactory.ConvertDocumentType(DocumentType)),
                                                 this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                                                 this.dataAccess.CreateParameter(DBCTickerID, SqlDbType.Int, TickerID),
                                                 this.dataAccess.CreateParameter(DBCProsID, SqlDbType.Int, ProsID),
                                                 this.dataAccess.CreateParameter(DBCFundName, SqlDbType.NVarChar, FundName),
                                                 this.dataAccess.CreateParameter(DBCProsDocID, SqlDbType.Int, ProsDocID),
                                                 this.dataAccess.CreateParameter(DBCProcessedDate, SqlDbType.DateTime, ProcessedDate),
                                                 this.dataAccess.CreateParameter(DBCZipFileName, SqlDbType.NVarChar, bcsclient.CurrentZipFileName)

                                             );
                                        }

                                    }
                                    catch (Exception createzipexception)
                                    {
                                        IsErrored = true;

                                        //send and email

                                        string ErrorEmailBody = "Error has occured during Creating Zip for client " + bcsclient.ClientName
                                                + "  Dequeue and Processessing of URL " + URLToDownload
                                                + " QueueID is " + BCSURLDownloadQueueID.ToString() +
                                                @" Under DequeueAndProcessURLToDownloadJob.Thread number" + ThreadNumber.ToString() + ". Error message: "
                                         + createzipexception.Message;

                                        IsErrored = true;


                                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null,
                                                "DequeueAndProcessURLToDownloadJob Create Zip Exception", ErrorEmailBody, "support", null);


                                        System.Threading.Thread.Sleep(ErrorSleepTime);

                                    }



                                }

                                if (IsWatchListCUSIP && DocumentType.Contains("Supplement"))
                                {
                                    string SupplementDocumentType = DocumentType.Contains("Summary") ? "SPS" : "PS";

                                    BCSSupplement bcssupplement = CreateSlinkZipFileForSupplement(slink,
                                                                        bcsclient,
                                                                        ThreadNumber,
                                                                        zipfilecounter,
                                                                        SupplementDocumentType,
                                                                        EdgarID,
                                                                        ProsID
                                                                        );

                                    if (bcssupplement != null)
                                    {

                                        this.dataAccess.ExecuteNonQuery
                            (
                                 this.DB1029ConnectionString,
                                 "BCS_GIMSaveSupplementInformation",
                                 this.dataAccess.CreateParameter(DBCEdgarID, SqlDbType.Int, EdgarID),
                                 this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                                 this.dataAccess.CreateParameter(DBCDocumentType, SqlDbType.NVarChar, SupplementDocumentType),
                                 this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                                 this.dataAccess.CreateParameter(DBCTickerID, SqlDbType.Int, TickerID),
                                 this.dataAccess.CreateParameter(DBCProsID, SqlDbType.Int, ProsID),
                                 this.dataAccess.CreateParameter(DBCFundName, SqlDbType.NVarChar, FundName),
                                 this.dataAccess.CreateParameter(DBCProsDocID, SqlDbType.Int, ProsDocID),
                                 this.dataAccess.CreateParameter(DBCProcessedDate, SqlDbType.DateTime, ProcessedDate),
                                 this.dataAccess.CreateParameter("PageCount", SqlDbType.Int, bcssupplement.PageCount),
                                 this.dataAccess.CreateParameter("BCSDocUpdateSupplementsSlinkID", SqlDbType.Int, bcssupplement.BCSDocUpdateSupplementsSlinkID)

                             );
                                    }

                                }


                                slink.SlinkExists = true;


                            }
                        }
                        else // AR and SAR
                        {
                            if (IsWatchListCUSIP) // only create SLINK for cusips in watchlist.
                            {
                                if (CurrentProsDocTypeID != ProsDocTypeId)
                                {
                                    bcsclient.CurrentZipFileName = string.Empty;

                                    bcsclient.BCSDocUpdateARSARSlinkID = null;

                                    CurrentProsDocTypeID = ProsDocTypeId;

                                }

                                if (CurrentProsID != ProsID)
                                {
                                    CurrentProsID = ProsID;
                                }

                                if (CurrentUrlToDownload != URLToDownload || (!slink.SlinkExists))
                                {
                                    CurrentUrlToDownload = URLToDownload;

                                    slink = CreateSLINKForURL(CurrentUrlToDownload, ThreadNumber);

                                    bcsclient.CurrentZipFileName = string.Empty;

                                    bcsclient.BCSDocUpdateARSARSlinkID = null;

                                    CreateSlinkZipFileForARAndSAR(slink,
                                            bcsclient,
                                            ThreadNumber,
                                            zipfilecounter,
                                            ProsDocTypeId);

                                }

                                if (bcsclient.BCSDocUpdateARSARSlinkID != null)
                                {

                                    this.dataAccess.ExecuteNonQuery
                        (
                             this.DB1029ConnectionString,
                             "BCS_GIMSaveARSAR",
                             this.dataAccess.CreateParameter(DBCEdgarID, SqlDbType.Int, EdgarID),
                             this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                             this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                             this.dataAccess.CreateParameter(DBCTickerID, SqlDbType.Int, TickerID),
                             this.dataAccess.CreateParameter(DBCProsID, SqlDbType.Int, ProsID),
                             this.dataAccess.CreateParameter(DBCFundName, SqlDbType.NVarChar, FundName),
                             this.dataAccess.CreateParameter(DBCProsDocID, SqlDbType.Int, ProsDocID),
                             this.dataAccess.CreateParameter(DBCProcessedDate, SqlDbType.DateTime, ProcessedDate),
                             this.dataAccess.CreateParameter("BCSDocUpdateARSARSlinkID", SqlDbType.Int, bcsclient.BCSDocUpdateARSARSlinkID)

                         );
                                }
                            }
                        } // End of Else for AR and SAR


                    } // End of Try
                    catch (Exception exception)
                    {
                        string ErrorEmailBody = "Error has occured during Dequeue and Processessing of URL " + URLToDownload + " QueueID is " + BCSURLDownloadQueueID.ToString() +
                                         @" Under DequeueAndProcessURLToDownloadJob.Thread number" + ThreadNumber.ToString() + ". Error message: "
                                         + exception.Message;

                        IsErrored = true;


                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null,
                                "DequeueAndProcessURLToDownloadJob Exception", ErrorEmailBody, "support", null);

                        System.Threading.Thread.Sleep(ErrorSleepTime);

                    }

                    if (IsErrored)
                    {
                        this.dataAccess.ExecuteNonQuery
                             (
                                  this.DB1029ConnectionString,
                                  SPUpdateURLToDownloadStatus,
                                  this.dataAccess.CreateParameter(DBCBCSURLDownloadQueueID, SqlDbType.Int, CurrentBCSURLDownloadQueueID),
                                  this.dataAccess.CreateParameter(DBCIsErrored, SqlDbType.Bit, 1)
                              );
                    }

                } // end of while read

                if (CurrentBCSURLDownloadQueueID != -1)
                {
                    if (IsErrored)
                    {
                        this.dataAccess.ExecuteNonQuery
                             (
                                  this.DB1029ConnectionString,
                                  SPUpdateURLToDownloadStatus,
                                  this.dataAccess.CreateParameter(DBCBCSURLDownloadQueueID, SqlDbType.Int, CurrentBCSURLDownloadQueueID),
                                  this.dataAccess.CreateParameter(DBCIsErrored, SqlDbType.Bit, 1)
                              );
                    }
                    else
                    {
                        this.dataAccess.ExecuteNonQuery
                                         (
                                              this.DB1029ConnectionString,
                                              SPUpdateURLToDownloadStatus,
                                              this.dataAccess.CreateParameter(DBCBCSURLDownloadQueueID, SqlDbType.Int, CurrentBCSURLDownloadQueueID),
                                              this.dataAccess.CreateParameter(DBCIsDownloaded, SqlDbType.Bit, 1)
                                          );
                    }
                }

            }
        }

        public void RemoveCUSIPsFromFLTableNotinFLMode()
        {
            //Get List of clients with IncludeRemovedWatchListCUSIPInIPDocUpdate =1

            //ForEach client - Call the remove SP

            List<BCSClient> flBCSClients = GetALLClientConfig().Where(fl => fl.IncludeRemovedWatchListCUSIPInIPDocUpdate == true).ToList();

            flBCSClients.ForEach(
                    b =>
                    {
                        string StoredProcedureName = "BCS_" + b.ClientName + "RemoveCUSIPsFromFLTableNotInFLMode";


                        this.dataAccess.ExecuteNonQuery
                                                         (
                                                              this.DB1029ConnectionString,
                                                              StoredProcedureName
                                                          );

                    }
                );

        }

        public void ProcessDocUpdatesForFilingsPendingToBeProcessed()
        {
            DateTime CurrentDate = DateTime.Now;

            DateTime PreviousDate = CurrentDate.AddDays(-1).AddHours(-2); // just to be safe that we don't miss any entries.

            BCSClient bcsclient = GetGIMClientConfigs();

            int CurrentProsID = -1;

            int CurrentEdgarID = -1;

            DataTable SECDetails = null;

            bool ProcessFiling = true;

            using (IDataReader reader = this.dataAccess.ExecuteReader
            (
                 this.DB1029ConnectionString,
                 SPGetFilingsPendingToBeProcessed,
                  this.dataAccess.CreateParameter(DBCFromDateTime, SqlDbType.DateTime, PreviousDate),
                  this.dataAccess.CreateParameter(DBCToDateTime, SqlDbType.DateTime, CurrentDate)
             ))
            {


                while (reader.Read())
                {

                    int ProsID = Convert.ToInt32(reader["ProsID"]);

                    string DocumentType = reader["DocumentType"].ToString();

                    string DocumentTypeCode = UtilityFactory.ConvertDocumentType(DocumentType);

                    DateTime currentdate = DateTime.Now;

                    int EdgarID = Convert.ToInt32(reader["EdgarID"]);

                    string AccNum = reader["Acc#"].ToString();

                    string FormType = reader["FormType"].ToString();

                    DateTime EffectiveDate = Convert.ToDateTime(reader["EffectiveDate"]);

                    DateTime DocumentDate = Convert.ToDateTime(reader["DocumentDate"]);

                    DateTime DateFiled = Convert.ToDateTime(reader["DateFiled"]);

                    string CUSIP = reader["CUSIP"].ToString();

                    int TickerID = Convert.ToInt32(reader["TickerID"]);

                    string FundName = reader["FundName"].ToString();

                    try
                    {



                        if (CurrentProsID != ProsID || CurrentEdgarID != EdgarID)
                        {
                            SECDetails = null;

                            if (DocumentTypeCode == "SPS")
                            {
                                SECDetails = GetSECDetailsForProsID(ProsID, "SP");
                            }
                            else
                                if (DocumentTypeCode == "PS")
                                {
                                    SECDetails = GetSECDetailsForProsID(ProsID, "P");
                                }
                                else
                                {
                                    SECDetails = new DataTable();
                                    SECDetails.Columns.Add("Acc#", typeof(string));
                                    SECDetails.Columns.Add("EdgarID", typeof(Int32));
                                    SECDetails.Columns.Add("DateFiled", typeof(DateTime));
                                    SECDetails.Columns.Add("DocumentDate", typeof(DateTime));
                                    SECDetails.Columns.Add("DocumentType", typeof(string));
                                    SECDetails.Columns.Add("EffectiveDate", typeof(DateTime));
                                    SECDetails.Columns.Add("FormType", typeof(string));
                                }

                            if (SECDetails != null && SECDetails.Rows.Count == 0 && (DocumentTypeCode == "SPS" || DocumentTypeCode == "PS")) //SECDetails.Rows.Count will be 0 if there is not SP,P,RP and RSP that is processed.this check that current one is not a SPS or PS
                                ProcessFiling = false;
                            else
                                ProcessFiling = true;

                            if (SECDetails.Select("EdgarID = " + EdgarID.ToString()).Count() == 0) // check if for some reason we got the current EdgarID processed.
                            {

                                SECDetails.Rows.Add(
                                            AccNum,
                                            EdgarID,
                                            DateFiled,
                                            DocumentDate,
                                            DocumentTypeCode,
                                            EffectiveDate,
                                            FormType
                                            );
                            }

                            CurrentProsID = ProsID;

                            CurrentEdgarID = EdgarID;
                        }





                        if (ProcessFiling) // Process only if this is true.
                        {


                            DbParameterCollection collection = null;

                            bool IsProcessed = false;

                            int BCSDocUpdateID = -1;

                            collection = this.dataAccess.ExecuteNonQueryReturnOutputParams
                                    (
                                         this.DB1029ConnectionString,
                                         SPSaveDocUpdateFilingsPendingToBeProcessed,
                                         this.dataAccess.CreateParameter(DBCSECDetails, SqlDbType.Structured, SECDetails),
                                         this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                                         this.dataAccess.CreateParameter(DBCTickerID, SqlDbType.Int, TickerID),
                                         this.dataAccess.CreateParameter(DBCProsID, SqlDbType.Int, ProsID),
                                         this.dataAccess.CreateParameter(DBCEdgarID, SqlDbType.Int, EdgarID),
                                         this.dataAccess.CreateParameter(DBCFundName, SqlDbType.NVarChar, FundName),
                                         this.dataAccess.CreateParameter(DBCFilingAddedDate, SqlDbType.DateTime, currentdate),
                                         this.dataAccess.CreateParameter(DBCBCSDocUpdateID, SqlDbType.Int, BCSDocUpdateID, ParameterDirection.Output),
                                         this.dataAccess.CreateParameter(DBCIsProcessed, SqlDbType.Bit, IsProcessed, ParameterDirection.Output)
                                     );

                            if (collection != null)
                            {
                                if (collection["BCSDocUpdateID"].Value != DBNull.Value)
                                {
                                    BCSDocUpdateID = Convert.ToInt32(collection["BCSDocUpdateID"].Value);
                                }

                                if (collection["IsProcessed"].Value != DBNull.Value)
                                {
                                    IsProcessed = Convert.ToBoolean(collection["IsProcessed"].Value);
                                }
                            }

                            if (!IsProcessed)
                            {

                                string StoredProcedureName = "BCS_" + bcsclient.ClientPrefix + "RemoveExistingPDFZipRecords";

                                this.dataAccess.ExecuteNonQuery
             (
                  this.DB1029ConnectionString,
                  StoredProcedureName,
                  this.dataAccess.CreateParameter(DBCBCSDocUpdateID, SqlDbType.Int, BCSDocUpdateID)

              );

                            }


                        }

                    }

                    catch (Exception exception)
                    {
                        string error = "ProcessDocUpdatesForFilingsPendingToBeProcessed Failed for CUSIP "
                                    + CUSIP + " ProsID is "
                                    + ProsID.ToString()
                                    + " Exception is " + exception;
                        foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                                    "Process Doc Updates For FilingsPendingToBeProcessed Exception. Environment "
                                    + ConfigValues.AppEnvironment
                                , error, "support", null);
                        }

                        System.Threading.Thread.Sleep(ErrorSleepTime);
                    }



                    //Save Base and Supplement fillings in BCSDocUpdateSupplement

                    try
                    {

                        this.dataAccess.ExecuteNonQuery
                        (
                            this.DB1029ConnectionString,
                            SPSaveBaseAndSupplementsFilingsPendingToBeProcessed,
                            this.dataAccess.CreateParameter(DBCEdgarID, SqlDbType.Int, EdgarID),
                            this.dataAccess.CreateParameter(DBCDocumentType, SqlDbType.NVarChar, DocumentTypeCode),
                            this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                            this.dataAccess.CreateParameter(DBCTickerID, SqlDbType.Int, TickerID),
                            this.dataAccess.CreateParameter(DBCProsID, SqlDbType.Int, ProsID),
                            this.dataAccess.CreateParameter(DBCFundName, SqlDbType.NVarChar, FundName),
                            this.dataAccess.CreateParameter(DBCFilingAddedDate, SqlDbType.DateTime, currentdate)
                        );

                    }
                    catch (Exception exception)
                    {
                        string error = "SaveBaseAndSupplementsFilingsPendingToBeProcessed -  Failed for CUSIP "
                                    + CUSIP + " ProsID is "
                                    + ProsID.ToString()
                                    + " Exception is " + exception;
                        foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                                    "Process Doc Updates For FilingsPendingToBeProcessed Exception. Environment "
                                    + ConfigValues.AppEnvironment
                                , error, "support", null);
                        }

                        System.Threading.Thread.Sleep(ErrorSleepTime);
                    }




                }
            }


        }


        public void ProcessARSARFilingsPendingToBeProcessed()
        {
            DateTime CurrentDate = DateTime.Now;

            DateTime PreviousDate = CurrentDate.AddDays(-1).AddHours(-2); // just to be safe that we don't miss any entries.

            using (IDataReader reader = this.dataAccess.ExecuteReader
            (
                this.DB1029ConnectionString,
                SPGetARSARFilingsPendingToBeProcessed,
                this.dataAccess.CreateParameter(DBCFromDateTime, SqlDbType.DateTime, PreviousDate),
                this.dataAccess.CreateParameter(DBCToDateTime, SqlDbType.DateTime, CurrentDate)
            ))
            {
                while (reader.Read())
                {

                    int ProsID = Convert.ToInt32(reader["ProsID"]);

                    string DocumentTypeCode = UtilityFactory.ConvertDocumentType(reader["DocumentType"].ToString());

                    DateTime currentdate = DateTime.Now;

                    int EdgarID = Convert.ToInt32(reader["EdgarID"]);

                    string AccNum = reader["Acc#"].ToString();

                    string FormType = reader["FormType"].ToString();

                    string CUSIP = reader["CUSIP"].ToString();

                    int TickerID = Convert.ToInt32(reader["TickerID"]);

                    string FundName = reader["FundName"].ToString();

                    try
                    {

                        DbParameterCollection collection = null;

                        collection = this.dataAccess.ExecuteNonQueryReturnOutputParams
                        (
                            this.DB1029ConnectionString,
                            SPSaveARSARFilingsPendingToBeProcessed,
                            this.dataAccess.CreateParameter(DBCEdgarID, SqlDbType.Int, EdgarID),
                            this.dataAccess.CreateParameter(DBCDocumentType, SqlDbType.NVarChar, DocumentTypeCode),
                            this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                            this.dataAccess.CreateParameter(DBCTickerID, SqlDbType.Int, TickerID),
                            this.dataAccess.CreateParameter(DBCProsID, SqlDbType.Int, ProsID),
                            this.dataAccess.CreateParameter(DBCFundName, SqlDbType.NVarChar, FundName),
                            this.dataAccess.CreateParameter(DBCFilingAddedDate, SqlDbType.DateTime, currentdate)
                        );

                    }
                    catch (Exception exception)
                    {
                        string error = "ProcessARSARFilingsPendingToBeProcessed Failed for CUSIP "
                                        + CUSIP + " ProsID is "
                                        + ProsID.ToString()
                                        + " Exception is " + exception;

                        foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                                                    "Process Doc Updates For ProcessARSARFilingsPendingToBeProcessed Exception. Environment "
                                                    + ConfigValues.AppEnvironment
                                                    , error, "support", null);
                        }

                        System.Threading.Thread.Sleep(ErrorSleepTime);
                    }
                }

            }
        }


        #region BCSDocUpdateValidationService

        public void UpdateAPCOPCReceivedDate()
        {
            BCSClient client = GetGIMClientConfigs();

            using (IDataReader reader = this.dataAccess.ExecuteReader
            (
                this.DB1029ConnectionString,
                SPGetAPCOPCNotReceivedPDFNames
            ))
            {
                while (reader.Read())
                {
                    int EdgarID = Convert.ToInt32(reader["EdgarID"]);

                    string PDFName = reader["PDFName"].ToString();

                    string DocumentType = reader["DocumentType"].ToString();

                    bool isAPF = Convert.ToBoolean(reader["IsAPF"]);

                    bool isFullyLoaded = Convert.ToBoolean(reader["FullyLoaded"]);

                    string TableName = reader["TableName"].ToString();

                    string GatewayPDFName = string.Empty;

                    if (!isFullyLoaded && (DocumentType == "SPS" || DocumentType == "PS"))
                    {
                        GatewayPDFName = DocumentType + PDFName.Replace(".pdf", "") + EdgarID;
                    }
                    else
                    {
                        GatewayPDFName = DocumentType.Replace("SPS", "SP").Replace("PS", "P").Replace("RSP", "SP").Replace("RP", "P").Replace("RAR", "AR").Replace("RSAR", "SAR").Replace("RQR", "QR") + PDFName.Replace(".pdf", "");
                    }

                    GatewayPDFName = GatewayPDFName + "GIM.pdf";


                    if (File.Exists(ConfigValues.GatewayGoldPdfRepository + GatewayPDFName))
                    {
                        try
                        {

                            this.dataAccess.ExecuteNonQuery
                            (
                                this.DB1029ConnectionString,
                                SPUpdateAPCOPCReceivedDate,
                                this.dataAccess.CreateParameter(DBCEdgarID, SqlDbType.Int, EdgarID),
                                this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, PDFName),
                                this.dataAccess.CreateParameter(DBCIsAPF, SqlDbType.Bit, isAPF),
                                this.dataAccess.CreateParameter(DBCTableName, SqlDbType.NVarChar, TableName)
                            );

                        }
                        catch (Exception exception)
                        {
                            string error = "UpdateAPCOPCReceivedDate Failed " + exception;

                            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                            {
                                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                                                        "UpdateAPCOPCReceivedDate Failed"
                                                        , error, "support", null);
                            }

                            System.Threading.Thread.Sleep(ErrorSleepTime);
                        }
                    }
                }

            }
        }

        public void SendBCSDocUpdateValidationReport()
        {
            try
            {
                string gridHtml = string.Empty;
                BCSDocUpdateApprovalCUSIPData bCSDocUpdateApprovalCUSIPData = new BCSDocUpdateApprovalFactory().GetBCSDocUpdateApprovalDuplicateCUSIPData(null, null, 0, 0);
                BCSDocUpdateApprovalCUSIPData bcsCustomerDocUpdateDuplicateCUSIPData = new BCSDocUpdateApprovalFactory().GetBCSCustomerDocUpdateDuplicateCUSIPData(null, null, 0, 0);
                if (bCSDocUpdateApprovalCUSIPData.DuplicateCUSIPDetails.Count > 0 || bcsCustomerDocUpdateDuplicateCUSIPData.DuplicateCUSIPDetails.Count > 0)
                {
                    if (bCSDocUpdateApprovalCUSIPData.DuplicateCUSIPDetails.Count > 0)
                    {
                        gridHtml = GetBCSDocUpdateValidationReportDuplicateGridHtml(bCSDocUpdateApprovalCUSIPData.DuplicateCUSIPDetails);
                    }

                    if (bcsCustomerDocUpdateDuplicateCUSIPData.DuplicateCUSIPDetails.Count > 0)
                    {
                        gridHtml = gridHtml + "<br/><br/>Customer Doc Update Duplicate Details <br/><br/>";
                        gridHtml = gridHtml + GetBCSDocUpdateValidationReportDuplicateGridHtml(bcsCustomerDocUpdateDuplicateCUSIPData.DuplicateCUSIPDetails);
                    }



                    StringBuilder MailBody = new StringBuilder();

                    MailBody.Append("<html><body><p>");
                    MailBody.Append("There are duplicate CUSIP entries which need to be removed for BCS Document Update.");
                    MailBody.Append("<br/><br/>To view or remove the duplicate CUSIP entries, please click on this <a href=" + ConfigValues.BCSDocUpdateUIURL + " Target=_blank style=color:blue>link</a> or paste this link into your browser's location bar: " + ConfigValues.BCSDocUpdateUIURL);
                    MailBody.Append("<br/><br/>Please find below details for duplicate entries.");
                    MailBody.Append("<br/><br/>");
                    MailBody.Append(gridHtml);
                    MailBody.Append("</p></body></html>");
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.ConfirmationEmailListTo, null, null, ConfigValues.BCSDocUpdateValidationReportEmailSub, MailBody.ToString(), "support", null);
                }

            }
            catch (Exception ex)
            {
                (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                System.Threading.Thread.Sleep(ErrorSleepTime);
            }


            try
            {
                List<BCSEdgarDocUpdateValidationReportData> bCSEdgarDocUpdateValidationReportData = GetEdgarDocUpdateValidationReportData();

                if (bCSEdgarDocUpdateValidationReportData.Count > 0)
                {
                    string gridHtml = GetEdgarDocUpdateValidationReportGridHtml(bCSEdgarDocUpdateValidationReportData);

                    StringBuilder MailBody = new StringBuilder();

                    MailBody.Append("<html><body><p>");
                    MailBody.Append(gridHtml);
                    MailBody.Append("</p></body></html>");
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSEdgarDocUpdateValidationReportEmailListTo, null, null, ConfigValues.BCSEdgarDocUpdateValidationReportEmailSub, MailBody.ToString(), "support", null);
                }
            }
            catch (Exception ex)
            {
                (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                System.Threading.Thread.Sleep(ErrorSleepTime);
            }

            try
            {
                List<BCSEdgarDocUpdateValidationReportData> bcsDocUpdateSECValidationReport = GetBCSDocUpdateSECValidationReportData();

                if (bcsDocUpdateSECValidationReport.Count > 0)
                {
                    string gridHtml = GetBCSDocUpdateSECValidationReportGridHtml(bcsDocUpdateSECValidationReport);

                    StringBuilder MailBody = new StringBuilder();

                    MailBody.Append("<html><body><p>");
                    MailBody.Append(gridHtml);
                    MailBody.Append("</p></body></html>");

                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSDocUpdateSECValidationReportEmailListTo, null, null, ConfigValues.BCSDocUpdateSECValidationReportEmailSub, MailBody.ToString(), "support", null);
                }
            }
            catch (Exception ex)
            {
                (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                System.Threading.Thread.Sleep(ErrorSleepTime);
            }

            try
            {
                List<BCSDocUpdateSecDetailsNotMatchingReportData> bcsdocupdatesecdetailsnotmatchingreportdata = GetBCSDocUpdateSecDetailsNotMatchingReportData();

                if (bcsdocupdatesecdetailsnotmatchingreportdata.Count > 0)
                {
                    string gridHtml = GetBCSDocUpdateSECDetailsNotMatchingReportGridHtml(bcsdocupdatesecdetailsnotmatchingreportdata);

                    StringBuilder MailBody = new StringBuilder();

                    MailBody.Append("<html><body><p>");
                    MailBody.Append(gridHtml);
                    MailBody.Append("</p></body></html>");

                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSDocUpdateSECValidationReportEmailListTo, null, null, "Doc Update And Sec Details Not Matching Report", MailBody.ToString(), "support", null);
                }
            }
            catch (Exception ex)
            {
                (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                System.Threading.Thread.Sleep(ErrorSleepTime);
            }


            try
            {
                List<BCSSLINKNotAvailableReportData> bcsSLINKNotAvailableReportData = GetBCSSLINKNotAvailableReportData();

                if (bcsSLINKNotAvailableReportData.Count > 0)
                {
                    string gridHtml = GetBCSSLINKNotAvailableReportDataReportGridHtml(bcsSLINKNotAvailableReportData);

                    StringBuilder MailBody = new StringBuilder();

                    MailBody.Append("<html><body><p>");
                    MailBody.Append(gridHtml);
                    MailBody.Append("</p></body></html>");

                    foreach (string emailTO in ConfigValues.BCSSLINKNotAvailableReportEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, emailTO, null, null, ConfigValues.BCSSLINKNotAvailableReportEmailSub, MailBody.ToString(), "support", null);
                    }
                }
            }
            catch (Exception ex)
            {
                (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                System.Threading.Thread.Sleep(ErrorSleepTime);
            }



        }


        private List<BCSSLINKNotAvailableReportData> GetBCSSLINKNotAvailableReportData()
        {
            List<BCSSLINKNotAvailableReportData> bcsSLINKNotAvailableReportData = new List<BCSSLINKNotAvailableReportData>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetSLINKNotAvailableReportData
                ))
            {


                while (reader.Read())
                {
                    BCSSLINKNotAvailableReportData details = new BCSSLINKNotAvailableReportData();
                    details.BCSDocUpdateId = Convert.ToInt32(reader["BCSDocUpdateId"]);
                    details.EdgarID = Convert.ToInt32(reader["EdgarID"]);
                    details.CUSIP = Convert.ToString(reader["CUSIP"]);
                    details.FundName = Convert.ToString(reader["FundName"]);
                    details.ProcessedDate = Convert.ToString(reader["ProcessedDate"]);
                    bcsSLINKNotAvailableReportData.Add(details);
                }

            }

            return bcsSLINKNotAvailableReportData;
        }

        private string GetBCSSLINKNotAvailableReportDataReportGridHtml(List<BCSSLINKNotAvailableReportData> bcsSLINKNotAvailableReportData)
        {
            GridView emailDoc = new GridView();
            emailDoc.Width = Unit.Percentage(90);
            emailDoc.RowDataBound += BCSSLINKNotAvailableReport_RowDataBound;
            emailDoc.DataSource = from t in bcsSLINKNotAvailableReportData
                                  select new
                                  {
                                      t.BCSDocUpdateId,
                                      t.EdgarID,
                                      t.CUSIP,
                                      t.FundName,
                                      t.ProcessedDate
                                  };
            emailDoc.DataBind();

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            emailDoc.RenderControl(hw);
            return sb.ToString();
        }

        private void BCSSLINKNotAvailableReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Attributes.Add("style", "font-family: Verdana, Arial, Tahoma, Sans-Serif;vertical-align:middle;padding-left: 10px;line-height: 2.1;color: #ffffff;font-size: 12px;font-weight: 600;background: #1a6ab1;padding-bottom: 5px;");

                    e.Row.Cells[0].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:15%");
                    e.Row.Cells[1].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:15%");
                    e.Row.Cells[2].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:20%");
                    e.Row.Cells[3].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:35%");
                    e.Row.Cells[4].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:15%");

                    e.Row.Cells[0].Text = "BCSDocUpdateId";
                    e.Row.Cells[1].Text = "EdgarID";
                    e.Row.Cells[2].Text = "CUSIP";
                    e.Row.Cells[3].Text = "Fund Name";
                    e.Row.Cells[4].Text = "Processed Date";

                    break;
                case DataControlRowType.DataRow:
                    if (e.Row.RowState == DataControlRowState.Normal)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#ffffff;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[2].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[3].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[4].Attributes.Add("style", "padding-left: 10px;");

                    }
                    else if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#A9A9A9;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[2].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[3].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[4].Attributes.Add("style", "padding-left: 10px;");
                    }
                    break;
            }
        }

        private List<BCSEdgarDocUpdateValidationReportData> GetBCSDocUpdateSECValidationReportData()
        {
            List<BCSEdgarDocUpdateValidationReportData> edgarDocUpdateValidationReportData = new List<BCSEdgarDocUpdateValidationReportData>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetBCSDocUpdateSECValidationReportData
                ))
            {


                while (reader.Read())
                {
                    BCSEdgarDocUpdateValidationReportData details = new BCSEdgarDocUpdateValidationReportData();
                    details.BCSDocUpdateId = Convert.ToInt32(reader["BCSDocUpdateId"]);
                    details.CUSIP = Convert.ToString(reader["CUSIP"]);
                    details.ProsID = Convert.ToInt32(reader["ProsID"]);
                    details.FundName = Convert.ToString(reader["FundName"]);
                    details.BCSDocUpdateEdgarID = Convert.ToInt32(reader["EdgarID"]);
                    edgarDocUpdateValidationReportData.Add(details);
                }

            }

            return edgarDocUpdateValidationReportData;
        }


        private List<BCSDocUpdateSecDetailsNotMatchingReportData> GetBCSDocUpdateSecDetailsNotMatchingReportData()
        {
            List<BCSDocUpdateSecDetailsNotMatchingReportData> bcsdocupdatesecdetailsnotmatchingreportdata = new List<BCSDocUpdateSecDetailsNotMatchingReportData>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetBCSDocUpdateSecDetailsNotMatchingReport
                ))
            {


                while (reader.Read())
                {
                    BCSDocUpdateSecDetailsNotMatchingReportData details = new BCSDocUpdateSecDetailsNotMatchingReportData();
                    details.BCSDocUpdateId = Convert.ToInt32(reader["BCSDocUpdateId"]);
                    details.DocUpdateAccNumber = Convert.ToString(reader["BCSDocUpdateAccNumber"]);
                    details.SecDetailsAccNumber = Convert.ToString(reader["BCSDocUpdateSECDetailsAccNumber"]);

                    bcsdocupdatesecdetailsnotmatchingreportdata.Add(details);
                }

            }

            return bcsdocupdatesecdetailsnotmatchingreportdata;
        }
        private string GetBCSDocUpdateSECValidationReportGridHtml(List<BCSEdgarDocUpdateValidationReportData> bcsDocUpdateSECValidationReport)
        {
            GridView emailDoc = new GridView();
            emailDoc.Width = Unit.Percentage(90);
            emailDoc.RowDataBound += BCSDocUpdateSECValidationReport_RowDataBound;
            emailDoc.DataSource = from t in bcsDocUpdateSECValidationReport
                                  select new
                                  {
                                      t.BCSDocUpdateId,
                                      t.CUSIP,
                                      t.ProsID,
                                      t.FundName,
                                      t.BCSDocUpdateEdgarID
                                  };
            emailDoc.DataBind();

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            emailDoc.RenderControl(hw);
            return sb.ToString();
        }

        private string GetBCSDocUpdateSECDetailsNotMatchingReportGridHtml(List<BCSDocUpdateSecDetailsNotMatchingReportData> bcsdocupdatesecdetailsnotmatchingreportdata)
        {
            GridView emailDoc = new GridView();
            emailDoc.Width = Unit.Percentage(90);
            emailDoc.RowDataBound += BCSDocUpdateSECDetailsNotMatchingReport_RowDataBound;
            emailDoc.DataSource = from t in bcsdocupdatesecdetailsnotmatchingreportdata
                                  select new
                                  {
                                      t.BCSDocUpdateId,
                                      t.DocUpdateAccNumber,
                                      t.SecDetailsAccNumber
                                  };
            emailDoc.DataBind();

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            emailDoc.RenderControl(hw);
            return sb.ToString();
        }

        private void BCSDocUpdateSECDetailsNotMatchingReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Attributes.Add("style", "font-family: Verdana, Arial, Tahoma, Sans-Serif;vertical-align:middle;padding-left: 10px;line-height: 2.1;color: #ffffff;font-size: 12px;font-weight: 600;background: #1a6ab1;padding-bottom: 5px;");

                    e.Row.Cells[0].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:25%");
                    e.Row.Cells[1].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:25%");
                    e.Row.Cells[2].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:25%");

                    e.Row.Cells[0].Text = "BCSDocUpdateId";
                    e.Row.Cells[1].Text = "BCSAccNumber";
                    e.Row.Cells[2].Text = "SecDetailsAccNumber";

                    break;
                case DataControlRowType.DataRow:
                    if (e.Row.RowState == DataControlRowState.Normal)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#ffffff;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[2].Attributes.Add("style", "padding-left: 10px;");

                    }
                    else if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#A9A9A9;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[2].Attributes.Add("style", "padding-left: 10px;");
                    }
                    break;
            }
        }

        private void BCSDocUpdateSECValidationReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Attributes.Add("style", "font-family: Verdana, Arial, Tahoma, Sans-Serif;vertical-align:middle;padding-left: 10px;line-height: 2.1;color: #ffffff;font-size: 12px;font-weight: 600;background: #1a6ab1;padding-bottom: 5px;");

                    e.Row.Cells[0].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:20%");
                    e.Row.Cells[1].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:10%");
                    e.Row.Cells[2].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:10%");
                    e.Row.Cells[3].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:40%");
                    e.Row.Cells[4].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:20%");

                    e.Row.Cells[0].Text = "BCSDocUpdateId";
                    e.Row.Cells[1].Text = "CUSIP";
                    e.Row.Cells[2].Text = "ProsID";
                    e.Row.Cells[3].Text = "FundName";
                    e.Row.Cells[4].Text = "BCSDocUpdate - EdgarID";

                    break;
                case DataControlRowType.DataRow:
                    if (e.Row.RowState == DataControlRowState.Normal)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#ffffff;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[2].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[3].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[4].Attributes.Add("style", "padding-left: 10px;");

                    }
                    else if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#A9A9A9;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[2].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[3].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[4].Attributes.Add("style", "padding-left: 10px;");
                    }
                    break;
            }
        }



        private string GetEdgarDocUpdateValidationReportGridHtml(List<BCSEdgarDocUpdateValidationReportData> bCSEdgarDocUpdateValidationReportData)
        {
            GridView emailDoc = new GridView();
            emailDoc.Width = Unit.Percentage(90);
            emailDoc.RowDataBound += EdgarDocUpdateValidationReport_RowDataBound;
            emailDoc.DataSource = from t in bCSEdgarDocUpdateValidationReportData
                                  select new
                                  {
                                      t.BCSDocUpdateId,
                                      t.CUSIP,
                                      t.BCSDocUpdateEdgarID,
                                      t.BCSURLDownloadQueueEdgarID,
                                      t.ProsID
                                  };
            emailDoc.DataBind();

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            emailDoc.RenderControl(hw);
            return sb.ToString();
        }

        private static void EdgarDocUpdateValidationReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Attributes.Add("style", "font-family: Verdana, Arial, Tahoma, Sans-Serif;vertical-align:middle;padding-left: 10px;line-height: 2.1;color: #ffffff;font-size: 12px;font-weight: 600;background: #1a6ab1;padding-bottom: 5px;");

                    e.Row.Cells[0].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:20%");
                    e.Row.Cells[1].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:20%");
                    e.Row.Cells[2].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:20%");
                    e.Row.Cells[3].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:20%");
                    e.Row.Cells[4].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:20%");

                    e.Row.Cells[0].Text = "BCSDocUpdateId";
                    e.Row.Cells[1].Text = "CUSIP";
                    e.Row.Cells[2].Text = "BCSDocUpdate EdgarID";
                    e.Row.Cells[3].Text = "BCSURLDownloadQueue EdgarID";
                    e.Row.Cells[4].Text = "ProsID";

                    break;
                case DataControlRowType.DataRow:
                    if (e.Row.RowState == DataControlRowState.Normal)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#ffffff;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[2].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[3].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[4].Attributes.Add("style", "padding-left: 10px;");

                    }
                    else if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#A9A9A9;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[2].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[3].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[4].Attributes.Add("style", "padding-left: 10px;");
                    }
                    break;
            }

        }

        private List<BCSEdgarDocUpdateValidationReportData> GetEdgarDocUpdateValidationReportData()
        {
            List<BCSEdgarDocUpdateValidationReportData> edgarDocUpdateValidationReportData = new List<BCSEdgarDocUpdateValidationReportData>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetEdgarDocUpdateValidationReportData
                ))
            {


                while (reader.Read())
                {
                    BCSEdgarDocUpdateValidationReportData details = new BCSEdgarDocUpdateValidationReportData();
                    details.BCSDocUpdateId = Convert.ToInt32(reader["BCSDocUpdateId"]);
                    details.CUSIP = Convert.ToString(reader["CUSIP"]);
                    details.BCSDocUpdateEdgarID = Convert.ToInt32(reader["BCSDocUpdate - EdgarID"]);
                    details.BCSURLDownloadQueueEdgarID = Convert.ToInt32(reader["BCSURLDownloadQueue - EdgarID"]);
                    details.ProsID = Convert.ToInt32(reader["ProsID"]);
                    edgarDocUpdateValidationReportData.Add(details);
                }

            }

            return edgarDocUpdateValidationReportData;
        }



        private static string GetBCSDocUpdateValidationReportDuplicateGridHtml(List<BCSDocUpdateApprovalCUSIPDetails> duplicateCUSIPDetails)
        {
            GridView emailDoc = new GridView();

            BoundField CUSIP = new BoundField();
            CUSIP.DataField = "CUSIP";
            CUSIP.HeaderText = "CUSIP";
            emailDoc.Columns.Add(CUSIP);

            BoundField EdgarID = new BoundField();
            EdgarID.DataField = "EdgarID";
            EdgarID.HeaderText = "Edgar ID";
            emailDoc.Columns.Add(EdgarID);

            BoundField Accnumber = new BoundField();
            Accnumber.DataField = "Accnumber";
            Accnumber.HeaderText = "Acc#";
            emailDoc.Columns.Add(Accnumber);

            BoundField FundName = new BoundField();
            FundName.DataField = "FundName";
            FundName.HeaderText = "Fund Name";
            emailDoc.Columns.Add(FundName);

            TemplateField RRDPDFURL = new TemplateField();
            RRDPDFURL.HeaderText = "Document Type";
            RRDPDFURL.ItemTemplate = new AddTemplateLinkToGridView(ListItemType.Item, "DocumentType", "RRDPDFURL");
            emailDoc.Columns.Add(RRDPDFURL);

            BoundField Status = new BoundField();
            Status.DataField = "Status";
            Status.HeaderText = "Status";
            emailDoc.Columns.Add(Status);

            emailDoc.AutoGenerateColumns = false;

            emailDoc.Width = Unit.Percentage(90);
            emailDoc.RowDataBound += BCSDocUpdateApprovalCUSIPDetails_RowDataBound;
            emailDoc.DataSource = from t in duplicateCUSIPDetails
                                  select new
                                  {
                                      t.CUSIP,
                                      t.EdgarID,
                                      t.Accnumber,
                                      t.FundName,
                                      t.DocumentType,
                                      t.Status,
                                      t.RRDPDFURL
                                  };
            emailDoc.DataBind();

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            emailDoc.RenderControl(hw);
            return sb.ToString();
        }

        private static void BCSDocUpdateApprovalCUSIPDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Attributes.Add("style", "font-family: Verdana, Arial, Tahoma, Sans-Serif;vertical-align:middle;padding-left: 10px;line-height: 2.1;color: #ffffff;font-size: 12px;font-weight: 600;background: #1a6ab1;padding-bottom: 5px;");

                    e.Row.Cells[0].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:10%");
                    e.Row.Cells[1].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:10%");
                    e.Row.Cells[2].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:15%");
                    e.Row.Cells[3].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:35%");
                    e.Row.Cells[4].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:20%");
                    e.Row.Cells[5].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:10%");


                    break;
                case DataControlRowType.DataRow:
                    if (e.Row.RowState == DataControlRowState.Normal)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#ffffff;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[2].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[3].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[4].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[5].Attributes.Add("style", "padding-left: 10px;");

                    }
                    else if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#A9A9A9;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[2].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[3].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[4].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[5].Attributes.Add("style", "padding-left: 10px;");
                    }
                    break;
            }
        }

        public void CheckBCSDocUpdateValidations()
        {
            this.dataAccess.ExecuteNonQuery
              (
                   this.DB1029ConnectionString,
                   SPCheckBCSDocUpdateValidation
              );
        }

        #endregion

        public void ProcessNewlyAddedorModifiedCUSIPDetails()
        {
            BCSClient bcsclient = GetGIMClientConfigs();

            ZipFileCounter zipfilecounter = new ZipFileCounter();

            string ThreadNumber = "6";

            zipfilecounter.Counter = 0;

            DataTable SECDetails = null;

            int CurrentProsID = -1;

            string CurrentProsDocTypeId = string.Empty;

            Slink slink = null;

            string CurrentUrlToDownload = string.Empty;

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetNewlyAddedorModifiedCUSIPDetails
                ))
            {

                string CUSIP = null;
                int ProsID = 0;
                string ProsDocTypeId = string.Empty;
                while (reader.Read())
                {
                    try
                    {
                        int BCSDocUpdateID = 0;
                        bool IsReprocessed = false;
                        CUSIP = Convert.ToString(reader["CUSIP"]).Trim();
                        ProsID = Convert.ToInt32(reader["ProspectusID"]);
                        ProsDocTypeId = Convert.ToString(reader["ProsDocTypeId"]).Trim();




                        if (CurrentProsID != ProsID || SECDetails == null || CurrentProsDocTypeId != ProsDocTypeId)
                        {
                            SECDetails = null;

                            CurrentProsID = ProsID;

                            CurrentProsDocTypeId = ProsDocTypeId;

                            SECDetails = GetSECDetailsForProsID(ProsID, ProsDocTypeId);
                        }

                        if (SECDetails != null && SECDetails.Rows.Count > 0) // Process only if there are SEC Documents.
                        {
                            string URL = reader["URL"].ToString();

                            int TickerID = Convert.ToInt32(reader["TickerID"]);

                            string FundName = reader["FundName"].ToString();

                            int ProsDocID = Convert.ToInt32(reader["ProsDocID"]);



                            //call function

                            if (CurrentUrlToDownload != URL || (!slink.SlinkExists))
                            {
                                CurrentUrlToDownload = URL;

                                slink = CreateSLINKForURL(CurrentUrlToDownload, ThreadNumber);


                                bcsclient.CurrentZipFileName = string.Empty;

                            }



                            string ZipFileNameWithpath = string.Empty;

                            DbParameterCollection collection = null;

                            DateTime currentdate = DateTime.Now;

                            collection = this.dataAccess.ExecuteNonQueryReturnOutputParams
                                     (
                                          this.DB1029ConnectionString,
                                          SPSaveDocUpdateSLinkAndSECDetailsWithoutEdgarID,
                                          this.dataAccess.CreateParameter(DBCSECDetails, SqlDbType.Structured, SECDetails),
                                          this.dataAccess.CreateParameter(DBCRRDPDFURL, SqlDbType.NVarChar, slink.PDFURL),
                                          this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                                          this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                                          this.dataAccess.CreateParameter(DBCTickerID, SqlDbType.Int, TickerID),
                                          this.dataAccess.CreateParameter(DBCProsID, SqlDbType.Int, ProsID),
                                          this.dataAccess.CreateParameter(DBCFundName, SqlDbType.NVarChar, FundName),
                                          this.dataAccess.CreateParameter(DBCFromProcess, SqlDbType.Int, FromProcess.NewlyAddedOrModifiedCUSIPS),
                                          this.dataAccess.CreateParameter(DBCProsDocID, SqlDbType.Int, ProsDocID),
                                          this.dataAccess.CreateParameter(DBCDocumentType, SqlDbType.VarChar, ProsDocTypeId),
                                          this.dataAccess.CreateParameter(DBCProcessedDate, SqlDbType.DateTime, currentdate),
                                          this.dataAccess.CreateParameter(DBCFilingAddedDate, SqlDbType.DateTime, currentdate),
                                          this.dataAccess.CreateParameter(DBCBCSDocUpdateID, SqlDbType.Int, BCSDocUpdateID, ParameterDirection.Output),
                                          this.dataAccess.CreateParameter(DBCIsReprocessed, SqlDbType.Bit, IsReprocessed, ParameterDirection.Output)
                                      );

                            BCSDocUpdateID = -1;
                            if (collection != null)
                            {
                                if (collection["BCSDocUpdateID"].Value != DBNull.Value)
                                {
                                    BCSDocUpdateID = Convert.ToInt32(collection["BCSDocUpdateID"].Value);
                                }

                                if (collection["IsReprocessed"].Value != DBNull.Value)
                                {
                                    IsReprocessed = Convert.ToBoolean(collection["IsReprocessed"].Value);
                                }
                            }





                            string StoredProcedureForSLINK = "BCS_" + bcsclient.ClientPrefix + "SaveSlinkZipDetails";


                            bool CreateSlink = false;



                            if (bcsclient.ClientPrefix == "GIM")
                            {
                                if (IsReprocessed || BCSDocUpdateID == -1)
                                {
                                    CreateSlink = false;
                                }
                                else
                                    CreateSlink = true; // GIM looks at universe so slink zip needs to be created
                            }
                            else
                            {
                                //check if other clients like TRP etc support the cusip and if not then CreateSlink will be true.
                                CreateSlink = false;
                            }

                            if (CreateSlink)
                            {

                                try
                                {

                                    if (bcsclient.CurrentZipFileName == string.Empty && BCSDocUpdateID != -1)
                                    {
                                        bcsclient.CurrentZipFileName = CreateSlinkZipFile(slink, bcsclient,
                                                                    ThreadNumber, zipfilecounter,
                                                                    ProsDocTypeId, BCSDocUpdateID);
                                    }




                                    this.dataAccess.ExecuteNonQuery
                                 (
                                      this.DB1029ConnectionString,
                                      StoredProcedureForSLINK,
                                      this.dataAccess.CreateParameter(DBCBCSDocUpdateID, SqlDbType.Int, BCSDocUpdateID),
                                      this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                                      this.dataAccess.CreateParameter(DBCZipFileName, SqlDbType.NVarChar, bcsclient.CurrentZipFileName),
                                      this.dataAccess.CreateParameter(DBCIsSlinkExists, SqlDbType.Bit, slink.SlinkExists)
                                  );
                                }
                                catch (Exception createzipexception)
                                {
                                    //send and email

                                    string ErrorEmailBody = "Error has occured during Creating Zip for client " + bcsclient.ClientName
                                            + "  Process New Added or Modified CUSIP Details " + URL + " CUSIP " + CUSIP + " SLINK PDF Name "
                                            + slink.SlinkPDFName
                                            + " . Error message: "
                                     + createzipexception.Message;




                                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null,
                                            "Process New Added or Modified CUSIP Details Create Zip Exception", ErrorEmailBody, "support", null);

                                    System.Threading.Thread.Sleep(ErrorSleepTime);

                                }

                            }





                            slink.SlinkExists = true;


                        }


                        //
                    }

                    catch (Exception exception)
                    {
                        string error = "ProcessNewlyAddedorModifiedCUSIPDetails Failed for CUSIP "
                                    + CUSIP + " ProsID is "
                                    + ProsID.ToString()
                                    + " Exception is " + exception;
                        foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                                    "Process Doc Updates For ProcessNewlyAddedorModifiedCUSIPDetails Exception. Environment "
                                    + ConfigValues.AppEnvironment
                                , error, "support", null);
                        }

                        System.Threading.Thread.Sleep(ErrorSleepTime);
                    }
                }

            }
        }

        #region BCS TRP Report Service


        public void SendBCSCUSIPMissingInRPReport()
        {
            try
            {

                Object IsMissingCusip = this.dataAccess.ExecuteScalar
                (
                    this.DB1029ConnectionString,
                    SPCheckBCSTRPCUSIPMissingDetails
                );

                if ((bool)IsMissingCusip)
                {

                    StringBuilder MailBody = new StringBuilder();

                    MailBody.Append("<html><body><p>");
                    MailBody.Append("There are CUSIP(s) entries which are present in TRowePrice (InLine) FLT but not present in RightProspectus.");
                    MailBody.Append("<br/><br/>Please click on the <a href=" + ConfigValues.BCSTRPReportUIURL + " Target=_blank style=color:blue>link</a> below to find out more information on missing CUSIP(s).");
                    MailBody.Append("<br/><br/>" + ConfigValues.BCSTRPReportUIURL);
                    MailBody.Append("<br/>");
                    MailBody.Append("</p></body></html>");

                    foreach (string emailID in ConfigValues.BCSTRPReportEmailTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, emailID, null, null, ConfigValues.BCSTRPRPCUSIPMissignReportEmailSub, MailBody.ToString(), "support", null);
                    }
                }
            }
            catch (Exception ex)
            {
                (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSTRPReportService, true);

                System.Threading.Thread.Sleep(ErrorSleepTime);
            }

            //Send CUSIP Present in WatchList but not in RP email

            foreach (BCSClient client in GetALLClientConfig().FindAll(p => p.IsCUSIPWatchListProvided))
            {
                string SPCheckCUSIPMissingDetails = "BCS_" + client.ClientName + "CheckCUSIPMissingInRP";

                try
                {

                    Object IsMissingCusip = this.dataAccess.ExecuteScalar
                    (
                        this.DB1029ConnectionString,
                        SPCheckCUSIPMissingDetails
                    );

                    if ((bool)IsMissingCusip)
                    {

                        StringBuilder MailBody = new StringBuilder();

                        MailBody.Append("<html><body><p>");
                        MailBody.Append("There are CUSIP(s) entries which are present in " + client.ClientName + " (InLine) WatchList but not present in RightProspectus.");
                        MailBody.Append("<br/><br/>Please click on the <a href=" + ConfigValues.BCSDashBoardUIURL + " Target=_blank style=color:blue>link</a> below to find out more information on missing CUSIP(s).");
                        MailBody.Append("<br/><br/>" + ConfigValues.BCSDashBoardUIURL);
                        MailBody.Append("<br/>");
                        MailBody.Append("</p></body></html>");

                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSWatchlistCUSIPMissingInRPReportEmailTo, null, null, ConfigValues.BCSWatchlistCUSIPMissingInRPReportEmailSub.Replace("customer", client.ClientName), MailBody.ToString(), "support", null);

                    }
                }
                catch (Exception ex)
                {
                    (new Logging()).SaveExceptionLog("CUSIP Present in WatchList but not in RP" + ex.ToString(), BCSApplicationName.BCSTRPReportService, true);

                    System.Threading.Thread.Sleep(ErrorSleepTime);
                }
            }
        }



        public void SendBCSTRPFLTFTPDataDiscrepancyReport()
        {
            try
            {

                Object IsBCSTRPFLTFTPDataDiscrepancy = this.dataAccess.ExecuteScalar
                (
                    this.DB1029ConnectionString,
                    SPCheckBCSTRPFLTFTPDataDiscrepancy
                );

                if ((bool)IsBCSTRPFLTFTPDataDiscrepancy)
                {

                    StringBuilder MailBody = new StringBuilder();

                    MailBody.Append("<html><body><p>");
                    MailBody.Append("Data discrepancies have been identified for TRowePrice (InLine) project when comparing FLT and documents from the FTP site.<br/><br/>Please click on the <a href = " + ConfigValues.BCSTRPReportUIURL + " Target=_blank style=color:blue>link</a> below to find out more information on data descepencies.");
                    MailBody.Append("<br/><br/>" + ConfigValues.BCSTRPReportUIURL);
                    MailBody.Append("<br/><br/>Note: <br/>Data Discrepencies: The above link provides information on <br/>* Documents available on FTP but no entry in the FLT<br/>* Documents not available on FTP but there is an entry in the FLT<br/>* FLT - not received from TRP");
                    MailBody.Append("</p></body></html>");

                    foreach (string emailID in ConfigValues.BCSTRPReportEmailTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, emailID, null, null, ConfigValues.BCSTRPFLTFTPDataDiscrepancyReportEmailSub, MailBody.ToString(), "support", null);
                    }
                }
            }
            catch (Exception ex)
            {
                (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSTRPReportService, true);

                System.Threading.Thread.Sleep(ErrorSleepTime);
            }
        }


        #endregion

        public void SendBCSSLINKReport()
        {
            List<BCSSLINKReportData> bCSSLINKReportData = new List<BCSSLINKReportData>();


            using (IDataReader reader = this.dataAccess.ExecuteReader
                                        (
                                          DB1029ConnectionString,
                                          SPBCSGetBCSSLINKReportData
                                        ))
            {

                while (reader.Read())
                {
                    BCSSLINKReportData details = new BCSSLINKReportData();
                    details.SLINKFileName = Convert.ToString(reader["SLINKFileName"]);
                    details.ZipFileName = Convert.ToString(reader["ZipFileName"]);
                    if (!string.IsNullOrWhiteSpace(details.ZipFileName))
                    {
                        details.ZipFileName = details.ZipFileName.Split('\\')[details.ZipFileName.Split('\\').Length - 1].ToString();
                    }
                    bCSSLINKReportData.Add(details);
                }
            }

            if (bCSSLINKReportData.Count > 0)
            {
                GridView emailDoc = new GridView();
                emailDoc.Width = Unit.Percentage(90);
                emailDoc.RowDataBound += emailDoc_RowDataBound;
                emailDoc.DataSource = bCSSLINKReportData;
                emailDoc.DataBind();

                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                emailDoc.RenderControl(hw);
                string gridHtml = sb.ToString();

                StringBuilder MailBody = new StringBuilder();

                MailBody.Append("<html><body><p>");
                MailBody.Append("GIM SLINK FILE NAMES AND ZIP FILE NAMES");
                MailBody.Append("<br/><br/>");
                MailBody.Append(gridHtml);
                MailBody.Append("</p></body></html>");
                foreach (string emailID in ConfigValues.BCSSLINKReportEmailTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, emailID, null, null, ConfigValues.BCSSLINKReportEmailSub, MailBody.ToString(), "support", null);
                }
            }
        }

        private static void emailDoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Attributes.Add("style", "font-family: Verdana, Arial, Tahoma, Sans-Serif;vertical-align:middle;padding-left: 10px;line-height: 2.1;color: #ffffff;font-size: 12px;font-weight: 600;background: #1a6ab1;padding-bottom: 5px;");

                    e.Row.Cells[0].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:30%");
                    e.Row.Cells[1].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:30%");

                    e.Row.Cells[0].Text = "SLINK FILE NAME";
                    e.Row.Cells[1].Text = "ZIP FILE NAME";

                    break;
                case DataControlRowType.DataRow:
                    if (e.Row.RowState == DataControlRowState.Normal)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#ffffff;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding-left: 10px;");
                    }
                    else if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#A9A9A9;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding-left: 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding-left: 10px;");
                    }
                    break;
            }

        }

        public void UpdateDequeueStatusForNewAddedCUSIPS()
        {
            this.dataAccess.ExecuteNonQuery
              (
                   this.DB1029ConnectionString,
                   SPUpdateDequeueStatusForNewAddedCUSIPS
              );
        }

        public void GetAllOlderSPInFLModeAndReplaceWithP()
        {
            BCSClient bcsclient = GetGIMClientConfigs();

            ZipFileCounter zipfilecounter = new ZipFileCounter();

            string ThreadNumber = "8";

            zipfilecounter.Counter = 0;

            DataTable SECDetails = null;

            int CurrentProsID = -1;

            Slink slink = null;

            string CurrentUrlToDownload = string.Empty;

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetAllOlderSPInFLMode
                ))
            {

                string CUSIP = null;
                int ProsID = 0;
                while (reader.Read())
                {
                    try
                    {
                        int BCSDocUpdateID = 0;
                        bool IsReprocessed = false;
                        CUSIP = Convert.ToString(reader["CUSIP"]).Trim();
                        ProsID = Convert.ToInt32(reader["ProsID"]);

                        string ProsDocTypeID = Convert.ToString(reader["ProsDocTypeId"]).Trim();



                        if (CurrentProsID != ProsID || SECDetails == null)
                        {
                            SECDetails = null;

                            SECDetails = GetSECDetailsForProsID(ProsID, ProsDocTypeID);

                            CurrentProsID = ProsID;
                        }

                        if (SECDetails != null && SECDetails.Rows.Count > 0) // Process only if there are SEC Documents.
                        {
                            string URL = reader["URL"].ToString();

                            int TickerID = Convert.ToInt32(reader["TickerID"]);

                            string FundName = reader["FundName"].ToString();

                            int ProsDocID = Convert.ToInt32(reader["ProsDocId"]);

                            //call function

                            if (CurrentUrlToDownload != URL || (!slink.SlinkExists))
                            {
                                CurrentUrlToDownload = URL;

                                slink = CreateSLINKForURL(CurrentUrlToDownload, ThreadNumber);


                                bcsclient.CurrentZipFileName = string.Empty;



                            }



                            string ZipFileNameWithpath = string.Empty;

                            DbParameterCollection collection = null;

                            DateTime currentdate = DateTime.Now;

                            collection = this.dataAccess.ExecuteNonQueryReturnOutputParams
                                     (
                                          this.DB1029ConnectionString,
                                          SPSaveDocUpdateSLinkAndSECDetailsWithoutEdgarID,
                                          this.dataAccess.CreateParameter(DBCSECDetails, SqlDbType.Structured, SECDetails),
                                          this.dataAccess.CreateParameter(DBCRRDPDFURL, SqlDbType.NVarChar, slink.PDFURL),
                                          this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                                          this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                                          this.dataAccess.CreateParameter(DBCTickerID, SqlDbType.Int, TickerID),
                                          this.dataAccess.CreateParameter(DBCProsID, SqlDbType.Int, ProsID),
                                          this.dataAccess.CreateParameter(DBCFundName, SqlDbType.NVarChar, FundName),
                                          this.dataAccess.CreateParameter(DBCProsDocID, SqlDbType.Int, ProsDocID),
                                          this.dataAccess.CreateParameter(DBCProcessedDate, SqlDbType.DateTime, currentdate),
                                          this.dataAccess.CreateParameter(DBCFilingAddedDate, SqlDbType.DateTime, currentdate),
                                          this.dataAccess.CreateParameter(DBCFromProcess, SqlDbType.Int, FromProcess.ValidationProcesssToCheckSPInFLMode),
                                          this.dataAccess.CreateParameter(DBCDocumentType, SqlDbType.VarChar, ProsDocTypeID),
                                          this.dataAccess.CreateParameter(DBCBCSDocUpdateID, SqlDbType.Int, BCSDocUpdateID, ParameterDirection.Output),
                                          this.dataAccess.CreateParameter(DBCIsReprocessed, SqlDbType.Bit, IsReprocessed, ParameterDirection.Output)
                                      );
                            BCSDocUpdateID = -1;
                            if (collection != null)
                            {
                                if (collection["BCSDocUpdateID"].Value != DBNull.Value)
                                {
                                    BCSDocUpdateID = Convert.ToInt32(collection["BCSDocUpdateID"].Value);
                                }

                                if (collection["IsReprocessed"].Value != DBNull.Value)
                                {
                                    IsReprocessed = Convert.ToBoolean(collection["IsReprocessed"].Value);
                                }
                            }







                            string StoredProcedureForSLINK = "BCS_" + bcsclient.ClientPrefix + "SaveSlinkZipDetails";


                            bool CreateSlink = false;



                            if (bcsclient.ClientPrefix == "GIM")
                            {
                                if (IsReprocessed || BCSDocUpdateID == -1)
                                {
                                    CreateSlink = false;
                                }
                                else
                                    CreateSlink = true; // GIM looks at universe so slink zip needs to be created
                            }
                            else
                            {
                                //check if other clients like TRP etc support the cusip and if not then CreateSlink will be true.
                                CreateSlink = false;
                            }

                            if (CreateSlink)
                            {

                                try
                                {

                                    if (bcsclient.CurrentZipFileName == string.Empty && BCSDocUpdateID != -1)
                                    {
                                        bcsclient.CurrentZipFileName = CreateSlinkZipFile(slink, bcsclient,
                                                                            ThreadNumber, zipfilecounter,
                                                                            ProsDocTypeID, BCSDocUpdateID);
                                    }




                                    this.dataAccess.ExecuteNonQuery
                                 (
                                      this.DB1029ConnectionString,
                                      StoredProcedureForSLINK,
                                      this.dataAccess.CreateParameter(DBCBCSDocUpdateID, SqlDbType.Int, BCSDocUpdateID),
                                      this.dataAccess.CreateParameter(DBCPDFName, SqlDbType.NVarChar, slink.SlinkPDFName),
                                      this.dataAccess.CreateParameter(DBCZipFileName, SqlDbType.NVarChar, bcsclient.CurrentZipFileName),
                                      this.dataAccess.CreateParameter(DBCIsSlinkExists, SqlDbType.Bit, slink.SlinkExists)
                                  );
                                }
                                catch (Exception createzipexception)
                                {
                                    //send and email

                                    string ErrorEmailBody = "Error has occured during Creating Zip for client " + bcsclient.ClientName
                                            + "  Process New Added or Modified CUSIP Details " + URL + " CUSIP " + CUSIP + " SLINK PDF Name "
                                            + slink.SlinkPDFName
                                            + " . Error message: "
                                     + createzipexception.Message;




                                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null,
                                            "Process New Added or Modified CUSIP Details Create Zip Exception", ErrorEmailBody, "support", null);

                                    System.Threading.Thread.Sleep(ErrorSleepTime);

                                }

                            }





                            slink.SlinkExists = true;


                        }


                        //
                    }

                    catch (Exception exception)
                    {
                        string error = "ProcessNewlyAddedorModifiedCUSIPDetails Failed for CUSIP "
                                    + CUSIP + " ProsID is "
                                    + ProsID.ToString()
                                    + " Exception is " + exception;
                        foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                                    "Process Doc Updates For ProcessNewlyAddedorModifiedCUSIPDetails Exception. Environment "
                                    + ConfigValues.AppEnvironment
                                , error, "support", null);
                        }

                        System.Threading.Thread.Sleep(ErrorSleepTime);
                    }
                }

            }
        }


        public void GetMasterDBAndUpdateInWorkflowDB()
        {

            List<BCSClient> watchlistclients = GetALLClientConfig().Where(p => p.IsCUSIPWatchListProvided == true).ToList();

            watchlistclients.ForEach(
                            watchlistclient =>
                            {

                                DataTable FirstDollarCusipMasterTable = new DataTable();
                                FirstDollarCusipMasterTable.Columns.Add("CUSIP", typeof(string));
                                FirstDollarCusipMasterTable.Columns.Add("CIK", typeof(string));
                                FirstDollarCusipMasterTable.Columns.Add("SeriesID", typeof(string));
                                FirstDollarCusipMasterTable.Columns.Add("ClassContractID", typeof(string));
                                FirstDollarCusipMasterTable.Columns.Add("TickerSymbol", typeof(string));

                                using (IDataReader reader = this.dataAccess.ExecuteReader
                               (
                                    this.DB1029ConnectionString,
                                    "FirstDollarGetMasterCusips",
                                    this.dataAccess.CreateParameter("ClientPrefix", SqlDbType.VarChar, watchlistclient.ClientPrefix)
                                ))
                                {


                                    while (reader.Read())
                                    {
                                        string CUSIP = reader["CUSIP"].ToString();

                                        string CIK = reader["CIK"].ToString();

                                        string SeriesID = reader["SeriesID"].ToString();

                                        string ClassContractID = reader["ClassContractID"].ToString();

                                        string TickerSymbol = reader["TickerSymbol"].ToString();

                                        FirstDollarCusipMasterTable.Rows.Add(CUSIP,
                                                                             CIK,
                                                                             SeriesID,
                                                                             ClassContractID,
                                                                             TickerSymbol
                                                                             );
                                    }
                                }





                                if (FirstDollarCusipMasterTable.Rows.Count > 0)
                                {
                                    UpdateFirstDollarMasterinWorkflowDB(FirstDollarCusipMasterTable, watchlistclient.ClientPrefix);
                                }
                            }

            );
        }

        private void UpdateFirstDollarMasterinWorkflowDB(DataTable FirstDollarCusipMasterTable, string Prefix)
        {

            this.dataAccess.ExecuteNonQuery
                             (
                                  DBConnectionString.WorkflowDBConnectionString(),
                                  "UpdateFirstDollarCusipMaster",
                                  this.dataAccess.CreateParameter("CusipMasterTable", SqlDbType.Structured, FirstDollarCusipMasterTable),
                                  this.dataAccess.CreateParameter("Prefix", SqlDbType.NVarChar, Prefix)
                              );
        }

        public void ManualZipFileUpload()
        {
            using (IDataReader reader = dataAccess.ExecuteReader
               (
                    DB1029ConnectionString,
                    "BCS_ManualZipFileUpload"
                ))
            {
                while (reader.Read())
                {
                    string ZIPFileName = reader["ZIPFileName"].ToString();

                    try
                    {

                        if (UtilityFactory.Upload(ZIPFileName,
                                                ConfigurationManager.AppSettings.Get("FTP"),
                                                ConfigurationManager.AppSettings.Get("FTPUserName"),
                                                ConfigurationManager.AppSettings.Get("FTPPassword")
                                                )
                            )
                        {
                            Console.WriteLine("Uploaded: " + ZIPFileName);
                        }
                    }
                    catch (Exception exception)
                    {
                    }
                }
            }
        }

        #region Onetime Retrofit Process for Page properties

        public void RetrofitProcessForPageProperty()
        {
            try
            {
                //get a list of all prosdocid and prosdocurl  in prosdocs table with prosdoctypeid in (‘SP’,’P’) 

                //                string GetAllUnprocessedFilesSql = @"select distinct Prosdocs.ProsDocURL, Prosdocs.ProsDocTypeId  from ProsDocs Where Prosdocs.ProsDocTypeId in ('P', 'SP')
                //                                                      and Prosdocs.ProsDocURL <> '' and Prosdocs.ProsDocURL like '%www.rightprospectus.com/documents/%'
                //                                                      and ( Prosdocs.[PageCount] is null
                //                                                      or Prosdocs.[PageSizeHeight] is null
                //                                                      or Prosdocs.[PageSizeWidth] is null
                //                                                      )";


                string GetAllUnprocessedFilesSql = @"select distinct [RRDPDFURL] as ProsDocURL, [DocumentType] as ProsDocTypeId  from [BCSDocUpdate] Where [DocumentType] in ('P', 'SP')
                                                        and [RRDPDFURL] <> '' and [RRDPDFURL] like '%www.rightprospectus.com/documents/%'
                                                        and ( [PageCount] is null
                                                        or [PageSizeHeight] is null
                                                        or [PageSizeWidth] is null )";

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = DBConnectionString.DB1029ConnectionString();

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(GetAllUnprocessedFilesSql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //replace the prosdocurl url with the SAN path
                                string SANPath = String.Empty;
                                SANPath = reader["ProsDocURL"].ToString().Replace("http://www.rightprospectus.com/documents/", @"\\ecomad.int\webdfsprod\RightProspectus\WorkFiles\WebDocuments\");
                                SANPath = SANPath.Replace("/", @"\");

                                string DocumentTypeID = reader["ProsDocTypeId"].ToString();

                                PDFProperties PDFFileProperty = CheckPDFAndUpdateBookMarksView(SANPath, DocumentTypeID);

                                //Update this in database
                                string ProsDocURL = reader["ProsDocURL"].ToString();
                                StringBuilder UpdatePDFPropertyQuery = new StringBuilder();

                                if (PDFFileProperty.PageHeight != null && PDFFileProperty.PageWidth != null && PDFFileProperty.PageCount != null)
                                {

                                    UpdatePDFPropertyQuery.Append("Update [BCSDocUpdate] set ");

                                    if (PDFFileProperty.PageHeight != null)
                                    {
                                        UpdatePDFPropertyQuery.Append("[BCSDocUpdate].PageSizeHeight =  " + PDFFileProperty.PageHeight + " ,");
                                    }
                                    if (PDFFileProperty.PageWidth != null)
                                    {
                                        UpdatePDFPropertyQuery.Append("[BCSDocUpdate].PageSizeWidth = " + PDFFileProperty.PageWidth + ",");
                                    }
                                    if (PDFFileProperty.PageCount != null)
                                    {
                                        UpdatePDFPropertyQuery.Append("[BCSDocUpdate].PageCount = " + PDFFileProperty.PageCount);
                                    }

                                    UpdatePDFPropertyQuery.Append(" where [BCSDocUpdate].[RRDPDFURL] = " + "'" + ProsDocURL + "'");


                                    using (SqlConnection connection2 = new SqlConnection())
                                    {
                                        connection2.ConnectionString = DBConnectionString.DB1029ConnectionString();

                                        connection2.Open();
                                        using (SqlCommand command2 = new SqlCommand(UpdatePDFPropertyQuery.ToString(), connection2))
                                        {
                                            int rslt = command2.ExecuteNonQuery();
                                        }

                                        connection2.Close();
                                    }

                                }
                            }

                            reader.Close();
                        }
                    }
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        public static PDFProperties CheckPDFAndUpdateBookMarksView(string FileNameWithPath, string DocumentTypeID)
        {

            int? PageCount = null;

            double? PageHeight = null;

            double? PageWidth = null;


            try
            {

                PdfDocument document = null;

                //var func = new Func<bool>(() =>
                //{
                try
                {
                    document = PdfDocument.FromFile(FileNameWithPath);

                }
                catch (Exception excp)
                {
                    return null;

                    //return false;
                }
                //    return true;
                //});



                //if (document.Pages.Count > 0 && document.Security.AllowEditContent == true)
                //{

                //PageCount = document.Pages.Count;

                try
                {

                    if (DocumentTypeID == "P" || DocumentTypeID == "SP")
                    {
                        using (iTextSharp.text.pdf.PdfReader pdf = new iTextSharp.text.pdf.PdfReader(FileNameWithPath))
                        {
                            //http://api.itextpdf.com/itext/com/itextpdf/text/pdf/PdfReader.html#getBoxSize(int, java.lang.String)

                            PageCount = (int)pdf.NumberOfPages;
                            iTextSharp.text.Rectangle trimBox = pdf.GetBoxSize(1, "trim");


                            if (trimBox == null)
                            {
                                iTextSharp.text.Rectangle cropbox = pdf.GetBoxSize(1, "crop");
                                if (cropbox != null)
                                {

                                    PageWidth = Math.Round((Double)cropbox.Width / 72, 2);

                                    PageHeight = Math.Round((Double)cropbox.Height / 72, 2);

                                }
                                else
                                {
                                    iTextSharp.text.Rectangle mediabox = pdf.GetBoxSize(1, "media");
                                    if (mediabox != null)
                                    {

                                        PageWidth = Math.Round((Double)mediabox.Width / 72, 2);

                                        PageHeight = Math.Round((Double)mediabox.Height / 72, 2);

                                    }
                                }
                            }
                            else
                            {
                                PageWidth = Math.Round((Double)trimBox.Width / 72, 2);

                                PageHeight = Math.Round((Double)trimBox.Height / 72, 2);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                //}


                document.Close(); // important to close the file.
            }
            catch (Exception ex)
            {

            }

            return new PDFProperties()
            {

                PageCount = PageCount,
                PageHeight = PageHeight,
                PageWidth = PageWidth
            };
        }

        #endregion


        public void SaveBCSDocUpdateFileHistory(int clientId)
        {

            this.dataAccess.ExecuteNonQuery
                             (
                                  DB1029ConnectionString,
                                  BCSSaveBCSDocUpdateFileHistory,
                                  this.dataAccess.CreateParameter("ClientId", SqlDbType.Int, clientId)                                  
                              );
        }

        public void SendFailureNotification()
        {
            //Validate Customer watchlist
            try
            {
                using (IDataReader reader = dataAccess.ExecuteReader
               (
                    DB1029ConnectionString,
                    SPGetClientsForWatchlistFailureNotification
                ))
                {                   
                    
                    while (reader.Read())
                    {
                        string clientName = reader["ClientName"].ToString();

                        string emailTemplate = UtilityFactory.GetFailureNotificationEmailTemplate(FailureNotificationEmailTemplate.WatchlistFailureNotification).Replace("<<CustomerName>>", clientName).Replace("<<Date>>", DateTime.Now.ToShortDateString());

                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.WatchlistFailureNotificationEmailTo, null, null,
                                ConfigValues.WatchlistFailureNotificationEmailSubject.Replace("CustomerName", clientName), emailTemplate, "support", null);
                    }
                }
            }
            catch (Exception ex)
            {
                (new Logging()).SaveExceptionLog("SendFailureNotification SPGetClientsForWatchlistFailureNotification : " + ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);

            }


            //Validate Customer DOCUPDT
            try
            {
                using (IDataReader reader = dataAccess.ExecuteReader
               (
                    DB1029ConnectionString,
                    SPGetClientsForDocUPDTFailureNotification
                ))
                {

                    while (reader.Read())
                    {
                        string clientName = reader["ClientName"].ToString();

                        string emailTemplate = UtilityFactory.GetFailureNotificationEmailTemplate(FailureNotificationEmailTemplate.CustDocUPDTFailureNotification).Replace("<<CustomerName>>", clientName).Replace("<<Date>>", DateTime.Now.ToShortDateString());

                        string emailSubject = ConfigValues.CustDocUPDTFailureNotificationEmailSubject.Replace("CustomerName", clientName);

                        switch (clientName)
                        {
                            case "GIM":
                            case "GMS":
                                emailSubject = emailSubject.Replace("(IP) File", "(NU/IP) Files");
                                break;
                        }

                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.CustDocUPDTFailureNotificationEmailTo, null, null,
                               emailSubject, emailTemplate, "support", null);
                    }
                }
            }
            catch (Exception ex)
            {
                (new Logging()).SaveExceptionLog("SendFailureNotification SPGetClientsForDocUPDTFailureNotification : " + ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);

            }
        }
    }

    internal class AddTemplateLinkToGridView : ITemplate
    {
        ListItemType _type;
        string _colText, _colURL;

        public AddTemplateLinkToGridView(ListItemType type, string colText, string colURL)
        {
            _type = type;
            _colText = colText;
            _colURL = colURL;
        }
        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {
            switch (_type)
            {
                case ListItemType.Item:
                    HyperLink ht = new HyperLink();
                    ht.Target = "_blank";
                    ht.DataBinding += new EventHandler(ht_DataBinding);
                    container.Controls.Add(ht);
                    break;
            }
        }

        void ht_DataBinding(object sender, EventArgs e)
        {
            HyperLink lnk = (HyperLink)sender;
            GridViewRow container = (GridViewRow)lnk.NamingContainer;
            object colText = DataBinder.Eval(container.DataItem, _colText);
            object colURL = DataBinder.Eval(container.DataItem, _colURL);
            if (colText != DBNull.Value)
            {
                lnk.Text = Convert.ToString(colText);
                lnk.NavigateUrl = Convert.ToString(colURL);
            }

        }
    }
}
