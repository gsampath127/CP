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
using System.Web.Http;

namespace BCSDocUpdateApprovalV2.Controllers.TRP
{
    [CustomAuthenticationFilter]
    [CustomAuthorizationFilter(Roles = "Super Admin,TRP User,TRP Admin")]
    [CustomExceptionFilter]
    [RoutePrefix("api/TRP")]
    public class CustomerDocsController : ApiController
    {
        [Route("CustomerDocs")]
        [HttpPost]
        public Object CustomerDocs([FromBody]JObject data)
        {
            dynamic gridData = data;
            List<BCSTRPFLTFileInfo> lstFLTFileInfo = new List<BCSTRPFLTFileInfo>();
            DateTime selecteddate = Convert.ToDateTime(gridData.DocDate.ToString());
            var month = selecteddate.Month;
            var year = selecteddate.Year;
            var date = selecteddate.Day;
            string doumentPath = ConfigValues.BCSTRPFLTDocArchiveDropPath + year + @"/" + month + @"/" + date + @"/";
            string hrefPath = doumentPath.Replace(ConfigValues.RPSourceURLReplace, ConfigValues.RPDestinationSANReplace).Replace(@"\", "/");
            if (Directory.Exists(doumentPath))
            {
                var filePaths = Directory.EnumerateFiles(doumentPath);
                foreach (string file in filePaths)
                {
                    BCSTRPFLTFileInfo fltDatafiles = new BCSTRPFLTFileInfo();
                    fltDatafiles.DirectoryName = hrefPath + Path.GetFileName(file);
                    fltDatafiles.FileName = Path.GetFileName(file);// Path.GetFileNameWithoutExtension(file);
                    FileInfo f = new FileInfo(file);
                    fltDatafiles.DateReceived = f.LastWriteTime.ToString();
                    lstFLTFileInfo.Add(fltDatafiles);
                }
            }
            return Json(new { total = lstFLTFileInfo.Count, data = lstFLTFileInfo });
        }
    }
}
