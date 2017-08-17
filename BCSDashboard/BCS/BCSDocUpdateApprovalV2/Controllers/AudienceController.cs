using BCSDocUpdateApprovalV2.Entities;
using BCSDocUpdateApprovalV2.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BCS.ObjectModel;
using BCS.ObjectModel.Factories;

namespace BCSDocUpdateApprovalV2.Controllers
{
    [CustomExceptionFilter]
    [RoutePrefix("api/audience")]
    public class AudienceController : ApiController
    {
        [Route("User")]
        public IHttpActionResult Post([FromBody]JObject user)
        {
            dynamic credentials = user;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Audience newAudience = AudiencesStore.AddAudience(credentials.email.ToString(), PasswordHash.ComputeHash(credentials.password.ToString()));
            
            return Ok<Audience>(newAudience);

        }

        [Route("Logout")]
        [HttpPost]
        public void Logout([FromBody]JObject data)
        {
            dynamic tokenObject = data;
            TokenDetails token = new TokenDetails()
            {
                Token = tokenObject.token.ToString(),
                UserId = Convert.ToInt32(tokenObject.userId)
            };
            BCSV2LoginFactory loginFactory = new BCSV2LoginFactory();
            loginFactory.DeleteToken(token);
        }

    }
}
