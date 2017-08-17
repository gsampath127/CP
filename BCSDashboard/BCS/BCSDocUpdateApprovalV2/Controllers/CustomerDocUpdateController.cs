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
    [CustomAuthorizationFilter(Roles = "Super Admin,GMS Admin,GMS User,Alliance Bernstein Admin,Alliance Bernstein User,Transamerica Admin,Transamerica User")]
    [CustomExceptionFilter]
    [RoutePrefix("api/CUSTOMERDOCUPDATE")]
    public class CustomerDocUpdateController : ApiController
    {
        private const string filteredIpTransamericaDocumentPath = "filteredIpTransamericaDocumentPath";
        private const string filteredIpAllianceBernsteinDocumentPath = "filteredIpAllianceBernsteinDocumentPath";

        [Route("GetReportStatus")]
        [HttpGet]
        public List<string> GetReportStatus()
        {
            List<string> lstStatus = new List<string>();

            lstStatus.Add("--Select Status--");
            UtilityFactory.GetEnumDescriptionList(typeof(CustomerUpdateDownstream)).ForEach(x =>
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

            string clientName = gridData.clientName.ToString();
            string doumentPath = string.Empty;         
            List<SANFileDetails> CustDocUPDTDetails = new List<SANFileDetails>();
            bool isFilterIP = false;
            DateTime? selectedDate = DateTime.Now;
            List<BCSGatewayDocUpdateData> lstHeaderData = new List<BCSGatewayDocUpdateData>();
            string fileSelected = gridData.SelectedValue;
            switch (clientName)
            {
                case "GMS": doumentPath = ConfigurationManager.AppSettings.Get("CustomerDocUPDTPathGMS");
                    selectedDate = Convert.ToDateTime(gridData.reportDate);
                    isFilterIP = false;
                    break;
                case "Transamerica": doumentPath = ConfigurationManager.AppSettings.Get(filteredIpTransamericaDocumentPath);
                    selectedDate = Convert.ToDateTime(gridData.customerDocUpdateDate);
                    isFilterIP = true;
                    break;
                case "AllianceBernstein": doumentPath = ConfigurationManager.AppSettings.Get(filteredIpAllianceBernsteinDocumentPath);
                    selectedDate = Convert.ToDateTime(gridData.CustomerDocUpdateDate);
                    isFilterIP = true;
                    break;
            }

            if (isFilterIP)
            {
                if (selectedDate != null)
                {
                    CustDocUPDTDetails = new BCSDocUpdateApprovalFactory().GetSANFileDetails(doumentPath, Convert.ToDateTime(selectedDate), "*.txt");

                }

            }
            else
            {
                string filter = string.Empty;
                switch (fileSelected)
                {
                    case "NU-File": filter = "*NU*.txt"; break;
                    case "IP-File": filter = "*IP*.txt"; break;
                    case "Customer - Doc Update": filter = "*.txt"; break;

                }
                if (selectedDate != null)
                {
                    CustDocUPDTDetails = new BCSDocUpdateApprovalFactory().GetSANFileDetails(doumentPath, Convert.ToDateTime(selectedDate), filter);

                }


                foreach (SANFileDetails dirName in CustDocUPDTDetails)
                {
                    lstHeaderData.Add(new BCSDocUpdateApprovalFactory().GetDocUpdateHedaerData(dirName.DirectoryName));
                }
            }

            return Json(new { HeaderData = lstHeaderData, CustDocUPDTDetails = CustDocUPDTDetails });

        }
    }
}
