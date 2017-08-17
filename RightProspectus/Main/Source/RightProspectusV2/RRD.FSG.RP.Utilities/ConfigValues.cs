// ***********************************************************************
// Assembly         : RRD.FSG.RP.Utilities
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;

/// <summary>
/// The Utilities namespace.
/// </summary>
namespace RRD.FSG.RP.Utilities
{
    /// <summary>
    /// Class ConfigValues.
    /// </summary>
    public static class ConfigValues
    {
        /// <summary>
        /// The system database cache time out
        /// </summary>
        private static TimeSpan? systemDBCacheTimeOut = null;

        /// <summary>
        /// The client database cache time out
        /// </summary>
        private static TimeSpan? clientDBCacheTimeOut = null;

        /// <summary>
        /// The vertical XML import export infopath form name
        /// </summary>
        private static string verticalXMLImportExportInfopathFormName = string.Empty;

        /// <summary>
        /// The print request log file dir
        /// </summary>
        private static string printRequestLogFileDir = string.Empty;

        /// <summary>
        /// The print request drop file dir
        /// </summary>
        private static string printRequestDropFileDir = string.Empty;

        /// <summary>
        /// The environment
        /// </summary>
        private static string environment = string.Empty;

        /// <summary>
        /// The SMTP
        /// </summary>
        private static string smtp = string.Empty;

        /// <summary>
        /// The email from
        /// </summary>
        private static string emailFrom = string.Empty;

        /// <summary>
        /// The error email to
        /// </summary>
        private static string errorEmailTo = string.Empty;

        /// <summary>
        /// The request material email from
        /// </summary>
        private static string requestMaterialEmailFrom = string.Empty;

        /// <summary>
        /// The report content email from
        /// </summary>
        private static string reportContentEmailFrom = string.Empty;
        /// <summary>
        /// hostedEngineURL
        /// </summary>
        private static string hostedEngineURL = string.Empty;
        /// <summary>
        /// hTTPSHostedEngineURL
        /// </summary>
        private static string hTTPSHostedEngineURL = string.Empty;        
        /// <summary>
        /// The Liquidatin Merging content email to
        /// </summary>
        private static string cusipMergerLiqudationEmailTo = string.Empty;
        /// <summary>
        /// The sleep time between requests for vertical import export
        /// </summary>
        private static int? sleepTimeBetweenRequestsForVerticalImportExport;
        
        /// <summary>
        /// The sleep time between requests for vertical import export
        /// </summary>
        private static int? sleepTimeCUSIPMergerLiqudationReport;
        /// <summary>
        /// The sleep time between requests for service
        /// </summary>
        private static int? sleepTimeBetweenServiceRequests;
        /// <summary>
        /// The email from
        /// </summary>
        private static string validationServiceExceptionEmailTo = string.Empty;
        /// <summary>
        /// appEnvironment
        /// </summary>
        private static string appEnvironment = string.Empty;
        /// <summary>
        /// CUSIPMergerLiqudationEmailSub
        /// </summary>
        private static string cusipMergerLiqudationEmailSub = string.Empty;
        /// <summary>
        /// verticalDataImport_ExcelImport_CommandTimeout
        /// </summary>
        private static int? verticalDataImport_ExcelImport_CommandTimeout;
        /// <summary>
        /// VerticalDataImport_ApproveProofing_CommandTimeout
        /// </summary>
        private static int? verticalDataImport_ApproveProofing_CommandTimeout;

        /// <summary>
        /// The sleep time when error
        /// </summary>
        private static int? sleepTimeWhenError;

        /// <summary>
        /// SleepTimeWhenError
        /// </summary>
        /// <value>The sleep time when error.</value>
        public static int SleepTimeWhenError
        {
            get
            {
                if (sleepTimeWhenError == null)
                {
                    sleepTimeWhenError = Convert.ToInt32(ConfigurationManager.AppSettings["SleepTimeWhenError"]);
                }

                return sleepTimeWhenError.Value;
            }
        }
        /// <summary>
        /// SleepTimeBetweenRequestsForVerticalImportExport
        /// </summary>
        /// <value>The sleep time between requests for vertical import export.</value>
        public static int SleepTimeBetweenRequestsForVerticalImportExport
        {
            get
            {
                if (sleepTimeBetweenRequestsForVerticalImportExport == null)
                {
                    sleepTimeBetweenRequestsForVerticalImportExport = Convert.ToInt32(ConfigurationManager.AppSettings["SleepTimeBetweenRequestsForVerticalImportExport"]);
                }

                return sleepTimeBetweenRequestsForVerticalImportExport.Value;
            }
        }
       /// <summary>
        /// SleepTimeBetweenRequestsForLiquidatingMergingReport
        /// </summary>
        /// <value>The sleep time between requests for vertical import export.</value>
        public static int SleepTimeCUSIPMergerLiqudationReport
        {
            get
            {
                if (sleepTimeCUSIPMergerLiqudationReport == null)
                {
                    sleepTimeCUSIPMergerLiqudationReport = Convert.ToInt32(ConfigurationManager.AppSettings["SleepTimeCUSIPMergerLiqudationReport"]);
                }

                return sleepTimeCUSIPMergerLiqudationReport.Value;
            }
        }
                /// <summary>
        /// SleepTimeBetweenServiceRequests
        /// </summary>
        /// <value>The sleep time between service requests.</value>
        public static int SleepTimeBetweenServiceRequests
        {
            get
            {
                if (sleepTimeBetweenServiceRequests == null)
                {
                    sleepTimeBetweenServiceRequests = Convert.ToInt32(ConfigurationManager.AppSettings["SleepTimeBetweenServiceRequests"]);
                }

                return sleepTimeBetweenServiceRequests.Value;
            }
        }

        

