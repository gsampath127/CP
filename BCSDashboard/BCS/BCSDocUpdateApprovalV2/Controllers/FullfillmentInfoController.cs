using BCS.ObjectModel;
using BCSDocUpdateApprovalV2.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace BCSDocUpdateApprovalV2.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorizationFilter(Roles = "*")]
    [CustomExceptionFilter]
    public class FullfillmentInfoController : ApiController
    {

        [Route("api/FullfillmentInfo/Report")]
        [HttpPost]
        public List<FullfillmentInfo> Report([FromBody]JObject data)
        {
            dynamic gridData = data;

            DateTime dtReportDate = Convert.ToDateTime(gridData.reportDate);
            string ClientName = gridData.ClientName.ToString();

            List<FullfillmentInfo> lstResult = new List<FullfillmentInfo>();
            lstResult.Add(BindToGridFullfillment(dtReportDate, ClientName));
            return lstResult;
        }

        private FullfillmentInfo BindToGridFullfillment(DateTime dtReportDate, string ClientName)
        {
            FullfillmentInfo FullfillmentInfoHeader = new FullfillmentInfo();
            DateTime date = dtReportDate;
            if (dtReportDate != null)
            {
                string FolderPath = string.Empty;
                switch (ClientName)
                {
                    case "GIM":
                        FolderPath = ConfigurationManager.AppSettings.Get("FullfillmentInfoGIM").ToString();
                        break;
                    case "GMS":
                        FolderPath = ConfigurationManager.AppSettings.Get("FullfillmentInfoGMS").ToString();
                        break;
                    case "Transamerica":
                        FolderPath = ConfigurationManager.AppSettings.Get("FullfillmentInfoTransamerica").ToString();
                        break;
                    case "AllianceBernstein":
                        FolderPath = ConfigurationManager.AppSettings.Get("FullfillmentInfoAllianceBernstein").ToString();
                        break;
                    default:
                        break;
                }

                var files = Directory.EnumerateFiles(FolderPath, "Response_*.xml", SearchOption.TopDirectoryOnly)
         .Select(fn => new FileInfo(fn));

                var fileInfo = files.Where(fn => fn.LastWriteTime.ToShortDateString() == date.ToShortDateString());
                List<FullfillmentInfoDetails> lstFullfillmentInfoDetails = new List<FullfillmentInfoDetails>();
                foreach (FileInfo file in fileInfo)
                {
                    XDocument doc = XDocument.Load(new StreamReader(file.FullName));
                    string transID = doc.Descendants("Response").FirstOrDefault().Element("TransId").Value;
                    var data = from item in doc.Descendants("file")
                               select new
                               {
                                   transID = transID,
                                   cusip = item.Element("cusip").Value,
                                   status = item.Element("message").Value
                               };
                    foreach (var r in data)
                    {
                        FullfillmentInfoDetails objFullInfoDtls = new FullfillmentInfoDetails()
                        {
                            TransID = r.transID,
                            CUSIP = r.cusip,
                            Status = r.status
                        };
                        lstFullfillmentInfoDetails.Add(objFullInfoDtls);
                    }
                }

                FullfillmentInfoHeader.TotalRequests = lstFullfillmentInfoDetails.Count;
                FullfillmentInfoHeader.Completed = lstFullfillmentInfoDetails.Count(p => p.Status == "ok");
                FullfillmentInfoHeader.NotAvailable = lstFullfillmentInfoDetails.Count(p => p.Status != "ok");
                FullfillmentInfoHeader.FullfillmentDetails = lstFullfillmentInfoDetails;
            }

            return FullfillmentInfoHeader;
        }

    }
}
