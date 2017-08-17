using BCS.ObjectModel.Factories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace BCSDocUpdateApprovalV2.Filters
{
    public class CustomAuthenticationFilter : Attribute, IAuthenticationFilter
    {

        private const string Token = "Token";
        public bool AllowMultiple { get { return false; } }

        private BCSV2LoginFactory loginFactory;

        public CustomAuthenticationFilter()
        {
            loginFactory = new BCSV2LoginFactory();            
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpRequestMessage req = context.Request;
            if (req.Headers.Authorization != null &&
                    req.Headers.Authorization.Scheme.Equals(
                              Token, StringComparison.OrdinalIgnoreCase))
            {
                var handler = new JwtSecurityTokenHandler();
                var JWTToken = handler.ReadToken(context.Request.Headers.Authorization.Parameter) as JwtSecurityToken;
                var application = JWTToken.Claims.Where(claim => claim.Type == "Application").Select(c => c.Value).FirstOrDefault();
                int tokenStatus = loginFactory.ValidateToken(context.Request.Headers.Authorization.Parameter.ToString(),application);

                if (loginFactory != null && tokenStatus==2)
                {
                    List<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>()
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, Token)
                };
                    ClaimsIdentity id = new ClaimsIdentity(claims, Token);
                    IPrincipal principal = new ClaimsPrincipal(new[] { id });
                    context.Principal = principal;
                }
                else if (tokenStatus==1)
                {
                    
                    context.ErrorResult = new StatusCodeResult(HttpStatusCode.BadRequest, context.Request);
                }
                else
                {
                    context.ErrorResult = new UnauthorizedResult(
                         new AuthenticationHeaderValue[0],
                                              context.Request);

                }
            }
            else
            {
                context.ErrorResult = new UnauthorizedResult(
                         new AuthenticationHeaderValue[0],
                                              context.Request);
            }

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result, Token);
            return Task.FromResult(0);
        }        

    }

    public class ResultWithChallenge : IHttpActionResult
    {
        private readonly IHttpActionResult next;
        private readonly string token;

        public ResultWithChallenge(IHttpActionResult next, string token)
        {
            this.next = next;
            this.token = token;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await next.ExecuteAsync(cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.Headers.WwwAuthenticate.Add(
                   new AuthenticationHeaderValue("Token", this.token));                
            }            
            return response;
        }
    }    

}