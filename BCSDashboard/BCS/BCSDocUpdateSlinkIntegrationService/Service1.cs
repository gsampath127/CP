using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using System.Timers;
using System.Configuration.Install;
using System.Threading;
using System.IO;


namespace BCSDocUpdateSlinkIntegrationService
{
    public partial class Service1 : ServiceBase
    {

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //StatusUpdateTimer.Interval = Convert.ToDouble(ConfigValues.TimerInterval);
            //StatusUpdateTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.StatusUpdateTimerJob);
            //StatusUpdateTimer.Start();

            System.Threading.ThreadStart job = new System.Threading.ThreadStart(StatusUpdateTimerJob);
            System.Threading.Thread thread = new System.Threading.Thread(job);
            thread.Start();

            // Job to create Doc Update File and Slink File. Upload Doc update File and Slink File
            System.Threading.ThreadStart DocUpdateandPreflightJob = new System.Threading.ThreadStart(this.FileUploadJob);
            System.Threading.Thread FileUploadJobthread = new System.Threading.Thread(DocUpdateandPreflightJob);
            FileUploadJobthread.Start();


            // Job to create IPDocUpdate file at specific time
            System.Threading.ThreadStart IPDocUpdateJobThreadStart = new System.Threading.ThreadStart(this.IPDocUpdateJob);
            System.Threading.Thread IPDocUpdateJobThread = new System.Threading.Thread(IPDocUpdateJobThreadStart);
            IPDocUpdateJobThread.Start();


