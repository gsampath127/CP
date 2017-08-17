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
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;

namespace BCSFLTIntegrationService
{
    public partial class Service1 : ServiceBase
    {

        private static BCSFLTandFTPLogicFactory fltandftpLogicfactory = new BCSFLTandFTPLogicFactory();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
           
            //Job to fetch FTP files
            System.Threading.ThreadStart FetchFTPFilesAndUpdateDocuploadStagingTableJob = new System.Threading.ThreadStart(FetchFTPFilesAndUpdateDocuploadStagingTable);
            System.Threading.Thread FetchFTPFilesAndUpdateDocuploadStagingTableThread = new System.Threading.Thread(FetchFTPFilesAndUpdateDocuploadStagingTableJob);
            FetchFTPFilesAndUpdateDocuploadStagingTableThread.Start();

           
           // Job to fetch the current days FLT file and process
           System.Threading.ThreadStart PickUpAndProcessFLTFileJob = new System.Threading.ThreadStart(this.PickUpAndProcessFLTFileForToday);
           System.Threading.Thread PickUpAndProcessFLTFileJobThread = new System.Threading.Thread(PickUpAndProcessFLTFileJob);
           PickUpAndProcessFLTFileJobThread.Start();

           //Make each pdf file ready for doc upload push.
           System.Threading.ThreadStart MakeFTPDocReadyForDocUploadPushJob = new System.Threading.ThreadStart(MakeFTPDocReadyForDocUploadPush);
           System.Threading.Thread MakeFTPDocReadyForDocUploadPushThread = new System.Threading.Thread(MakeFTPDocReadyForDocUploadPushJob);
           MakeFTPDocReadyForDocUploadPushThread.Start();

           //Push each document from staging table to doc upload approval table
           System.Threading.ThreadStart PushFTPDocumentsToDocuploadForApprovalJob = new System.Threading.ThreadStart(PushFTPDocumentsToDocuploadForApproval);
           System.Threading.Thread PushFTPDocumentsToDocuploadForApprovalThread = new System.Threading.Thread(PushFTPDocumentsToDocuploadForApprovalJob);
           PushFTPDocumentsToDocuploadForApprovalThread.Start();

          

