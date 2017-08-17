using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using BCSDocUpdateApprovalV2.Filters;
using BCSDocUpdateApprovalV2.Formats;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BCSDocUpdateApprovalV2.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorizationFilter(Roles = "Super Admin,TRP User,TRP Admin")]
    [CustomExceptionFilter]
    [RoutePrefix("api/TRP")]
    public class DocumentStatusController : ApiController
    {
        [Route("DocumentStatus")]
        [HttpPost]
        public Object DocumentStatus([FromBody]JObject data)
        {
            dynamic gridData = data;
            KendoGridPost postData = new KendoGridPost(gridData);
            postData.PageSize = postData.PageSize == 0 ? 10 : postData.PageSize;
            int startIndex = (postData.Page - 1) * postData.PageSize;
            int endIndex = startIndex + postData.PageSize;
            bool? isPDFReceived = null;
            if (gridData.SelectedPDFStatus == "1")
            {
                isPDFReceived = true;
            }
            else if (gridData.SelectedPDFStatus == "2")
            {
                isPDFReceived = false;
            }

            BCSTRPReportData bCSTRPReportData = new BCSDocUpdateApprovalFactory().GetBCSTRPReportFLTFTPInfoData(string.IsNullOrWhiteSpace(gridData.CUSIP.ToString()) ? null : gridData.CUSIP.ToString().Trim(), isPDFReceived, startIndex, endIndex);
          
            return Json(new { total = bCSTRPReportData.BCSTRPReportFLTFTPInfoDataVirtualCount, data = bCSTRPReportData.BCSTRPReportFLTFTPInfoData });

        }

        [Route("MissingDocs")]
        [HttpPost]
        public Object MissingDocs([FromBody]JObject data)
        {
            dynamic gridData = data;
            KendoGridPost postData = new KendoGridPost(gridData);
            int startIndex = (postData.Page - 1) * postData.PageSize;
            int endIndex = startIndex + postData.PageSize;
            BCSTRPReportData bCSTRPReportData = new BCSDocUpdateApprovalFactory().GetBCSTRPReportFLTMissingData(startIndex, endIndex);
           
            return Json(new { total = bCSTRPReportData.BCSTRPReportFLTMissingDataVirtualCount, data = bCSTRPReportData.BCSTRPReportFLTMissingData });
        }
        [Route("MissingRPCusip")]
        [HttpPost]
        public Object MissingRPCusip([FromBody]JObject data)
        {
            dynamic gridData = data;
            KendoGridPost postData = new KendoGridPost(gridData);
            int startIndex = (postData.Page - 1) * postData.PageSize;
            int endIndex = startIndex + postData.PageSize;
            BCSTRPReportData bCSTRPReportData = new BCSDocUpdateApprovalFactory().GetBCSTRPReportRPMissingCUSIPData(startIndex, endIndex);
           
            return Json(new { total = bCSTRPReportData.BCSTRPReportRPCUSIPMissingDataVirtualCount, data = bCSTRPReportData.BCSTRPReportRPCUSIPMissingData });
        }
    }
}