        /// <summary>
        /// VerticalXMLImportExport Info path FormName
        /// </summary>
        /// <value>The name of the vertical XML import export infopath form.</value>
        public static string VerticalXMLImportExportInfopathFormName
        {
            get
            {
                if (string.IsNullOrEmpty(verticalXMLImportExportInfopathFormName))
                {
                    verticalXMLImportExportInfopathFormName = ConfigurationManager.AppSettings["VerticalXMLImportExportInfopathFormName"];
                }

                return verticalXMLImportExportInfopathFormName;
            }
        }

        /// <summary>
        /// SystemDBCacheTimeOut
        /// </summary>
        /// <value>The system database cache time out.</value>
        public static TimeSpan SystemDBCacheTimeOut
        {
            get
            {
                if (!systemDBCacheTimeOut.HasValue)
                {
                    int minutes = 0;
                    if (!int.TryParse(ConfigurationManager.AppSettings["SystemDBCacheTimeOut"], out minutes))
                    {
                        minutes = 120;
                    }

                    systemDBCacheTimeOut = TimeSpan.FromMinutes(minutes);
                }

                return systemDBCacheTimeOut.Value;
            }
        }

        /// <summary>
        /// ClientDBCacheTimeOut
        /// </summary>
        /// <value>The client database cache time out.</value>
        public static TimeSpan ClientDBCacheTimeOut
        {
            get
            {
                if (!clientDBCacheTimeOut.HasValue)
                {
                    int minutes = 0;
                    if (!int.TryParse(ConfigurationManager.AppSettings["ClientDBCacheTimeOut"], out minutes))
                    {
                        minutes = 120;
                    }

                    clientDBCacheTimeOut = TimeSpan.FromMinutes(minutes);
                }

                return clientDBCacheTimeOut.Value;
            }
        }

        /// <summary>
        /// GetAllConnectionStringsFromConfig
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> GetAllConnectionStringsFromConfig()
        {
            List<string> connections = new List<string>();
            foreach (ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings)
            {
                if (!(connectionString.Name.ToLower() == "localsqlserver" || connectionString.Name.ToLower() == "systemdb" || connectionString.Name.ToLower() == "usverticalmarketdbinstance"))
                {
                    connections.Add(connectionString.Name);
                }
            }
            return connections;
        }


        /// <summary>
        /// SMTP
        /// </summary>
        /// <value>The SMTP.</value>
        public static string SMTP
        {
            get
            {
                if (string.IsNullOrEmpty(smtp))
                {
                    smtp = ConfigurationManager.AppSettings["SMTP"];
                }

                return smtp;
            }
        }

        /// <summary>
        /// EmailFrom
        /// </summary>
        /// <value>The email from.</value>
        public static string EmailFrom
        {
            get
            {
                if (string.IsNullOrEmpty(emailFrom))
                {
                    emailFrom = ConfigurationManager.AppSettings["EmailFrom"];
                }

                return emailFrom;
            }
        }

        /// <summary>
        /// ErrorEmailTo
        /// </summary>
        /// <value>The error email to.</value>
        public static string ErrorEmailTo
        {
            get
            {
                if (string.IsNullOrEmpty(errorEmailTo))
                {
                    errorEmailTo = ConfigurationManager.AppSettings["ErrorEmailTo"];
                }

                return errorEmailTo;
            }
        }

        /// <summary>
        /// RequestMaterialEmailFrom
        /// </summary>
        /// <value>The request material email from.</value>
        public static string RequestMaterialEmailFrom
        {
            get
            {
                if (string.IsNullOrEmpty(requestMaterialEmailFrom))
                {
                    requestMaterialEmailFrom = ConfigurationManager.AppSettings["RequestMaterialEmailFrom"];
                }

                return requestMaterialEmailFrom;
            }
        }

        /// <summary>
        /// PrintRequestLogFileDir
        /// </summary>
        /// <value>The print request log file dir.</value>
        public static string PrintRequestLogFileDir
        {
            get
            {
                if (string.IsNullOrEmpty(printRequestLogFileDir))
                {
                    printRequestLogFileDir = ConfigurationManager.AppSettings["PrintRequest_LogFileDir"];
                }

                return printRequestLogFileDir;
            }
        }

