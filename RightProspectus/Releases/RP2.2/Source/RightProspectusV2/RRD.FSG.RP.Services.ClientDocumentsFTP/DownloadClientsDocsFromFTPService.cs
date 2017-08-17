using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Services.ClientDocumentsFTP
{
    public partial class DownloadClientsDocsFromFTPService : ServiceBase
    {
        public DownloadClientsDocsFromFTPService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ThreadStart downloadDocsJob = new ThreadStart(DownloadDocumentsFromFTPandUpdateClientDocs);
            Thread downloadDocsJobThread = new Thread(downloadDocsJob);
            downloadDocsJobThread.Start();
        }

        protected override void OnStop()
        {
        }

        /// <summary>
        /// Polls the export.
        /// </summary>
        public void DownloadDocumentsFromFTPandUpdateClientDocs()
        {
            DataAccess dataaccess = new DataAccess();

            ClientFactory clientFactory = new ClientFactory(dataaccess);
          



            while (true)
            {
                try
                {
                    IEnumerable<ClientObjectModel> clients = clientFactory.GetAllEntities().Where(a=> a.IsClientDocumentsAvailableFromFTP);

                    //Used to make use of the multicore processors.
                    Parallel.ForEach(clients, client =>
                    {
                        ClientDocumentTypeFactory clientDocumentTypeFactory = new ClientDocumentTypeFactory(dataaccess);                        

                        clientDocumentTypeFactory.ClientName = client.ClientName;

                        ClientDocumentFactory clientDocumentFactory = new ClientDocumentFactory(dataaccess);

                        clientDocumentFactory.ClientName = client.ClientName;

                        IEnumerable<ClientDocumentTypeObjectModel> clientdocumentTypeswithFtpEnabled = clientDocumentTypeFactory.GetAllEntities().Where(p => !string.IsNullOrEmpty(p.FTPName));

                        foreach(ClientDocumentTypeObjectModel clientdocumentTypewithFtpEnabled in clientdocumentTypeswithFtpEnabled)
                        {
                           List<string> filesForDownload =  FTPHelper.GetFileListFromFTP(clientdocumentTypewithFtpEnabled.FTPName,
                                                clientdocumentTypewithFtpEnabled.FTPUsername,
                                               clientdocumentTypewithFtpEnabled.FTPPassword);

                           filesForDownload.ForEach(s =>
                               {
                                

                                 var filebytes =   FTPHelper.DownloadFileFromFTP(clientdocumentTypewithFtpEnabled.FTPName,
                                                clientdocumentTypewithFtpEnabled.FTPUsername,
                                               clientdocumentTypewithFtpEnabled.FTPPassword,
                                               s);
                               });
                        }
                    });

                    Thread.Sleep(ConfigValues.SleepTimeBetweenServiceRequests);
                }
                catch
                {

                }
            }
        }
    }
}
