using System;
using System.Configuration;

namespace BCS.ObjectModel.Factories
{
    public static class ConfigValues
    {
        public static string RPSourceURLReplace = ConfigurationManager.AppSettings.Get("RPSourceURLReplace");

        public static string RPDestinationURLReplace = ConfigurationManager.AppSettings.Get("RPDestinationURLReplace");

        public static string RPDestinationSANReplace = ConfigurationManager.AppSettings.Get("RPDestinationSANReplace");

        public static string RPBCSSANPath = ConfigurationManager.AppSettings.Get("RPBCSSANPath");

        public static string SMTP = ConfigurationManager.AppSettings.Get("SMTP");

        public static string LogDirectory = ConfigurationManager.AppSettings.Get("LogDirectory");

        public static string LogFileName = ConfigurationManager.AppSettings.Get("LogFileName");

        public static string ConfirmationEmailListTo = ConfigurationManager.AppSettings.Get("ConfirmationEmailListTo");

        public static string EmailFrom = ConfigurationManager.AppSettings.Get("EmailFrom");

        public static string BCSDocUpdateValidationReportEmailSub = ConfigurationManager.AppSettings.Get("BCSDocUpdateValidationReportEmailSub");

        public static string BCSEdgarDocUpdateValidationReportEmailSub = ConfigurationManager.AppSettings.Get("BCSEdgarDocUpdateValidationReportEmailSub");

        public static string BCSDocUpdateSECValidationReportEmailSub = ConfigurationManager.AppSettings.Get("BCSDocUpdateSECValidationReportEmailSub");

        public static string BCSEdgarDocUpdateValidationReportEmailListTo = ConfigurationManager.AppSettings.Get("BCSEdgarDocUpdateValidationReportEmailListTo");

        public static string BCSDocUpdateSECValidationReportEmailListTo = ConfigurationManager.AppSettings.Get("BCSDocUpdateSECValidationReportEmailListTo");

        public static string BCSDocUpdateFileUploadedEmailSub = ConfigurationManager.AppSettings.Get("BCSDocUpdateFileUploadedEmailSub");

        public static string TimerInterval = ConfigurationManager.AppSettings.Get("TimerInterval");

        public static string BCSDocUpdateUIURL = ConfigurationManager.AppSettings.Get("BCSDocUpdateUIURL");

        public static string HiQPDFSerialNumber = ConfigurationManager.AppSettings.Get("HiQPDFSerialNumber");

        public static int PDFWorkflowTimeOutDuration = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PDFWorkflowTimeOutDuration"));

        //BCSDocUpdateSlinkIntegrationService

        public static string BCSDocUpdateSlinkIntegrationServiceEmailSub = ConfigurationManager.AppSettings.Get("BCSDocUpdateSlinkIntegrationServiceEmailSub");

        public static string AppEnvironment = ConfigurationManager.AppSettings.Get("AppEnvironment");

        public static string TimeFromRunSLinkDocUpdate = ConfigurationManager.AppSettings.Get("TimeFromRunSLinkDocUpdate");

        public static string TimeToRunSLinkDocUpdate = ConfigurationManager.AppSettings.Get("TimeToRunSLinkDocUpdate");

        public static int DequeueURLToDownloadThreads = Convert.ToInt32(ConfigurationManager.AppSettings.Get("DequeueURLToDownloadThreads"));        

        public static string BCSExceptionEmailListTo = ConfigurationManager.AppSettings.Get("BCSExceptionEmailListTo");

        public static string FLTConfirmationEmailListTo = ConfigurationManager.AppSettings.Get("FLTConfirmationEmailListTo");

        public static string FLTErrorEmailListTo = ConfigurationManager.AppSettings.Get("FLTErrorEmailListTo");

        public static string BCSExceptionEmailSub = ConfigurationManager.AppSettings.Get("BCSExceptionEmailSub");

        public static string FilingsPendingToBeProcessedTime = ConfigurationManager.AppSettings.Get("FilingsPendingToBeProcessedTime");

        public static string ProcessNewlyAddedOrModifiedCUSIPTime = ConfigurationManager.AppSettings.Get("ProcessNewlyAddedOrModifiedCUSIPTime");

        public static string TimeFromRunPreflight = ConfigurationManager.AppSettings.Get("TimeFromRunPreflight");

        public static string TimeToRunPreflight = ConfigurationManager.AppSettings.Get("TimeToRunPreflight");

        public static string TimeFromResetClients = ConfigurationManager.AppSettings.Get("TimeFromResetClients");

        public static string TimeToResetClients = ConfigurationManager.AppSettings.Get("TimeToResetClients");

