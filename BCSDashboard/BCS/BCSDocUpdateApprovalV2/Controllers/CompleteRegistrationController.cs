using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BCSDocUpdateApprovalV2.Controllers
{
    public class CompleteRegistrationController : ApiController
    {
        // GET: api/CompleteRegistration
        public IEnumerable<BCSV2SecurityQuestions> Get()
        {
            return new List<BCSV2SecurityQuestions>();
        }

        // GET: api/CompleteRegistration/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CompleteRegistration
        [Route("api/CompleteRegistration")]
        public string Post([FromBody]JObject value)
        {
            dynamic json = value;

            BCSV2User objUser = new BCSV2User();
            objUser.PasswordHash = PasswordHash.ComputeHash(json.password.ToString());
            objUser.UserId = json.UserId; 
            objUser.SecurityQuestionId = json.selectedSecurityQn.Id;
            objUser.SecurityAnswer = PasswordHash.ComputeHash(json.securityAns.ToString());
            objUser.ModifiedBy = 1;

            BCSV2AuthenticationFactory objAuthFactory = new BCSV2AuthenticationFactory();
            return objAuthFactory.CompleteRegistration(objUser);
        }

        // PUT: api/CompleteRegistration/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CompleteRegistration/5
        public void Delete(int id)
        {
        }

        [Route("api/CompleteRegistration/GetSecQns")]
        public List<BCSV2SecurityQuestions> GetSecQns()
        {
            BCSV2AuthenticationFactory objAuthFactory = new BCSV2AuthenticationFactory();
            return objAuthFactory.GetSecurityQns();
        }

        [Route("api/CompleteRegistration/ValidateUserProfile")]
        [HttpGet]
        public string ValidateUserProfile(int Id)
        {
            BCSV2AuthenticationFactory objAuthFactory = new BCSV2AuthenticationFactory();
            return objAuthFactory.ValidateUserProfile(Id);
        }

        [Route("api/Register/GetRoles")]
        public List<BCSV2Roles> GetRoles(int Id)
        {
            BCSV2AuthenticationFactory objAuthFactory = new BCSV2AuthenticationFactory();
            return objAuthFactory.GetChildRoles(Id);
        }

    }
}
