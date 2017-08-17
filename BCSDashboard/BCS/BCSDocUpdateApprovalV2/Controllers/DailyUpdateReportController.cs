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
    //[CustomAuthenticationFilter]
    //[CustomAuthorizationFilter(Roles = "Super Admin,Alliance Bernstein Admin,Alliance Bernstein User,Transamerica Admin,Transamerica User")]
    //[CustomExceptionFilter]
    [RoutePrefix("api/DailyUpdate")]
    public class DailyUpdateReportController : ApiController
    {
        
        [Route("Report")]
        [HttpPost]
        public Object Report([FromBody]JObject data)
        {

            dynamic gridData = data;

            List<BCSDailyUpdateReport> DailyReport = new List<BCSDailyUpdateReport>();
            KendoGridPost postData = new KendoGridPost(gridData);

            int startIndex = (postData.Page - 1) * postData.PageSize;
            int endIndex = startIndex + postData.PageSize, totalCount = 0;
            string sortField = string.IsNullOrEmpty(postData.SortColumn) ? "CUSIP" : postData.SortColumn,
                   sortOrder = string.IsNullOrEmpty(postData.SortOrder) ? "ASC" : postData.SortOrder.ToUpper();

            DailyReport = new RPSecurityTypeFeedFactory().GetDailyUpdateReport(
                Convert.ToDateTime(gridData.SelectDate), sortField, sortOrder, startIndex, endIndex, out totalCount
                );


            return Json(new { total = totalCount, data = DailyReport });

        }
    }
}
