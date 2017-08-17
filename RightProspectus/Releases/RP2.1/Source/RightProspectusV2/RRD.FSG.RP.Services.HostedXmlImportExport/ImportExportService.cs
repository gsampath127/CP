// ***********************************************************************
// Assembly         : RRD.FSG.RP.Services.HostedXmlImportExport
// Author           : NI317175
// Created          : 12-23-2015
//
// Last Modified By : NI317175
// Last Modified On : 12-23-2015
// ***********************************************************************



using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections;
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

/// <summary>
/// The HostedXmlImportExport namespace.
/// </summary>
namespace RRD.FSG.RP.Services.HostedXmlImportExport
{
    /// <summary>
    /// Class ImportExportService.
    /// </summary>
    public partial class ImportExportService : ServiceBase
    {




        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExportService"/> class.
        /// </summary>
        public ImportExportService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            ThreadStart exportXMLJob = new ThreadStart(PollExport);
            Thread exportXMLJobThread = new Thread(exportXMLJob);
            exportXMLJobThread.Start();

            ThreadStart importXMLJob = new ThreadStart(PollImport);
            Thread importXMLJobThread = new Thread(importXMLJob);
            importXMLJobThread.Start();

        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
        }

        /// <summary>
        /// Logs the error to database.
        /// </summary>
        /// <param name="sourceOfError">The source of error.</param>
        /// <param name="Message">The message.</param>
        /// <param name="dataaccess">The dataaccess.</param>
        private void LogErrorToDB(int sourceOfError, string Message,DataAccess dataaccess)
        {
            ErrorLogFactory logfactory = new ErrorLogFactory(dataaccess);            

            ErrorLogObjectModel errorlogobjectmodel = new ErrorLogObjectModel();

            if (sourceOfError==1)
            {
                errorlogobjectmodel.Title = "Import Service Error";
                errorlogobjectmodel.ErrorCode = 900;
            }
            else
                if (sourceOfError==2)
            {
                errorlogobjectmodel.Title = "Export Service Error";
                errorlogobjectmodel.ErrorCode = 1000;
            }
                else
                     if(sourceOfError ==3)
                {
                    errorlogobjectmodel.Title = "Import Export Service Error Email Send Error";
                    errorlogobjectmodel.ErrorCode = 1100;
                }


            errorlogobjectmodel.Message = Message;

            errorlogobjectmodel.ProcessID = Process.GetCurrentProcess().Id.ToString();

            errorlogobjectmodel.ProcessName = "RRD.FSG.RP.Services.HostedXmlImportExport";

            errorlogobjectmodel.ThreadName = Thread.CurrentThread.Name;

            errorlogobjectmodel.Win32ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();

            errorlogobjectmodel.MachineName = Environment.MachineName;

            errorlogobjectmodel.AppDomainName = AppDomain.CurrentDomain.FriendlyName;

            errorlogobjectmodel.Priority = 2;

            errorlogobjectmodel.Severity = "High";

            logfactory.SaveEntity(errorlogobjectmodel);


        }



        /// <summary>
        /// Polls the export.
        /// </summary>
        public void PollExport()
        {

            DataAccess dataaccess = new DataAccess();

            ClientFactory clientFactory = new ClientFactory(dataaccess);

            

            while (true)
            {
                try
                {                  

                    IEnumerable<ClientObjectModel> clients =  clientFactory.GetAllEntities();

                    

                    //Used to make use of the multicore processors.
                    Parallel.ForEach(clients, client =>
                    {
                        VerticalXmlExportFactory verticalXmlExportFactory = new VerticalXmlExportFactory(dataaccess);

                        verticalXmlExportFactory.ClientName = client.ClientName;

                        verticalXmlExportFactory.DequeueAndSaveExportXML();
                    });

                    Thread.Sleep(ConfigValues.SleepTimeBetweenRequestsForVerticalImportExport);
                }
                catch (Exception exception)
                {

                    // Log the exception to SystemDb.
                    LogErrorToDB(1, exception.Message + " " + exception.InnerException.Message, dataaccess);

                    try
                    {
                        EmailHelper.SendEmail(ConfigValues.EmailFrom, ConfigValues.ErrorEmailTo,
                                                "PollExport Exception From RRD.FSG.RP.Services.HostedXmlImportExport",
                                                exception.Message + " " + exception.InnerException.Message, null,
                                                null, null, "RRD.FSG.RP.Services.HostedXmlImportExport Export Error",
                                                null);
                    }
                    catch (Exception emailsendException)
                    {
                        LogErrorToDB(3, emailsendException.Message + " " + emailsendException.InnerException.Message, dataaccess);
                    }

                    Thread.Sleep(ConfigValues.SleepTimeWhenError);
                }
            }
        }

        /// <summary>
        /// Polls the import.
        /// </summary>
        public void PollImport()
        {

            DataAccess dataaccess = new DataAccess();

            ClientFactory clientFactory = new ClientFactory(dataaccess);



            while (true)
            {
                try
                {

                    IEnumerable<ClientObjectModel> clients = clientFactory.GetAllEntities();

                    //Used to make use of the multicore processors.
                    Parallel.ForEach(clients, client =>
                    {
                        VerticalXmlImportFactory verticalXmlImportFactory = new VerticalXmlImportFactory(dataaccess);

                        verticalXmlImportFactory.ClientName = client.ClientName;

                        verticalXmlImportFactory.DequeueAndLoadImportXML();
                    });

                    Thread.Sleep(ConfigValues.SleepTimeBetweenRequestsForVerticalImportExport);
                }
                catch (Exception exception)
                {

                    // Log the exception to SystemDb.
                    LogErrorToDB(1, exception.Message + " " + exception.InnerException.Message, dataaccess);
                    try
                    {
                        EmailHelper.SendEmail(ConfigValues.EmailFrom, ConfigValues.ErrorEmailTo,
                                                "PollImport Exception From RRD.FSG.RP.Services.HostedXmlImportExport",
                                                exception.Message + " " + exception.InnerException.Message, null,
                                                null, null, "RRD.FSG.RP.Services.HostedXmlImportExport Import Error",
                                                null);
                    }
                    catch(Exception emailsendException)
                    {
                        LogErrorToDB(3, emailsendException.Message + " " + emailsendException.InnerException.Message, dataaccess);
                    }
                    Thread.Sleep(ConfigValues.SleepTimeWhenError);
                }
            }
        }
    }

    /// <summary>
    /// Class ImportExportServiceInstaller.
    /// </summary>
    [RunInstallerAttribute(true)]
    public partial class ImportExportServiceInstaller : Installer
    {
        /// <summary>
        /// The service installer
        /// </summary>
        private ServiceInstaller serviceInstaller;
        /// <summary>
        /// The process installer
        /// </summary>
        private ServiceProcessInstaller processInstaller;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExportServiceInstaller"/> class.
        /// </summary>
        public ImportExportServiceInstaller()
        {

            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            // Service will run under system account
            processInstaller.Account = ServiceAccount.LocalSystem;

            // Service will have Start Type of Manual
            serviceInstaller.StartType = ServiceStartMode.Manual;

            serviceInstaller.ServiceName = "RRD.FSG.RP.Services.HostedXmlImportExport";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);

        }

    }
}
