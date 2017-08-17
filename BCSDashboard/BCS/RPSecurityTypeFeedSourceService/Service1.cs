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
using System.Threading.Tasks;

namespace RPSecurityTypeFeedSourceService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ThreadStart RPSecurityTypeFeedSourceJob = new ThreadStart(RunRPSecurityTypeFeedProcess);
            Thread RPSecurityTypeFeedSourceJobThread = new Thread(RPSecurityTypeFeedSourceJob);
            RPSecurityTypeFeedSourceJobThread.Start();

            Logging.LogToFile("RP.SecurityTypeFeedSourceService STARTED At " + DateTime.Now);

            UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, Process.GetCurrentProcess().ProcessName, 
                                        "RP.SecurityTypeFeedSourceService Service Started " + ConfigValues.AppEnvironment, "support", null);
            
        }

        protected override void OnStop()
        {
            Logging.LogToFile("RP.SecurityTypeFeedSourceService STOPPED At " + DateTime.Now);
            
            UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, Process.GetCurrentProcess().ProcessName,
                                        "RP.SecurityTypeFeedSourceService Service Stopped " + ConfigValues.AppEnvironment, "support", null);
            
        }

        public void RunRPSecurityTypeFeedProcess()
        {
            while (true)
            {
                try
                {
                    RPSecurityTypeFeedFactory securityTypeFeedFactory = new RPSecurityTypeFeedFactory();
                    securityTypeFeedFactory.PickUpStoreAndDeleteEdgarOnlineFeedFile();                    
                }
                catch (Exception expt)
                {
                    string ErrorEmailBody = "Error in RP.SecurityTypeFeedSourceService in function RunRPSecurityTypeFeedProcess - PickUpStoreAndDeleteEdgarOnlineFeedFile : " + expt.ToString();
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);

                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }

                try
                {
                    RPSecurityTypeFeedFactory securityTypeFeedFactory = new RPSecurityTypeFeedFactory();
                    securityTypeFeedFactory.ProcessEdgarOnlineFeedFile();
                }
                catch (Exception expt)
                {
                    string ErrorEmailBody = "Error in RP.SecurityTypeFeedSourceService in function RunRPSecurityTypeFeedProcess - ProcessEdgarOnlineFeedFile -  : " + expt.ToString();
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);

                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }

                Thread.Sleep(Convert.ToInt32(ConfigValues.TimerInterval));
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

            serviceInstaller.ServiceName = "RP.SecurityTypeFeedSourceService";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);

        }

    }
}
