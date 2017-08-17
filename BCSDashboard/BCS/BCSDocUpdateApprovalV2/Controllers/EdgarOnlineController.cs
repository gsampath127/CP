using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using BCSDocUpdateApprovalV2.Filters;
using BCSDocUpdateApprovalV2.Formats;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace BCSDocUpdateApprovalV2.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorizationFilter(Roles = "*")]
    [RoutePrefix("api/EdgarOnline")]
    public class EdgarOnlineController : ApiController
    {
        [Route("Report")]
        [HttpPost]
        public Object Report([FromBody]JObject data)
        {
            dynamic gridData = data;
            string path = ConfigurationManager.AppSettings["EdgarOnlineFeedPath"];
            DataTable Eonline = new RPSecurityTypeDashboardFactory().GetEdgarOnlineFeedHistory(
            Convert.ToDateTime(gridData.EonlineDate));
            DataColumn newColumn = new DataColumn("DirectoryName", typeof(System.String));
            Eonline.Columns.Add(newColumn);

            foreach(DataRow row in Eonline.Rows)
            {
                row["DirectoryName"] = Path.Combine(path, row["FileName"].ToString());
            }
          
            return Json(new { data = Eonline });
        }
    }
}
