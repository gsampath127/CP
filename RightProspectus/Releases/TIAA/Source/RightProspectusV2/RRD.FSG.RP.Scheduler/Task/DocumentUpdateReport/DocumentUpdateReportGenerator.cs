using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.Interfaces.HostedPages;
using RRD.FSG.RP.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RRD.FSG.RP.Scheduler.Task.DocumentUpdateReport
{
    public class DocumentUpdateReportGenerator
    {
        /// <summary>
        /// object for IHostedClientPageScenariosFactory
        /// </summary>
        private IHostedClientPageScenariosFactory hostedClientPagescenariosFactory;

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="clientName"></param>
        public DocumentUpdateReportGenerator(string clientName)
        {
            hostedClientPagescenariosFactory = RPV2Resolver.Resolve<IHostedClientPageScenariosFactory>();
        }        

        /// <summary>
        /// GenerateReport
        /// </summary>
        /// <param name="clientName">clientName</param>
        /// <param name="reportContentObjectModel">reportContentObjectModel</param>
        /// <returns></returns>
        public byte[] GenerateReport(string clientName, ReportContentObjectModel reportContentObjectModel)
        {
            List<DocumentUpdateReportModel> reportData = hostedClientPagescenariosFactory.GetTaxonomyAssociationforDocumentUpdateReport(
                                                                 clientName, reportContentObjectModel.ReportFromDate, reportContentObjectModel.ReportToDate);

            if (reportData.Exists(p => !p.DocumentDate.HasValue || !p.DocumentUpdatedDate.HasValue))
            {
                reportContentObjectModel.DoNotSendReport = true;

                //Send email
            }

            StreamWriter sw = new StreamWriter(reportContentObjectModel.FileName, false);
            string csvBuilderHeader = "UpdateDateTime,CUSIP,DocumentType,DocumentUpdated,DocumentDate";

            sw.WriteLine(csvBuilderHeader.ToString());
            reportData.ForEach(item =>
            {                
                sw.WriteLine(item.DocumentUpdatedDate + "," + item.MarketID + "," + item.DocumentTypeMarketId + "," + item.IsDocUpdated + "," + item.DocumentDate);
            });
            sw.Close();
            return File.ReadAllBytes(reportContentObjectModel.FileName);
        }
    }
}
