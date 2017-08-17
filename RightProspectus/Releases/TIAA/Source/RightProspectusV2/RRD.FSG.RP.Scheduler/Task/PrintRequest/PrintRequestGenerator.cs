// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Interfaces.HostedPages;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RRD.FSG.RP.Scheduler.Task.PrintRequest
{
    /// <summary>
    /// Class PrintRequestGenerator.
    /// </summary>
    public class PrintRequestGenerator
    {
        /// <summary>
        /// The hosted client request material factory
        /// </summary>
        private IHostedClientRequestMaterialFactory hostedClientRequestMaterialFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintRequestGenerator"/> class.
        /// </summary>
        public PrintRequestGenerator()
        {
            this.hostedClientRequestMaterialFactory = RPV2Resolver.Resolve<IHostedClientRequestMaterialFactory>();                       
        }
        /// <summary>
        /// PrintRequestGeneratorTest
        /// </summary>
        /// <param name="mockhostedClientRequestMaterialFactory">The mockhosted client request material factory.</param>
        public PrintRequestGenerator(IHostedClientRequestMaterialFactory mockhostedClientRequestMaterialFactory)
        {
            hostedClientRequestMaterialFactory = mockhostedClientRequestMaterialFactory;
        }

        /// <summary>
        /// Generates the print request XML.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="reportContentObjectModel">The report content object model.</param>
        public void GeneratePrintRequestXML(string clientName, ReportContentObjectModel reportContentObjectModel)
        {
            string strGUID = Guid.NewGuid().ToString();

            string printRequestXMLFileName = clientName + "-" + reportContentObjectModel.ReportToDate.ToLocalTime().ToString("MM-dd-yyyy-HH-mm-ss") + "-" + strGUID;
            string dropFileDir = ConfigValues.PrintRequestDropFileDir.Replace("ClientName", clientName) + reportContentObjectModel.ReportToDate.ToLocalTime().ToString(@"yyyy\\MM\\dd\\");
            string dir = dropFileDir + printRequestXMLFileName + @"\";

            List<RequestMaterialPrintHistory> requestMaterialPrintHistoryData = hostedClientRequestMaterialFactory.GetRequestMaterialPrintRequests(clientName, reportContentObjectModel.ReportFromDate, reportContentObjectModel.ReportToDate);

            if (requestMaterialPrintHistoryData != null && requestMaterialPrintHistoryData.Count > 0)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);                    
                }

                XDocument xDocPrintRequestXML = new XDocument();                

                XElement requests = new XElement("requests", new XAttribute("mobular-id", strGUID), new XAttribute("integer-id", 1));                
                
                XElement fundcomplex = new XElement("fund-complex", new XAttribute("name", clientName));
                
                foreach (var item in requestMaterialPrintHistoryData)
                {
                    XElement printrequest = new XElement("print-request", new XAttribute("mobular-id", item.UniqueID), new XAttribute("integer-id", item.RequestMaterialPrintHistoryID),
                                                                          new XAttribute("received-at", item.RequestDateUtc.ToLocalTime().ToString("G")), new XAttribute("fund-name", item.TaxonomyAssociationData.TaxonomyName));
                    XElement address = new XElement("address", new XElement("company", item.ClientCompanyName), new XElement("first-name", item.ClientFirstName), new XElement("last-name", item.ClientLastName),
                                                               new XElement("address-1", item.Address1), new XElement("address-2", item.Address2), new XElement("city", item.City),
                                                               new XElement("state", item.StateOrProvince), new XElement("zipcode", item.PostalCode));

                    XElement funddocuments = new XElement("fund-documents");
                    foreach (var documentType in item.TaxonomyAssociationData.DocumentTypes)
                    {
                        string directurl = documentType.ContentURI;
                        string name = Path.GetFileName(directurl);

                        string url = directurl.Replace(name, null);
                        
                        if (!string.IsNullOrWhiteSpace(documentType.SKUName))
                        {
                            name = documentType.SKUName;
                        }

                        funddocuments.Add(new XElement("fund-document", new XAttribute("name", name), new XAttribute("type", documentType.DocumentTypeLinkText),
                                                                        new XAttribute("quantity", item.Quantity), new XAttribute("url", url), new XAttribute("direct-url", directurl)));
                    }

                    printrequest.Add(address, funddocuments);
                    fundcomplex.Add(printrequest);
                }
                
                requests.Add(fundcomplex);

                xDocPrintRequestXML.Add(requests);
                
                XmlTextWriter writer = new XmlTextWriter(dir + printRequestXMLFileName + ".xml", Encoding.UTF8);                
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                xDocPrintRequestXML.Save(writer);
                writer.Close();


                StringBuilder emailString = new StringBuilder();
                emailString.Append("<html><head><title></title></head><body>");
                emailString.Append("<br /><span style='font-size: 18px; font-family: Arial, Helvetica, sans-serif; color: #666666;'>" + clientName + "PrintRequest" + " - " + ConfigValues.Environment + "</span><br /><br />");
                string strMessage = "Document(s) uploaded on " + DateTime.Now.Date.ToShortDateString() + ":<hr />";
                string strZipFile = printRequestXMLFileName + ".zip";

                ZipHelper.CreateZipFile(dropFileDir + strZipFile, dropFileDir + printRequestXMLFileName);

                strMessage = strMessage + "<li>" + strZipFile + "</li>";
                emailString.Append(strMessage);
                emailString.Append("</body></html>");
                
                reportContentObjectModel.EmailSubject = clientName + "PrintRequest" + " - " + ConfigValues.Environment;
                reportContentObjectModel.EmailBody = emailString.ToString();
                reportContentObjectModel.ContentUri = dropFileDir;
                reportContentObjectModel.FileName = strZipFile;
                reportContentObjectModel.IsAttachedZipFile = true;                
            }

        }        
    }
}