        public static string SourceFileWithPathToCompare = ConfigurationManager.AppSettings.Get("SourceFileWithPathToCompare");

        public static string DestinationFileWithPathToCompare = ConfigurationManager.AppSettings.Get("DestinationFileWithPathToCompare");

        public static string SleepTimeAfterResettingClients = ConfigurationManager.AppSettings.Get("SleepTimeAfterResettingClients");

        public static string UpdateAPCOPCReceivedDateTimeInterval = ConfigurationManager.AppSettings.Get("UpdateAPCOPCReceivedDateTimeInterval");

        public static string TimeFromResetFilingsPendingToBeProcessed = ConfigurationManager.AppSettings.Get("TimeFromResetFilingsPendingToBeProcessed");

        public static string TimeToResetFilingsPendingToBeProcessed = ConfigurationManager.AppSettings.Get("TimeToResetFilingsPendingToBeProcessed");

        public static string TimeFromARSARFilingsPendingToBeProcessed = ConfigurationManager.AppSettings.Get("TimeFromARSARFilingsPendingToBeProcessed");

        public static string TimeToARSARFilingsPendingToBeProcessed = ConfigurationManager.AppSettings.Get("TimeToARSARFilingsPendingToBeProcessed");

        public static string TimeFromProcessNewlyAddedOrModifiedCUSIP = ConfigurationManager.AppSettings.Get("TimeFromProcessNewlyAddedOrModifiedCUSIP");

        public static string TimeToProcessNewlyAddedOrModifiedCUSIP = ConfigurationManager.AppSettings.Get("TimeToProcessNewlyAddedOrModifiedCUSIP");

        public static string TimeFromRunFLTFileProcess = ConfigurationManager.AppSettings.Get("TimeFromRunFLTFileProcess");

        public static string TimeToRunFLTFileProcess = ConfigurationManager.AppSettings.Get("TimeToRunFLTFileProcess");

        public static string FLTFilePath = ConfigurationManager.AppSettings.Get("FLTFilePath");

        public static string FTPFilePath = ConfigurationManager.AppSettings.Get("FTPFilePath");

        public static string TRPCompany = ConfigurationManager.AppSettings.Get("TRPCompany");

        public static string BCSSLINKNotAvailableReportEmailSub = ConfigurationManager.AppSettings.Get("BCSSLINKNotAvailableReportEmailSub");

        public static string BCSSLINKNotAvailableReportEmailListTo = ConfigurationManager.AppSettings.Get("BCSSLINKNotAvailableReportEmailListTo");

        //WatchListFileProcessing       

        public static string WatchListReportLocation = ConfigurationManager.AppSettings.Get("WatchListReportLocation");


        #region BCS TRP REPORTS
        
        public static string BCSTRowePriceFLTFTPArchiveDocumentPath = ConfigurationManager.AppSettings.Get("BCSTRowePriceFLTFTPArchiveDocumentPath");
        
        public static string BCSTRowePriceFLTFTPArchiveDocumentPathURL = ConfigurationManager.AppSettings.Get("BCSTRowePriceFLTFTPArchiveDocumentPathURL");

        public static string BCSTRPFLTFTPDataDiscrepancyReportEmailSub = ConfigurationManager.AppSettings.Get("BCSTRPFLTFTPDataDiscrepancyReportEmailSub");

        public static string BCSTRPRPCUSIPMissignReportEmailSub = ConfigurationManager.AppSettings.Get("BCSTRPRPCUSIPMissignReportEmailSub");

        public static string BCSTRPReportEmailTo = ConfigurationManager.AppSettings.Get("BCSTRPReportEmailTo");

        public static string BCSTRPReportUIURL = ConfigurationManager.AppSettings.Get("BCSTRPReportUIURL");        

        public static string SleepTimeBCSTRPCUSIPMissingReport = ConfigurationManager.AppSettings.Get("SleepTimeBCSTRPCUSIPMissingReport");

        public static string TimeFromBCSTRPFLTFTPDataDiscrepancyReportMorning = ConfigurationManager.AppSettings.Get("TimeFromBCSTRPFLTFTPDataDiscrepancyReportMorning");

        public static string TimeToBCSTRPFLTFTPDataDiscrepancyReportMorning = ConfigurationManager.AppSettings.Get("TimeToBCSTRPFLTFTPDataDiscrepancyReportMorning");

        public static string TimeFromBCSTRPFLTFTPDataDiscrepancyReportEvening = ConfigurationManager.AppSettings.Get("TimeFromBCSTRPFLTFTPDataDiscrepancyReportEvening");