            Logging.LogToFile("BCS.BCSDocUpdateSlinkIntegrationService Service Started " + DateTime.Now);


            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
            {
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, Process.GetCurrentProcess().ProcessName, "BCS.BCSDocUpdateSlinkIntegrationService Service Started " + ConfigValues.AppEnvironment, "support", null);
            }
        }

        protected override void OnStop()
        {
            Logging.LogToFile("BCS.BCSDocUpdateSlinkIntegrationService Service Stopped " + DateTime.Now);


            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
            {
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, Process.GetCurrentProcess().ProcessName, "BCS.BCSDocUpdateSlinkIntegrationService Service Stopped " + ConfigValues.AppEnvironment, "support", null);
            }
        }

        public void StatusUpdateTimerJob()
        {
            while (true)
            {

                try
                {
                    //System.Threading.ThreadStart job = new System.Threading.ThreadStart(StatusUpdateJob);
                    //System.Threading.Thread thread = new System.Threading.Thread(job);
                    //thread.Start();               
                    StatusUpdateJob();
                    Thread.Sleep(Convert.ToInt32(ConfigValues.TimerInterval));

                }
                catch (Exception ex)
                {
                    string ErrorEmailBody = "Error has occured during execution of BCSDocUpdateSlinkIntegrationService. StatusUpdateTimerJob - Error message: " + ex.Message;

                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "support", null);
                    }

                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }

            }
        }

        public void StatusUpdateJob()
        {
            try
            {
                ServiceFactory servicefactory = new ServiceFactory();

                BCSClient bcsclient = servicefactory.GetGIMClientConfigs();

                DateTime currentdatetime = DateTime.Now;


                //Upload Slink Files And Update the export status. Commented for now
                //servicefactory.UploadSlinkForPreflightAndUpdateExportedStatus(bcsclient);

                //Update Automated preflight file status
                servicefactory.CheckAndprocessPreflightStatusFiles(bcsclient);
                //Look for DAS Status File and Update status.
                //servicefactory.CheckAndprocessDASStatusFiles(bcsclient);

            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error has occured during execution of BCSDocUpdateSlinkIntegrationService. StatusUpdateJob Error message: " + expt.Message;

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }

        }

        public void FileUploadJob()
        {
            while (true)
            {
                try
                {
                    ServiceFactory servicefactory = new ServiceFactory();



                    DateTime currentdatetime = DateTime.Now;

                    DateTime TimeFromRunSLinkDocUpdate = Convert.ToDateTime(ConfigValues.TimeFromRunSLinkDocUpdate);
                    DateTime TimeToRunSLinkDocUpdate = Convert.ToDateTime(ConfigValues.TimeToRunSLinkDocUpdate);

                    DateTime TimeFromRunPreflight = Convert.ToDateTime(ConfigValues.TimeFromRunPreflight);
                    DateTime TimeToRunPreflight = Convert.ToDateTime(ConfigValues.TimeToRunPreflight);






                    if (!currentdatetime.DayOfWeek.Equals(DayOfWeek.Saturday)  //Service is running in central time and friday CST will be saturday EST and saturday CST will be Sunday EST
                           && !currentdatetime.DayOfWeek.Equals(DayOfWeek.Sunday)
                           && (currentdatetime >= TimeFromRunPreflight
                                   && currentdatetime <= TimeToRunPreflight
                                   )
                       )
                    {
                        BCSClient bcsclient = servicefactory.GetGIMClientConfigs();


                        //Upload Slink Files And Update the export status
                        servicefactory.UploadSlinkForPreflightAndUpdateExportedStatus(bcsclient);
                        Thread.Sleep(Convert.ToInt32(ConfigValues.SleepTimeAfterResettingClients));
                    }



                    if (!currentdatetime.DayOfWeek.Equals(DayOfWeek.Sunday)
                            && !currentdatetime.DayOfWeek.Equals(DayOfWeek.Monday)
                            && (currentdatetime >= TimeFromRunSLinkDocUpdate
                                    && currentdatetime <= TimeToRunSLinkDocUpdate
                                    )
                        )
                    {
                        BCSClient bcsclient = servicefactory.GetGIMClientConfigs();


                        List<string> docupdatefiles = new List<string>();

                        //Generate Doc Update File
                        docupdatefiles = servicefactory.GenerateDocUpdateFile(bcsclient);
                        StringBuilder EmailBody = new StringBuilder();

                        int docUpdtUploadedCount = 0;
                        foreach (string docupdatefile in docupdatefiles)
                        {
                            try
                            {
                                if (UtilityFactory.Upload(bcsclient.DocUpdateStatusFilesSANPath + @"DocUpdate\" + docupdatefile,
                                       bcsclient.DocUpdateMetadataDropFTPPath,
                                       bcsclient.DocUpdateMetaDataUserName,
                                       bcsclient.DocUpdateMetaDataPassword
                                       )
                                    )
                                {
                                    //Change the style of this email to include both doc update file details in one email 
                                    EmailBody.Append("DAS Meta Data File Successfully uploaded to FTP:   " + docupdatefile + DateTime.Now.ToString());
                                    EmailBody.Append("\n");

                                    docUpdtUploadedCount++;                                    
                                }
                            }
                            catch (Exception exception)
                            {
                                string ErrorEmailBody = "Error has occured during execution of BCSDocUpdateSlinkIntegrationService. FileUploadJob Error message: FTP failed for "
                                                        + docupdatefile
                                                        + " Exception is " + exception;

                                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                                {
                                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "support", null);
                                }
                            }

                        }

                        if(docUpdtUploadedCount ==  2)
                        {
                            servicefactory.SaveBCSDocUpdateFileHistory(bcsclient.ClientId);
                        }

                        foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSDocUpdateFileUploadedEmailSub, EmailBody.ToString(), "support", null);
                        }



                        //Send separate doc update files to clients who need it - Start

                        CreateSeparateDocUpdateFilesForClients();

                        //Send separate doc update files to clients who need it - End 
                                                

                        Thread.Sleep(Convert.ToInt32(ConfigValues.SleepTimeAfterResettingClients));
                    }

                    Thread.Sleep(60000);

                }
                catch (Exception expt)
                {
                    string ErrorEmailBody = "Error has occured during execution of BCSDocUpdateSlinkIntegrationService. FileUploadJob Error message: " + expt.Message;

                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "support", null);
                    }

                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }
            }
        }

        public void CreateSeparateDocUpdateFilesForClients()
        {
            ServiceFactory servicefactory = new ServiceFactory();

            List<BCSClient> bcsDocUpdateClients = servicefactory.GetDocUpdateClientConfigs();

            foreach (BCSClient bcsclient in bcsDocUpdateClients)
            {
                List<string> docupdatefiles = new List<string>();

                //Generate Doc Update File
                docupdatefiles = servicefactory.GenerateDocUpdateFile(bcsclient);
                StringBuilder EmailBody = new StringBuilder();

                int docUpdtUploadedCount = 0;

                foreach (string docupdatefile in docupdatefiles)
                {
                    try
                    {
                        if (UtilityFactory.Upload(bcsclient.DocUpdateStatusFilesSANPath + @"IPDocUpdate\" + bcsclient.ClientName + @"\"  + docupdatefile,
                               bcsclient.DocUpdateMetadataDropFTPPath,
                               bcsclient.DocUpdateMetaDataUserName,
                               bcsclient.DocUpdateMetaDataPassword
                               )
                            )
                        {
                            //Change the style of this email to include both doc update file details in one email 
                            EmailBody.Append(bcsclient.ClientName + " Meta Data File Successfully uploaded to FTP :   " + docupdatefile + DateTime.Now.ToString());
                            EmailBody.Append("\n");

                            docUpdtUploadedCount++;
                        }
                    }
                    catch (Exception exception)
                    {
                        string ErrorEmailBody = "Error has occured during execution of BCSDocUpdateSlinkIntegrationService. CreateSeparateDocUpdateFilesForClients Error message: FTP failed for "
                                                + docupdatefile
                                                + " Exception is " + exception;

                        foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "support", null);
                        }

                        System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                    }

                }
                if (docUpdtUploadedCount == 2)
                {
                    servicefactory.SaveBCSDocUpdateFileHistory(bcsclient.ClientId);
                }

                foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSDocUpdateFileUploadedEmailSub, EmailBody.ToString(), "support", null);
                }
            }
        }


        public void IPDocUpdateJob()
        {
            while (true)
            {
                try
                {

                    GenerateIPDocUpdateFileUsingCUSIPWatchList();                   

                    Thread.Sleep(Convert.ToInt32(ConfigValues.TimerInterval));

                }
                catch (Exception ex)
                {
                    string ErrorEmailBody = "Error has occured during execution of BCSDocUpdateSlinkIntegrationService - IPDocUpdateJob. Error message: " + ex.Message;

                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "support", null);
                    }

                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }
            }
        }

        public void GenerateIPDocUpdateFileUsingCUSIPWatchList()
        {
            ServiceFactory servicefactory = new ServiceFactory();

            List<BCSClientIPFileConfigDetails> clientPrefixs = servicefactory.GetClientsForIPFileToBeSent();

            if (clientPrefixs.Count > 0)
            {
                List<BCSClient> bcsDocUpdateClients = servicefactory.GetALLClientConfig();

                foreach (BCSClientIPFileConfigDetails IPFileConfigDetails in clientPrefixs)
                {
                    BCSClient bcsclient = bcsDocUpdateClients.Find(x => x.ClientPrefix == IPFileConfigDetails.ClientPrefix);

                    //Generate Doc Update File
                    string IPDocUpdateFileName = servicefactory.GenerateIPDocUpdateFileUsingCUSIPWatchList(bcsclient, IPFileConfigDetails);
                    StringBuilder EmailBody = new StringBuilder();

                    try
                    {
                        if (UtilityFactory.Upload(bcsclient.DocUpdateStatusFilesSANPath + @"IPDocUpdate\" + bcsclient.ClientPrefix + @"\" + IPDocUpdateFileName,
                                bcsclient.DocUpdateMetadataDropFTPPath,
                                bcsclient.DocUpdateMetaDataUserName,
                                bcsclient.DocUpdateMetaDataPassword
                                )
                            )
                        {
                            //Change the style of this email to include both doc update file details in one email 
                            EmailBody.Append(bcsclient.ClientName + " IP Meta Data File Successfully uploaded to FTP :   " + IPDocUpdateFileName + " - " + DateTime.Now.ToString());
                            EmailBody.Append("<br/>");

                            servicefactory.SaveBCSDocUpdateFileHistory(bcsclient.ClientId);
                        }


                        if (IPFileConfigDetails.NeedIPConfirmationFile)
                        {
                            string IPConfirmationFileName = IPDocUpdateFileName + ".tag";
                            if (!Directory.Exists(bcsclient.DocUpdateStatusFilesSANPath + @"IPDocUpdate\" + bcsclient.ClientPrefix + @"\ConfirmationFile\"))
                            {
                                Directory.CreateDirectory(bcsclient.DocUpdateStatusFilesSANPath + @"IPDocUpdate\" + bcsclient.ClientPrefix + @"\ConfirmationFile\");
                            }

                            using (StreamWriter tws = new StreamWriter(bcsclient.DocUpdateStatusFilesSANPath + @"IPDocUpdate\" + bcsclient.ClientPrefix + @"\ConfirmationFile\" + IPConfirmationFileName, true))
                            {
                                tws.Close();
                            }

                            if (UtilityFactory.Upload(bcsclient.DocUpdateStatusFilesSANPath + @"IPDocUpdate\" + bcsclient.ClientPrefix + @"\ConfirmationFile\" + IPConfirmationFileName,
                                IPFileConfigDetails.IPConfirmationFileDropFTPPath,
                                IPFileConfigDetails.IPConfirmationFileDropFTPUserName,
                                IPFileConfigDetails.IPConfirmationFileDropFTPPassword
                                )
                            )
                            {
                                EmailBody.Append("<br/>");
                                EmailBody.Append(bcsclient.ClientName + " IP ConfirmationFile File Successfully uploaded to FTP :   " + IPConfirmationFileName + " - " + DateTime.Now.ToString());
                                EmailBody.Append("<br/>");
                            }
                        }

                        foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSDocUpdateFileUploadedEmailSub, EmailBody.ToString(), "support", null);
                        }
                    }
                    catch (Exception exception)
                    {
                        string ErrorEmailBody = "Error has occured during execution of BCSDocUpdateSlinkIntegrationService -  GenerateIPDocUpdateFileUsingCUSIPWatchList. Error message: FTP failed for "
                                                + IPDocUpdateFileName
                                                + " Exception is " + exception;

                        foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "support", null);
                        }

                        System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                    }
                }
            }
        }
    }

    [RunInstallerAttribute(true)]
    public partial class Installer1 : Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller processInstaller;

        public Installer1()
        {

            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            // Service will run under system account
            processInstaller.Account = ServiceAccount.LocalSystem;

            // Service will have Start Type of Manual
            serviceInstaller.StartType = ServiceStartMode.Manual;

            serviceInstaller.ServiceName = "BCS.DocUpdateSlinkIntegrationService Service";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);

        }

    }
}
