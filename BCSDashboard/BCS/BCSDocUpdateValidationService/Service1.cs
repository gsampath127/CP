using BCS.ObjectModel.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace BCSDocUpdateValidationService
{
    public partial class Service1 : ServiceBase
    {

        private static string smtp = ConfigValues.SMTP;
        private static string LogDirectory = ConfigValues.LogDirectory;

        private static string EmailFrom = ConfigValues.EmailFrom;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            Logging.LogToFile("BCS.BCSDocUpdateValidationService Service Started " + DateTime.Now);


            //1. Fill the BCSDocUpdate Table with filings that have been filed but not yet processed
            System.Threading.ThreadStart unprocessedFilings = new System.Threading.ThreadStart(ProcessDocUpdatesForFilingsPendingToBeProcessed);
            System.Threading.Thread unprocessedFilingsThread = new System.Threading.Thread(unprocessedFilings);
            unprocessedFilingsThread.Start();

            //2. Check for any cusip that has merged by looking into the Prospectus table.
            //3. Add new CUSIPs for existing funds
            System.Threading.ThreadStart CUSIPDetails = new System.Threading.ThreadStart(ProcessNewlyAddedorModifiedCUSIPDetails);
            System.Threading.Thread CUSIPDetailsThread = new System.Threading.Thread(CUSIPDetails);
            CUSIPDetailsThread.Start();


            //4. Check for any cusip that is not in the Prosticker table and update the IsRemoved=1
            //5. Query and Find duplicates
            //6. If duplicates exists,send a notifications to operations team with the link to the UI.
            //7. Configure the email address and the Link to the UI



            System.Threading.ThreadStart validation = new System.Threading.ThreadStart(processDocUpdateValidation);
            System.Threading.Thread validationThread = new System.Threading.Thread(validation);
            validationThread.Start();


            //8. UpdateDequeueStatusForNewAddedCUSIPS

            System.Threading.ThreadStart updateDequeueStatusForNewAddedCUSIPS = new System.Threading.ThreadStart(UpdateDequeueStatusForNewAddedCUSIPS);
            System.Threading.Thread updateDequeueStatusForNewAddedCUSIPSThread = new System.Threading.Thread(updateDequeueStatusForNewAddedCUSIPS);
            updateDequeueStatusForNewAddedCUSIPSThread.Start();


            //9. GetAllOlderSPInFLMode and replace with P records

            //System.Threading.ThreadStart getAllOlderSPInFLModeAndReplaceWithP = new System.Threading.ThreadStart(GetAllOlderSPInFLModeAndReplaceWithP);
            //System.Threading.Thread getAllOlderSPInFLModeAndReplaceWithPThread = new System.Threading.Thread(getAllOlderSPInFLModeAndReplaceWithP);
            //getAllOlderSPInFLModeAndReplaceWithPThread.Start();


            //11. Fill the BCSDocUpdateARSAR Table with filings that have been filed but not yet processed
            System.Threading.ThreadStart unprocessedFilingsARSAR = new System.Threading.ThreadStart(ProcessARSARFilingsPendingToBeProcessed);
            System.Threading.Thread unprocessedFilingsARSARThread = new System.Threading.Thread(unprocessedFilingsARSAR);
            unprocessedFilingsARSARThread.Start();


            //12. Fill the BCSDocUpdateARSAR Table with filings that have been filed but not yet processed
            System.Threading.ThreadStart updateAPCOPCReceivedDate = new System.Threading.ThreadStart(UpdateAPCOPCReceivedDate);
            System.Threading.Thread updateAPCOPCReceivedDateThread = new System.Threading.Thread(updateAPCOPCReceivedDate);
            updateAPCOPCReceivedDateThread.Start();



            //13. BCS InLine (All Customer)– WatchList and Doc Update– Failure Notification 
            System.Threading.ThreadStart failureNotification = new System.Threading.ThreadStart(SendFailureNotification);
            System.Threading.Thread failureNotificationThread = new System.Threading.Thread(failureNotification);
            failureNotificationThread.Start();


            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
            {
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, Process.GetCurrentProcess().ProcessName, "BCS.BCSDocUpdateValidationService  Started " + ConfigValues.AppEnvironment, "support", null);
            }

        }

        private void SendFailureNotification()
        {
            while (true)
            {
                try
                {
                    ServiceFactory serviceFactory = new ServiceFactory();

                    serviceFactory.SendFailureNotification();

                    Thread.Sleep(120000); //2 min
                }
                catch (Exception ex)
                {
                    (new Logging()).SaveExceptionLog("SendFailureNotification : " + ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }
            }
        }

        private void UpdateAPCOPCReceivedDate()
        {
            while (true)
            {
                try
                {
                    ServiceFactory serviceFactory = new ServiceFactory();

                    serviceFactory.UpdateAPCOPCReceivedDate();

                    Thread.Sleep(Convert.ToInt32(ConfigValues.UpdateAPCOPCReceivedDateTimeInterval));
                }
                catch (Exception ex)
                {
                    (new Logging()).SaveExceptionLog("UpdateAPCOPCReceivedDate : " + ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }
            }
        }

        private void GetAllOlderSPInFLModeAndReplaceWithP()
        {
            int SleepTimeAfterResettingClients = Convert.ToInt32(ConfigValues.SleepTimeAfterResettingClients);
            TimeSpan TimeFromGetAllOlderSPInFLModeAndReplaceWithP = System.TimeSpan.Parse(ConfigValues.TimeFromGetAllOlderSPInFLModeAndReplaceWithP);
            TimeSpan TimeToGetAllOlderSPInFLModeAndReplaceWithP = System.TimeSpan.Parse(ConfigValues.TimeToGetAllOlderSPInFLModeAndReplaceWithP);


            while (true)
            {
                try
                {
                    DateTime currenttime = DateTime.Now;

                    if (!currenttime.DayOfWeek.Equals(DayOfWeek.Saturday) && !currenttime.DayOfWeek.Equals(DayOfWeek.Sunday)
                        && currenttime.TimeOfDay >= TimeFromGetAllOlderSPInFLModeAndReplaceWithP && currenttime.TimeOfDay <= TimeToGetAllOlderSPInFLModeAndReplaceWithP)
                    {
                        new ServiceFactory().GetAllOlderSPInFLModeAndReplaceWithP();
                        Thread.Sleep(SleepTimeAfterResettingClients);
                    }

                    Thread.Sleep(20000);
                }
                catch (Exception ex)
                {
                    (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }
            }

        }



        private void UpdateDequeueStatusForNewAddedCUSIPS()
        {
            int SleepTimeAfterResettingClients = Convert.ToInt32(ConfigValues.SleepTimeAfterResettingClients);
            TimeSpan TimeFromUpdateDequeueStatusForNewAddedCUSIPS = System.TimeSpan.Parse(ConfigValues.TimeFromUpdateDequeueStatusForNewAddedCUSIPS);
            TimeSpan TimeToUpdateDequeueStatusForNewAddedCUSIPS = System.TimeSpan.Parse(ConfigValues.TimeToUpdateDequeueStatusForNewAddedCUSIPS);


            while (true)
            {
                try
                {
                    DateTime currenttime = DateTime.Now;

                    if (!currenttime.DayOfWeek.Equals(DayOfWeek.Saturday) && !currenttime.DayOfWeek.Equals(DayOfWeek.Sunday)
                        && currenttime.TimeOfDay >= TimeFromUpdateDequeueStatusForNewAddedCUSIPS && currenttime.TimeOfDay <= TimeToUpdateDequeueStatusForNewAddedCUSIPS)
                    {                        

                        new ServiceFactory().UpdateDequeueStatusForNewAddedCUSIPS();                        
                        
                        Thread.Sleep(SleepTimeAfterResettingClients);
                    }

                    Thread.Sleep(20000);
                }
                catch (Exception ex)
                {
                    (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }
            }

        }

        private void ProcessDocUpdatesForFilingsPendingToBeProcessed()
        {


            int SleepTimeAfterResettingClients = Convert.ToInt32(ConfigValues.SleepTimeAfterResettingClients);
            TimeSpan TimeFromResetFilingsPendingToBeProcessed = System.TimeSpan.Parse(ConfigValues.TimeFromResetFilingsPendingToBeProcessed);
            TimeSpan TimeToResetFilingsPendingToBeProcessed = System.TimeSpan.Parse(ConfigValues.TimeToResetFilingsPendingToBeProcessed);


            while (true)
            {
                try
                {
                    DateTime currenttime = DateTime.Now;

                    if (currenttime.TimeOfDay >= TimeFromResetFilingsPendingToBeProcessed && currenttime.TimeOfDay <= TimeToResetFilingsPendingToBeProcessed)
                    {
                        ServiceFactory serviceFactory = new ServiceFactory();

                        
                        serviceFactory.ProcessDocUpdatesForFilingsPendingToBeProcessed(); 
                                               
                        serviceFactory.CheckBCSDocUpdateValidations();

                        serviceFactory.RemoveCUSIPsFromFLTableNotinFLMode();

                        Thread.Sleep(SleepTimeAfterResettingClients);
                    }

                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }
            }

        }


        private void ProcessARSARFilingsPendingToBeProcessed()
        {
            int SleepTimeAfterResettingClients = Convert.ToInt32(ConfigValues.SleepTimeAfterResettingClients);
            TimeSpan TimeFromARSARFilingsPendingToBeProcessed = System.TimeSpan.Parse(ConfigValues.TimeFromARSARFilingsPendingToBeProcessed);
            TimeSpan TimeToARSARFilingsPendingToBeProcessed = System.TimeSpan.Parse(ConfigValues.TimeToARSARFilingsPendingToBeProcessed);


            while (true)
            {
                try
                {
                    DateTime currenttime = DateTime.Now;

                    if (currenttime.TimeOfDay >= TimeFromARSARFilingsPendingToBeProcessed && currenttime.TimeOfDay <= TimeToARSARFilingsPendingToBeProcessed)
                    {
                        new ServiceFactory().ProcessARSARFilingsPendingToBeProcessed();
                        Thread.Sleep(SleepTimeAfterResettingClients);
                    }

                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    (new Logging()).SaveExceptionLog("ProcessARSARFilingsPendingToBeProcessed : " + ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }
            }

        }

        public void processDocUpdateValidation()
        {
            while (true)
            {
                try
                {
                    ServiceFactory serviceFactory = new ServiceFactory();

                    

                    serviceFactory.SendBCSDocUpdateValidationReport();

                    Thread.Sleep(1200000);
                }
                catch (Exception ex)
                {
                    (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }
            }

        }

        private void ProcessNewlyAddedorModifiedCUSIPDetails()
        {
            try
            {

                int SleepTimeAfterResettingClients = Convert.ToInt32(ConfigValues.SleepTimeAfterResettingClients);
                TimeSpan TimeFromProcessNewlyAddedOrModifiedCUSIP = System.TimeSpan.Parse(ConfigValues.TimeFromProcessNewlyAddedOrModifiedCUSIP);
                TimeSpan TimeToProcessNewlyAddedOrModifiedCUSIP = System.TimeSpan.Parse(ConfigValues.TimeToProcessNewlyAddedOrModifiedCUSIP);
                DateTime currenttime = DateTime.Now;


                while (true)
                {

                    if (currenttime.TimeOfDay >= TimeFromProcessNewlyAddedOrModifiedCUSIP && currenttime.TimeOfDay <= TimeToProcessNewlyAddedOrModifiedCUSIP)
                    {                        

                        ServiceFactory serviceFactory = new ServiceFactory();
                        serviceFactory.ProcessNewlyAddedorModifiedCUSIPDetails();

                        serviceFactory.CheckBCSDocUpdateValidations();

                        Thread.Sleep(SleepTimeAfterResettingClients);
                    }

                    Thread.Sleep(2000);

                }
            }
            catch (Exception ex)
            {
                (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
            }
        }
        protected override void OnStop()
        {
            Logging.LogToFile("BCS.BCSDocUpdateValidationService Service Stopped. " + DateTime.Now);

            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
            {
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, Process.GetCurrentProcess().ProcessName, "BCS.BCSDocUpdateValidationService  Stopped " + ConfigValues.AppEnvironment, "support", null);
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

            serviceInstaller.ServiceName = "BCS.BCSDocUpdateValidationService";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);

        }

    }
}
