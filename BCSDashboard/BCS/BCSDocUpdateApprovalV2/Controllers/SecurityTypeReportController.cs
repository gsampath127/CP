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
    [RoutePrefix("api/SecurityTypeReport")]
    public class SecurityTypeReportController : ApiController
    {
        [Route("GetCompanyandSecurityType")]
        [HttpGet]
        public Object GetCompanyandSecurityType()
        {
             var data = new RPSecurityTypeDashboardFactory().GetCompanyandSecurityType();
            return Json(new { CompanyDetails = data.Tables[0], SecurityTypeDetails = data.Tables[1] });

        }
        [Route("SecurityReport")]
        [HttpPost]
        public Object Report([FromBody]JObject data)
        {

            dynamic gridData = data;
            string cusip=gridData.CUSIP == null ? null : gridData.CUSIP.ToString();
            int?  companyId = gridData.Company == null ? null : Convert.ToInt32(gridData.Company.ToString());
            int?  securityType=gridData.SecurityType == null ? null : Convert.ToInt32(gridData.SecurityType.ToString());

            List<BCSecurityType> securityTypeData = new List<BCSecurityType>();
            KendoGridPost postData = new KendoGridPost(gridData);
            int startIndex = (postData.Page - 1) * postData.PageSize;
            int endIndex = startIndex + postData.PageSize;
            string SortColumn = string.IsNullOrEmpty(postData.SortColumn) ? "CUSIP" : postData.SortColumn,
            SortDirection = string.IsNullOrEmpty(postData.SortOrder) ? "ASC" : postData.SortOrder.ToUpper();
            int total = 0;

            securityTypeData = new RPSecurityTypeDashboardFactory().GetSecurityTypeDetails
                (cusip,companyId ,
                securityType, SortColumn, 
                SortDirection, startIndex, endIndex, out total);


            return Json(new { total = total, data = securityTypeData });

        }
        [Route("SummaryData")]
        [HttpPost]
        public Object SummaryData([FromBody]JObject data)
        {
            
          var summaryData = new RPSecurityTypeDashboardFactory().GetSummarizedData();
            return Json(new { data = summaryData });
        }
    }
}
