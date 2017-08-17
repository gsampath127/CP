using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using System.Net;
using System.Net.Http;
using System.IdentityModel.Tokens;
using System.Web.Http;
using System.Security.Claims;

namespace BCSDocUpdateApprovalV2.Filters
{
    public class CustomAuthorizationFilter : AuthorizeAttribute
    {

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool authorize = false;
            if (!String.IsNullOrEmpty(Roles))
            {
                string[] allowedRoles = Roles.Split(',');
                if (allowedRoles.Contains("*"))
                    return true;
                var handler = new JwtSecurityTokenHandler();
                var JWTToken = handler.ReadToken(actionContext.Request.Headers.Authorization.Parameter) as JwtSecurityToken;
                var userRoles = JWTToken.Claims.Where(claim => claim.Type == "role").Select(c => c.Value).ToList();
                foreach (var role in allowedRoles)
                {
                    if (userRoles.Contains(role))
                    {
                        authorize = true; /* return true if Entity has current user(active) with specific role */
                        break;
                    }
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

    }
}