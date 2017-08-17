using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Text;
using BCS.ObjectModel.Factories;

namespace BCSDocumentSynchonizerService
{
    public partial class Service1 : ServiceBase
    {
       

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            int TotalThreads = ConfigValues.DequeueURLToDownloadThreads;

            string ProcessName = Process.GetCurrentProcess().ProcessName;

            for (int threadcounter = 1; threadcounter <= TotalThreads; threadcounter++)
            {
                Thread myNewThread = new Thread(() => new ServiceFactory().DequeueAndProcessURLToDownloadJob(threadcounter));                
                myNewThread.Start();

                Logging.LogToFile(ProcessName + " Service Started " + DateTime.Now + " Thread" + threadcounter.ToString());
            }

            ThreadStart archivejob = new ThreadStart(new ServiceFactory().ArchiveCompletedBCSDocSynchronizerQueues);
            Thread archivethread = new Thread(archivejob);
            archivethread.Start();

            Logging.LogToFile("BCSDocumentSynchonizerService ArchiveJob Thread Started ------------------------------");

            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
            {
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                    ProcessName, ProcessName + " Service Started " + ConfigValues.AppEnvironment, "support", null);
            }
        }

        protected override void OnStop()
        {
            string ProcessName = Process.GetCurrentProcess().ProcessName;

            Logging.LogToFile(ProcessName + " Service Stopped " + DateTime.Now);


            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
            {
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, 
                        ProcessName, ProcessName + " Service Stopped " + ConfigValues.AppEnvironment, "support", null);
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

            serviceInstaller.ServiceName = "BCS.DocumentSynchronizer Service";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);

        }

    }
}
