using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using BCSDocUpdateApprovalV2.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BCSDocUpdateApprovalV2.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorizationFilter(Roles = "*")]
    [CustomExceptionFilter]
    [RoutePrefix("api/GATEWAYDOCUPDATE")]
    public class GatewayDocUpdateController : ApiController
    {
        private const string docUpdateDocumentPath = "docUpdateDocumentPath";  

        [Route("GetReportStatus")]
        [HttpGet]
        public List<string> GetReportStatus()
        {
            List<string> lstStatus = new List<string>();

            lstStatus.Add("--Select Status--");
            UtilityFactory.GetEnumDescriptionList(typeof(DocUpdateDownstream)).ForEach(x =>
            {
                lstStatus.Add(x);
            });
            return lstStatus;
        }


        [Route("Report")]
        [HttpPost]
        public object Report([FromBody]JObject data)
        {
            dynamic gridData = data;
            string clientName = gridData.ClientName.ToString();
            string doumentPath = string.Empty;
            List<SANFileDetails> CustDocUPDTDetails = new List<SANFileDetails>();
            DateTime? selectedDate = Convert.ToDateTime(gridData.reportDate);
            string fileSelected = gridData.SelectedValue;
            doumentPath = ConfigurationManager.AppSettings.Get(docUpdateDocumentPath);
            

            string filter = string.Empty;
            switch (fileSelected)
            {
                case "NU-File": filter = "*NU*.txt"; break;
                case "IP-File": filter = "*IP*.txt"; break;
                case "Gateway - Doc Update": filter = "*.txt"; break;

            }
            if (selectedDate != null)
            {
                CustDocUPDTDetails = new BCSDocUpdateApprovalFactory().GetSANFileDetails(doumentPath, Convert.ToDateTime(selectedDate), filter);                
            }
            List<BCSGatewayDocUpdateData> lstHeaderData = new List<BCSGatewayDocUpdateData>();
            foreach (SANFileDetails dirName in CustDocUPDTDetails)
            {
                lstHeaderData.Add(new BCSDocUpdateApprovalFactory().GetDocUpdateHedaerData(dirName.DirectoryName));
            }
            return Json(new { HeaderData = lstHeaderData, GatewayDocUPDTDetails = CustDocUPDTDetails });
        }
    }
}