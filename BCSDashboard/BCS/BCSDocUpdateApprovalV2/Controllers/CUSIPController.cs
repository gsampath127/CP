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
    [CustomAuthorizationFilter(Roles = "Super Admin,Alliance Bernstein Admin,Alliance Bernstein User,Transamerica Admin,Transamerica User")]
    [CustomExceptionFilter]
    [RoutePrefix("api/CUSIP")]
    public class CUSIPController : ApiController
    {
        [Route("ReportTypes")]
        [HttpGet]
        public List<string> ReportTypes()
        {
            return UtilityFactory.GetEnumDescriptionList(typeof(BCSReportType));
           
        }
        [Route("Report")]
        [HttpPost]
        public Object Report([FromBody]JObject data)
        {
           
            dynamic gridData = data;
           
            List<BCSReports> bcsReportData = new List<BCSReports>();
            KendoGridPost postData = new KendoGridPost(gridData);

            int startIndex = postData.Page * postData.PageSize;
            int endIndex = startIndex + postData.PageSize, totalCount = 0;
            string sortField = string.IsNullOrEmpty(postData.SortColumn) ? "CUSIP_RP" : postData.SortColumn,
                   sortOrder = string.IsNullOrEmpty(postData.SortOrder) ? "ASC" : postData.SortOrder.ToUpper();
             
            bcsReportData = new BCSDocUpdateApprovalFactory().GetBCSReports(
                gridData.clientName.ToString(), gridData.reportType.ToString(), 
                Convert.ToDateTime(gridData.startDate),
                Convert.ToDateTime(gridData.endDate), startIndex, 
                endIndex, sortField, sortOrder, out totalCount);

            
            return Json(new { total = totalCount, data = bcsReportData });
          
        }
    }
}