            Logging.LogToFile("BCS.BCSFLTIntegrationService Service Started " + DateTime.Now);


            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
            {
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, Process.GetCurrentProcess().ProcessName, "BCS.BCSFLTIntegrationService Service Started " + ConfigValues.AppEnvironment, "support", null);
            }
        }

        protected override void OnStop()
        {

        }


       

       

        #region Process and Archive FLT File

        public void PickUpAndProcessFLTFileForToday()
        {
            while (true)
            {

                try
                {
                              
                    PickUpAndProcessFLTFile();
                    Thread.Sleep(Convert.ToInt32(ConfigValues.TimerInterval));

                }
                catch (Exception ex)
                {
                    string ErrorEmailBody = "Error in PickUpAndProcessFLTFileForToday : " + ex.Message;


                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                    }

                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }

            }
        }

        public static void PickUpAndProcessFLTFile()
        {
            try
            {
                ServiceFactory servicefactory = new ServiceFactory();

                DateTime currentdatetime = DateTime.Now;


                DateTime TimeFromRunFLTFileProcess = Convert.ToDateTime(ConfigValues.TimeFromRunFLTFileProcess);
                DateTime TimeToRunFLTFileProcess = Convert.ToDateTime(ConfigValues.TimeToRunFLTFileProcess);

                if (!currentdatetime.DayOfWeek.Equals(DayOfWeek.Saturday)
                        && !currentdatetime.DayOfWeek.Equals(DayOfWeek.Sunday)                        
                    )
                {
                    List<BCSClient> bcsclients = servicefactory.GetFLTClientConfigs();



                    foreach (BCSClient bcsclient in bcsclients)
                    {
                        // Do this only if FLT is enabled for this client.
                        
                        // Pick Up , Process and Archive the FLT File
                        fltandftpLogicfactory.GetFLTFileDownloadArchiveAndDelete(bcsclient);
                            
                        
                    }

                }

            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSFLTIntegrationService in function PickUpAndUpdateFLTFile : " + expt.Message;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }

        }   

       

        #endregion

        #region Process and Archive FTP Documents

        public void FetchFTPFilesAndUpdateDocuploadStagingTable()
        {
            while (true)
            {

                try
                {
                             
                    PickUpAndArchiveFTPFile();
                    Thread.Sleep(Convert.ToInt32(ConfigValues.TimerInterval));

                }
                catch (Exception ex)
                {
                    string ErrorEmailBody = "Error in BCSFLTIntegrationService in function FetchFTPFilesAndUpdateDocuploadStagingTable : " + ex.Message;
                    Logging.LogToFile(ErrorEmailBody + ex.Message);

                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                    }
                }

            }
        }



        public static void PickUpAndArchiveFTPFile()
        {
            try
            {
                ServiceFactory servicefactory = new ServiceFactory();

                List<BCSClient> bcsclients = servicefactory.GetFLTClientConfigs();

                DateTime currentdatetime = DateTime.Now;

                foreach (BCSClient bcsclient in bcsclients)
                {
                    // Do this only if FLT is enabled for this client.
                    
                    // Pick Up , Process and Archive the FLT File
                    fltandftpLogicfactory.GetFTPFileDownloadArchiveAndDelete(bcsclient);
                        
                    
                }
            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSFLTIntegrationService in function PickUpAndArchiveFTPFile : " + expt.Message;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }
            }

        }       

       

        #endregion

        #region Sweep the documents and compare with FLT Table

        public void MakeFTPDocReadyForDocUploadPush()
        {
            while (true)
            {

                try
                {

                    SweepDocumentsAndCompareWithFLT();
                    Thread.Sleep(Convert.ToInt32(ConfigValues.TimerInterval));

                }
                catch (Exception ex)
                {
                    string ErrorEmailBody = "Error in PickUpAndProcessFLTFileForToday: " + ex.Message;


                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                    }
                }

            }
        }

        public static void SweepDocumentsAndCompareWithFLT()
        {
            try
            {
                ServiceFactory servicefactory = new ServiceFactory();

                List<BCSClient> bcsclients = servicefactory.GetFLTClientConfigs();

                DateTime currentdatetime = DateTime.Now;

                foreach (BCSClient bcsclient in bcsclients)
                {
                    // Do this only if FLT is enabled for this client.
                    
                    // Pick Up , Process and Archive the FLT File
                    fltandftpLogicfactory.PickUPDocumentsAndMakeitDocUploadReady(bcsclient);
                    
                }
            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSFLTIntegrationService in function PickUpAndUpdateFLTFile : " + expt.Message;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }
            }

        }

        #endregion

        #region PDF Work on ready documents push it to Doc Upload

        public void PushFTPDocumentsToDocuploadForApproval()
        {
            while (true)
            {

                try
                {

                    PushFTPDocumentstoDocUpload();
                    Thread.Sleep(Convert.ToInt32(ConfigValues.TimerInterval));

                }
                catch (Exception ex)
                {
                    string ErrorEmailBody = "Error in PushFTPDocumentsToDocuploadForApproval: " + ex.Message;
                    Logging.LogToFile(ErrorEmailBody + ex.Message);


                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                    }
                }

            }
        }

        public static void PushFTPDocumentstoDocUpload()
        {
            try
            {
                ServiceFactory servicefactory = new ServiceFactory();

                List<BCSClient> bcsclients = servicefactory.GetFLTClientConfigs();
                                

                foreach (BCSClient bcsclient in bcsclients)
                {
                    // Do this only if FLT is enabled for this client.
                    
                    // Pick Up , Process and Archive the FLT File
                    fltandftpLogicfactory.PrepareDocAndPushEachOnetoDocUploadUI(bcsclient);
                    
                }
            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSFLTIntegrationService in function PushFTPDocumentstoDocUpload : " + expt.Message;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }
            }

        }

        #endregion


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

            serviceInstaller.ServiceName = "BCS.BCSFLTIntegrationService";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);

        }

    }
}
