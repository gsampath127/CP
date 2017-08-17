using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using BCSDocUpdateApprovalV2.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace BCSDocUpdateApprovalV2.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorizationFilter(Roles = "*")]
    [CustomExceptionFilter]
    public class RegisterController : ApiController
    {
        // GET: api/Register
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Register/5
        public string Get(int id)
        {
            return "test";
        }

        // POST: api/Register
        [Route("api/Register")]
        public string Post([FromBody]JObject value)
        {
            dynamic json = value;

            BCSV2User objUser = new BCSV2User();
            objUser.Email = json.email;
            objUser.FirstName = json.fName;
            objUser.LastName = json.lName;
            objUser.UserName = "";

            objUser.SecurityStamp = "test";
            objUser.ModifiedBy = 1;

            DataTable dtUserRoles = new DataTable();
            dtUserRoles.Columns.Add("UserId", typeof(int));
            dtUserRoles.Columns.Add("RoleId", typeof(int));

            List<int> lstRoles = json.selectedRoles.ToObject<List<int>>();
            lstRoles.ForEach(rl =>
            {
                dtUserRoles.Rows.Add(0, rl);
            });

            BCSV2AuthenticationFactory objAuthFactory = new BCSV2AuthenticationFactory();
            return objAuthFactory.RegisterUser(objUser, dtUserRoles);
        }

        // PUT: api/Register/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Register/5
        public void Delete(int id)
        {
        }


       

      
    }
}
