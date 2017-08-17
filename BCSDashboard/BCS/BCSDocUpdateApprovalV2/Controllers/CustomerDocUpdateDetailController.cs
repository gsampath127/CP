using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using BCSDocUpdateApprovalV2.Filters;
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
    [RoutePrefix("api/CustomerDocUpdateDetail")]
    public class CustomerDocUpdateDetailController : ApiController
    {
        [Route("Report")]
        [HttpPost]
        public BCSDailyIPReportData Report([FromBody]JObject data)
        {
            dynamic gridData = data;
            string clientName = gridData.ClientName.ToString();           
            string sortField = null;
            string sortOrder = null;
            BCSDailyIPReportData objBCSDailyUpdateReports = new BCSDailyIPReportData();
            BCSDailyIPReportDetailRecords objBCSDailyIPReportDetailRecords =new BCSDailyIPReportDetailRecords();
            DateTime? selectedDate = Convert.ToDateTime(gridData.reportDate);          
            if (selectedDate != null)
            {              
                if (sortField != null && sortOrder != null)
                {
                    objBCSDailyUpdateReports = new ServiceFactory().GetDailyIPReportDetails(clientName, Convert.ToDateTime(selectedDate), sortField, sortOrder);
                }
                else
                    objBCSDailyUpdateReports = new ServiceFactory().GetDailyIPReportDetails(clientName, Convert.ToDateTime(selectedDate), "RecordType", "Asc");
            }
            List<BCSDailyIPReportData> lstHeaderData = new List<BCSDailyIPReportData>();
            return objBCSDailyUpdateReports;
        }
    }
}
