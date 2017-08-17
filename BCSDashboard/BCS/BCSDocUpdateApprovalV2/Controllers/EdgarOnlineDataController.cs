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
    [RoutePrefix("api/EdgarOnlineData")]
    public class EdgarOnlineDataController : ApiController
    {
        [Route("Data")]
        [HttpPost]
        public Object Data([FromBody]JObject data)
        {

            dynamic gridData = data;
            string eoCUSIP = gridData.eoCUSIP == null ? null : gridData.eoCUSIP.ToString();
            string eoCIK = gridData.eoCIK == null ? null : gridData.eoCIK.ToString();
            string eoSeries = gridData.eoSeries == null ? null : gridData.eoSeries.ToString();
            string eoClass = gridData.eoClass == null ? null : gridData.eoClass.ToString();
            string eoTicker = gridData.eoTicker == null ? null : gridData.eoTicker.ToString();
            KendoGridPost postData = new KendoGridPost(gridData);
            int startIndex = (postData.Page - 1) * postData.PageSize;
            int endIndex = startIndex + postData.PageSize;                        
            int total = 0;
            var edgarOnlineData = new RPSecurityTypeDashboardFactory().GetBCSEdgarOnlineData(
            eoCUSIP, eoCIK, eoSeries, eoClass, eoTicker, startIndex, endIndex, out total);                
            return Json(new { total = total, data = edgarOnlineData });
        }
    }
}
