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
     [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {

         [Route("SecurityQuestion")]
         [HttpPost]
         public string SecurityQuestion(JObject value)
         {
             dynamic json = value;
             BCSV2User objUser = new BCSV2User();
             objUser.UserId = json.CurrentUserId;
             objUser.SecurityQuestionId = json.selectedQuestion.Id;
             string plainAnswer = json.securityAnswer;
             objUser.SecurityAnswer = PasswordHash.ComputeHash(plainAnswer);
             BCSV2ProfileFactory objProfileFactory = new BCSV2ProfileFactory();
             return  objProfileFactory.UpdateSecurityQuestion(objUser);
             
         }
         [Route("NewPassword")]
         [HttpPost]
         public bool UpdatePassword(JObject value)
         {
             dynamic json = value;
             BCSV2User objUser = new BCSV2User();
             objUser.UserId = json.CurrentUserId;
             string plainOldPassword = json.oldPassword;
             objUser.Password = PasswordHash.ComputeHash(plainOldPassword);
             string plainNewAnswer = json.newPassword;
             objUser.NewPassword = PasswordHash.ComputeHash(plainNewAnswer);            
             BCSV2ProfileFactory objProfileFactory = new BCSV2ProfileFactory();
             return  objProfileFactory.UpdatePassword(objUser);
             
         }
    }
}
