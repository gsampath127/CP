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

namespace BCSTRPReportService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logging.LogToFile("BCS.BCSTRPReportService Service Started " + DateTime.Now);


            //1. SEBD BCS TRP  FLT FTP Data Discrepancy Report
            System.Threading.ThreadStart BCSTRPFLTFTPDataDiscrepancyReport = new System.Threading.ThreadStart(SendBCSTRPFLTFTPDataDiscrepancyReport);
            System.Threading.Thread BCSTRPFLTFTPDataDiscrepancyReportThread = new System.Threading.Thread(BCSTRPFLTFTPDataDiscrepancyReport);
            BCSTRPFLTFTPDataDiscrepancyReportThread.Start();

            //2. Send BCS CUSIP Missing in RP Report
            System.Threading.ThreadStart BCSCUSIPMissingInRP = new System.Threading.ThreadStart(SendBCSCUSIPMissingInRPReport);
            System.Threading.Thread BCSCUSIPMissingInRPThread = new System.Threading.Thread(BCSCUSIPMissingInRP);
            BCSCUSIPMissingInRPThread.Start();

            //2. Send BCS TRP CUSIP Missing in RP Report
            System.Threading.ThreadStart BCSSLINKReport = new System.Threading.ThreadStart(SendBCSSLINKReport);
            System.Threading.Thread BCSSLINKReportThread = new System.Threading.Thread(BCSSLINKReport);
            BCSSLINKReportThread.Start();



            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
            {
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, Process.GetCurrentProcess().ProcessName, "BCS.BCSTRPReportService  Started " + ConfigValues.AppEnvironment, "support", null);
            }
        }

        private void SendBCSSLINKReport()
        {
            int SleepTimeBCSTRPFLTFTPDataDiscrepancyReport = Convert.ToInt32(ConfigValues.SleepTimeBCSTRPFLTFTPDataDiscrepancyReport);
            TimeSpan TimeFromBCSSLINKReport = System.TimeSpan.Parse(ConfigValues.TimeFromBCSSLINKReport);
            TimeSpan TimeToFromBCSSLINKReport = System.TimeSpan.Parse(ConfigValues.TimeToFromBCSSLINKReport);

            while (true)
            {
                try
                {

                    DateTime currenttime = DateTime.Now;

                    if (currenttime.TimeOfDay >= TimeFromBCSSLINKReport && currenttime.TimeOfDay <= TimeToFromBCSSLINKReport)
                    {
                        new ServiceFactory().SendBCSSLINKReport();                                               
                    }

                    Thread.Sleep(SleepTimeBCSTRPFLTFTPDataDiscrepancyReport);
                }
                catch (Exception ex)
                {
                    (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSTRPReportService, true);
                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }
            }
        }

        private void SendBCSTRPFLTFTPDataDiscrepancyReport()
        {
            int SleepTimeBCSTRPFLTFTPDataDiscrepancyReport = Convert.ToInt32(ConfigValues.SleepTimeBCSTRPFLTFTPDataDiscrepancyReport);
            TimeSpan TimeFromBCSTRPFLTFTPDataDiscrepancyReportMorning = System.TimeSpan.Parse(ConfigValues.TimeFromBCSTRPFLTFTPDataDiscrepancyReportMorning);
            TimeSpan TimeToBCSTRPFLTFTPDataDiscrepancyReportMorning = System.TimeSpan.Parse(ConfigValues.TimeToBCSTRPFLTFTPDataDiscrepancyReportMorning);

            TimeSpan TimeFromBCSTRPFLTFTPDataDiscrepancyReportEvening = System.TimeSpan.Parse(ConfigValues.TimeFromBCSTRPFLTFTPDataDiscrepancyReportEvening);
            TimeSpan TimeToBCSTRPFLTFTPDataDiscrepancyReportEvening = System.TimeSpan.Parse(ConfigValues.TimeToBCSTRPFLTFTPDataDiscrepancyReportEvening);



            while (true)
            {
                try
                {
                    DateTime currenttime = DateTime.Now;

                    if ((currenttime.TimeOfDay >= TimeFromBCSTRPFLTFTPDataDiscrepancyReportMorning && currenttime.TimeOfDay <= TimeToBCSTRPFLTFTPDataDiscrepancyReportMorning)
                        || (currenttime.TimeOfDay >= TimeFromBCSTRPFLTFTPDataDiscrepancyReportEvening && currenttime.TimeOfDay <= TimeToBCSTRPFLTFTPDataDiscrepancyReportEvening))
                    {                        

                        new ServiceFactory().SendBCSTRPFLTFTPDataDiscrepancyReport();
                        
                    }

                    Thread.Sleep(SleepTimeBCSTRPFLTFTPDataDiscrepancyReport);
                }
                catch (Exception ex)
                {
                    (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSTRPReportService, true);
                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }

            }

        }

        private void SendBCSCUSIPMissingInRPReport()
        {
            while (true)
            {
                try
                {
                    ServiceFactory serviceFactory = new ServiceFactory();
                    serviceFactory.SendBCSCUSIPMissingInRPReport();

                    Thread.Sleep(Convert.ToInt32(ConfigValues.SleepTimeBCSTRPCUSIPMissingReport));
                }
                catch (Exception ex)
                {
                    (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateValidationService, true);
                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }
            }
        }

        protected override void OnStop()
        {
            Logging.LogToFile("BCS.BCSTRPReportService Service Stopped. " + DateTime.Now);

            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
            {
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, Process.GetCurrentProcess().ProcessName, "BCS.BCSTRPReportService  Stopped " + ConfigValues.AppEnvironment, "support", null);
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

            serviceInstaller.ServiceName = "BCS.BCSTRPReportService";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);

        }

    }
}