        public static string TimeToBCSTRPFLTFTPDataDiscrepancyReportEvening = ConfigurationManager.AppSettings.Get("TimeToBCSTRPFLTFTPDataDiscrepancyReportEvening");

        public static string SleepTimeBCSTRPFLTFTPDataDiscrepancyReport = ConfigurationManager.AppSettings.Get("SleepTimeBCSTRPFLTFTPDataDiscrepancyReport");        

        public static string TimeFromBCSSLINKReport = ConfigurationManager.AppSettings.Get("TimeFromBCSSLINKReport");

        public static string TimeToFromBCSSLINKReport = ConfigurationManager.AppSettings.Get("TimeToFromBCSSLINKReport");

        public static string BCSSLINKReportEmailSub = ConfigurationManager.AppSettings.Get("BCSSLINKReportEmailSub");

        public static string BCSSLINKReportEmailTo = ConfigurationManager.AppSettings.Get("BCSSLINKReportEmailTo");

        public static int ErrorSleepTime = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ErrorSleepTime"));

        #endregion


        public static string TimeFromUpdateDequeueStatusForNewAddedCUSIPS = ConfigurationManager.AppSettings.Get("TimeFromUpdateDequeueStatusForNewAddedCUSIPS");

        public static string TimeToUpdateDequeueStatusForNewAddedCUSIPS = ConfigurationManager.AppSettings.Get("TimeToUpdateDequeueStatusForNewAddedCUSIPS");

        public static string TimeFromGetAllOlderSPInFLModeAndReplaceWithP = ConfigurationManager.AppSettings.Get("TimeFromGetAllOlderSPInFLModeAndReplaceWithP");

        public static string TimeToGetAllOlderSPInFLModeAndReplaceWithP = ConfigurationManager.AppSettings.Get("TimeToGetAllOlderSPInFLModeAndReplaceWithP");

        public static string GatewayGoldPdfRepository = ConfigurationManager.AppSettings.Get("GatewayGoldPdfRepository");

        public static string BCSWatchlistCUSIPMissingInRPReportEmailSub = ConfigurationManager.AppSettings.Get("BCSWatchlistCUSIPMissingInRPReportEmailSub");

        public static string BCSWatchlistCUSIPMissingInRPReportEmailTo = ConfigurationManager.AppSettings.Get("BCSWatchlistCUSIPMissingInRPReportEmailTo");

        public static string BCSDashBoardUIURL = ConfigurationManager.AppSettings.Get("BCSDashBoardUIURL");

        public static string BCSTRPFLTDocArchiveDropPath = ConfigurationManager.AppSettings.Get("BCSTRPFLTDocArchiveDropPath");

        public static string BCSTRPFLTArchiveDropPath = ConfigurationManager.AppSettings.Get("BCSTRPFLTArchiveDropPath");

        public static string WatchlistFailureNotificationEmailSubject = ConfigurationManager.AppSettings.Get("WatchlistFailureNotificationEmailSubject");

        public static string CustDocUPDTFailureNotificationEmailSubject = ConfigurationManager.AppSettings.Get("CustDocUPDTFailureNotificationEmailSubject");

        public static string WatchlistFailureNotificationEmailTo = ConfigurationManager.AppSettings.Get("WatchlistFailureNotificationEmailTo");

        public static string CustDocUPDTFailureNotificationEmailTo = ConfigurationManager.AppSettings.Get("CustDocUPDTFailureNotificationEmailTo");


        public static string EdgarOnlineFeedFTPPath = ConfigurationManager.AppSettings.Get("EdgarOnlineFeedFTPPath");
        public static string EdgarOnlineFeedFTPUsername = ConfigurationManager.AppSettings.Get("EdgarOnlineFeedFTPUsername");
        public static string EdgarOnlineFeedFTPPassword = ConfigurationManager.AppSettings.Get("EdgarOnlineFeedFTPPassword");
        public static string EdgarOnlineFeedArchiveDropPath = ConfigurationManager.AppSettings.Get("EdgarOnlineFeedArchiveDropPath");
        public static string EdgarOnlineFeedArchiveDropExractFilePath = ConfigurationManager.AppSettings.Get("EdgarOnlineFeedArchiveDropExractFilePath");
        public static string EdgarOnlineFeedArchiveProcessedFilePath = ConfigurationManager.AppSettings.Get("EdgarOnlineFeedArchiveProcessedFilePath");

        public static string RightprospectusURL = ConfigurationManager.AppSettings.Get("RightprospectusURL");

        public static string EdgarOnlineFilePath = ConfigurationManager.AppSettings.Get("EdgarOnlineFilePath");

    }
}


