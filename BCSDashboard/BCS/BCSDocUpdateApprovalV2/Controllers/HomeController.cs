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
    [CustomAuthorizationFilter(Roles = "*")]
    [CustomExceptionFilter]
    [RoutePrefix("api/Home")]
    public class HomeController : ApiController
    {
        [Route("GetClients")]
        [HttpPost]
        public List<string> GetClients([FromBody]JObject data)
        {
            dynamic userData = data;

            List<BCSClient> bcsClient = new ServiceFactory().GetALLClientConfig().FindAll(x => x.ShowClientInDashboard);


            IEnumerable<string> filteredClients = new List<string>();


            foreach (var item in userData.roles)
            {
                if (item.ToString() == "Super Admin")
                {
                    filteredClients = bcsClient.Select(x => x.ClientName).ToList();
                    break;
                }
                filteredClients = filteredClients.Concat(bcsClient.Where(x => item.ToString().StartsWith(x.ClientName)).Select(x => x.ClientName));
            }

            return filteredClients.Distinct().ToList();
        }
    }
}
