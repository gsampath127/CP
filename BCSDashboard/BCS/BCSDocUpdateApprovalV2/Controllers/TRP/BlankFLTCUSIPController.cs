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

namespace BCSDocUpdateApprovalV2.Controllers.TRP
{
    [CustomAuthenticationFilter]
    [CustomAuthorizationFilter(Roles = "Super Admin,TRP User,TRP Admin")]
    [CustomExceptionFilter]
    [RoutePrefix("api/TRP")]
    public class BlankFLTCUSIPController : ApiController
    {
        [Route("BlankFLTCUSIP")]
        [HttpPost]
        public Object BlankFLTCUSIP([FromBody]JObject data)
        {
            dynamic gridData = data;
            KendoGridPost postData = new KendoGridPost(gridData);
            postData.PageSize = postData.PageSize == 0 ? 10 : postData.PageSize;
            int startIndex = (postData.Page - 1) * postData.PageSize;
            int endIndex = startIndex + postData.PageSize;
            BCSTRPReportData bCSTRPReportData = new BCSDocUpdateApprovalFactory().GetBCSTRPReportBlankFLTCUSIPData(startIndex, endIndex);
            return Json(new { total = bCSTRPReportData.BCSTRPReportBlankFLTCUSIPDataVirtualCount, data = bCSTRPReportData.BCSTRPReportBlankFLTCUSIPData });
        }
    }
}