        /// <summary>
        /// PrintRequestDropFileDir
        /// </summary>
        /// <value>The print request drop file dir.</value>
        public static string PrintRequestDropFileDir
        {
            get
            {
                if (string.IsNullOrEmpty(printRequestDropFileDir))
                {
                    printRequestDropFileDir = ConfigurationManager.AppSettings["PrintRequest_DropFileDir"];
                }

                return printRequestDropFileDir;
            }
        }

        /// <summary>
        /// Environment
        /// </summary>
        /// <value>The environment.</value>
        public static string Environment
        {
            get
            {
                if (string.IsNullOrEmpty(environment))
                {
                    environment = ConfigurationManager.AppSettings["PrintRequest_Environment"];
                }

                return environment;
            }
        }

        /// <summary>
        /// ReportContentEmailFrom
        /// </summary>
        /// <value>The report content email from.</value>
        public static string ReportContentEmailFrom
        {
            get
            {
                if (string.IsNullOrEmpty(reportContentEmailFrom))
                {
                    reportContentEmailFrom = ConfigurationManager.AppSettings["ReportContentEmailFrom"];
                }

                return reportContentEmailFrom;
            }
        }

        /// <summary>
        /// HostedEngineURL
        /// </summary>
        /// <value>HostedEngineURL</value>
        public static string HostedEngineURL
        {
            get
            {
                if (string.IsNullOrEmpty(hostedEngineURL))
                {
                    hostedEngineURL = ConfigurationManager.AppSettings["HostedEngineURL"];
                }

                return hostedEngineURL;
            }
        }

        /// <summary>
        /// HostedEngineURL
        /// </summary>
        /// <value>HostedEngineURL</value>
        public static string HTTPSHostedEngineURL
        {
            get
            {
                if (string.IsNullOrEmpty(hTTPSHostedEngineURL))
                {
                    hTTPSHostedEngineURL = ConfigurationManager.AppSettings["HTTPSHostedEngineURL"];
                }

                return hTTPSHostedEngineURL;
            }
        }        
        /// <summary>
        /// CUSIPMergerLiqudationEmailTo
        /// </summary>
        /// <value>The LiquidatedMergedReport email from.</value>
        public static string CUSIPMergerLiqudationEmailTo
        {
            get
            {
                if (string.IsNullOrEmpty(cusipMergerLiqudationEmailTo))
                {
                    cusipMergerLiqudationEmailTo = ConfigurationManager.AppSettings["CUSIPMergerLiqudationEmailTo"];
                }

                return cusipMergerLiqudationEmailTo;
            }
        }

        /// <summary>
        /// ValidationServiceExceptionEmailTo
        /// </summary>
        /// <value>ValidationServiceExceptionEmailTo.</value>
        public static string ValidationServiceExceptionEmailTo
        {
            get
            {
                if (string.IsNullOrEmpty(validationServiceExceptionEmailTo))
                {
                    validationServiceExceptionEmailTo = ConfigurationManager.AppSettings["ValidationServiceExceptionEmailTo"];
                }

                return validationServiceExceptionEmailTo;
            }
        }

        /// <summary>
        /// AppEnvironment
        /// </summary>
        /// <value>AppEnvironment.</value>
        public static string AppEnvironment
        {
            get
            {
                if (string.IsNullOrEmpty(appEnvironment))
                {
                    appEnvironment = ConfigurationManager.AppSettings["AppEnvironment"];
                }

                return appEnvironment;
            }
        }
        /// <summary>
        /// CUSIPMergerLiqudationEmailSub
        /// </summary>
        /// <value>CUSIPMergerLiqudationEmailSub.</value>
        public static string CUSIPMergerLiqudationEmailSub
        {
            get
            {
                if (string.IsNullOrEmpty(cusipMergerLiqudationEmailSub))
                {
                    cusipMergerLiqudationEmailSub = ConfigurationManager.AppSettings["CUSIPMergerLiqudationEmailSub"];
                }

                return cusipMergerLiqudationEmailSub;
            }
        }
        
        /// <summary>
        /// verticalDataImport_ExcelImport_CommandTimeout
        /// </summary>
        /// <value>verticalDataImport_ExcelImport_CommandTimeout</value>
        public static int VerticalDataImport_ExcelImport_CommandTimeout
        {
            get
            {
                if (verticalDataImport_ExcelImport_CommandTimeout == null)
                {
                    verticalDataImport_ExcelImport_CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["VerticalDataImport_ExcelImport_CommandTimeout"]);
                }

                return verticalDataImport_ExcelImport_CommandTimeout.Value;
            }
        }
        
        /// <summary>
        /// VerticalDataImport_ApproveProofing_CommandTimeout
        /// </summary>
        /// <value>VerticalDataImport_ApproveProofing_CommandTimeout</value>
        public static int VerticalDataImport_ApproveProofing_CommandTimeout
        {
            get
            {
                if (verticalDataImport_ApproveProofing_CommandTimeout == null)
                {
                    verticalDataImport_ApproveProofing_CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["VerticalDataImport_ApproveProofing_CommandTimeout"]);
                }

                return verticalDataImport_ApproveProofing_CommandTimeout.Value;
            }
        }

    }
}
