using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using BCSDocUpdateApprovalV2.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace BCSDocUpdateApprovalV2.Controllers.TRP
{
    [CustomAuthenticationFilter]
    [CustomAuthorizationFilter(Roles = "Super Admin,TRP User,TRP Admin")]
    [CustomExceptionFilter]
    [RoutePrefix("api/TRP")]
    public class CustomerFLTController : ApiController
    {
        [Route("CustomerFLT")]
        [HttpPost]
        public Object CustomerFLT([FromBody]JObject data)
        {
            dynamic gridData = data;
            List<SANFileDetails> lstFLTFileInfo = new BCSDocUpdateApprovalFactory().GetSANFileDetails(ConfigValues.BCSTRPFLTArchiveDropPath, Convert.ToDateTime(gridData.FLTDate.ToString()), "*.txt");
            return Json(new { total = lstFLTFileInfo.Count, data = lstFLTFileInfo });
        }
    }
}
