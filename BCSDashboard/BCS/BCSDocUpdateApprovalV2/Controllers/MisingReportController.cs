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
    [CustomExceptionFilter]
    [RoutePrefix("api/SecurityTypes")]
    public class MisingReportController : ApiController
    {
        [Route("Report")]
        [HttpPost]
        public Object MissingReportCusip([FromBody]JObject data)
        {
            dynamic gridData = data;

            List<BCSMissingReports> bcsReportData = new List<BCSMissingReports>();
            KendoGridPost postData = new KendoGridPost(gridData);

            int startIndex = (postData.Page - 1) * postData.PageSize;
            int endIndex = startIndex + postData.PageSize, totalCount = 0;
            string sortField = string.IsNullOrEmpty(postData.SortColumn) ? "CUSIP" : postData.SortColumn,
                   sortOrder = string.IsNullOrEmpty(postData.SortOrder) ? "ASC" : postData.SortOrder.ToUpper();

            bcsReportData = new RPSecurityTypeDashboardFactory().GetMissingReports(
                gridData.reportType.ToString(), startIndex, 
                endIndex, sortField, sortOrder, out totalCount);

            
            return Json(new { total = totalCount, data = bcsReportData });
        }

        [Route("AddEditMissingReport")]
        [HttpPost]
        public int AddEditLevel([FromBody]JObject data)
        {
            dynamic gridData = data;
            int result = new RPSecurityTypeDashboardFactory().UpdateSecurityTypesInProsTicker(Convert.ToString(gridData.CUSIP), Convert.ToInt32(gridData.SelectedLevel), Convert.ToString(gridData.SecurityTypeFeedSourceName));
            return result;
        }
    }
}
