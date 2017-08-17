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
    [CustomAuthorizationFilter(Roles = "*")]
    [RoutePrefix("api/RPMissingCusip")]
    public class RPMissingCusipController : ApiController
    {
        [Route("GetCompany")]
        [HttpGet]
        public Object GetCompany()
        {
          List < BCSClient > bcsClient = new ServiceFactory().GetALLClientConfig().FindAll(x => x.ShowClientInDashboard).
          Where(x => x.ClientName == "AllianceBernstein" || x.ClientName == "Transamerica" || x.ClientName == "TRP").ToList();
          return Json(new { CompanyDetails = bcsClient });
        }
       [Route("Report")]
        public Object Report([FromBody]JObject data)
        {
            dynamic gridData = data;
            string companyName = gridData.Company == null ? null : gridData.Company.ToString();           
            KendoGridPost postData = new KendoGridPost(gridData);
            int startIndex = (postData.Page - 1) * postData.PageSize;
            int endIndex = startIndex + postData.PageSize;
            string SortColumn = string.IsNullOrEmpty(postData.SortColumn) ? "CompanyName" : postData.SortColumn,
            SortDirection = string.IsNullOrEmpty(postData.SortOrder) ? "ASC" : postData.SortOrder.ToUpper();
            int total = 0;
            List<BCSTRPReportRPCUSIPMissingData> bCSReportData = new BCSDocUpdateApprovalFactory().GetMissingCUSIP(
            companyName, startIndex, endIndex, SortColumn,
            SortDirection, out total);
            return Json(new { total = total, data = bCSReportData });
        }
    }
}
