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
    
    public class ForgotPasswordController : ApiController
    {

        // GET: api/ForgotPassword
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ForgotPassword/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ForgotPassword
        [Route("api/ForgotPassword")]
        public BCSV2User Post([FromBody]JObject value)
        {
            dynamic json = value;
            BCSV2User objUser = new BCSV2User();
            objUser.Email = json.email;            
            BCSV2ForgotPasswordFactory objForgotPassword = new BCSV2ForgotPasswordFactory();
            return objForgotPassword.CheckEmailForSecurityQuestion(json.email.ToString());
        }

        // PUT: api/ForgotPassword/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ForgotPassword/5
        public void Delete(int id)
        {
        }
        [Route("api/ForgotPassword/CheckAnswer")]
        public string CheckAnswer(JObject value)
        {
            dynamic json = value;
            BCSV2User objUser=new BCSV2User();
            objUser.SecurityQuestion=json.SecurityQuestion;
            objUser.SecurityAnswer=json.SecurityAnswer.ToString();
            objUser.UserSecurityAnswer=PasswordHash.ComputeHash(json.CheckAnswer.ToString());
            BCSV2ForgotPasswordFactory objForgotPassword = new BCSV2ForgotPasswordFactory();
            
            return objForgotPassword.CheckSecurityAnswer(objUser);
        }
        [Route("api/ForgotPassword/SetPassword")]
        public string SetPassword(JObject value)
        {
            dynamic json = value;
            BCSV2User objUser=new BCSV2User();
            objUser.Email = json.email;
            objUser.NewPassword=PasswordHash.ComputeHash(json.ReEnterPassword.ToString());
            BCSV2ForgotPasswordFactory objForgotPassword = new BCSV2ForgotPasswordFactory();

            return objForgotPassword.SetPassword(objUser);
        }
    }
}
