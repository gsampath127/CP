using BCSDocUpdateApprovalV2.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace BCSDocUpdateApprovalV2.Controllers
{
    
    public class DefaultController : ApiController
    {
        [Route("api/Download/DownloadFile")]
        [HttpGet]
        public HttpResponseMessage Download(string fileLocation)
        {
            var path = fileLocation;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);

            return result;
        }
    }
}
