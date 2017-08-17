using BCSDocUpdateApprovalV2.Entities;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using Thinktecture.IdentityModel.Tokens;
using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using System.Security.Claims;
using System.Configuration;

namespace BCSDocUpdateApprovalV2.Formats
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {

        private const string AudiencePropertyKey = "audience";

        private readonly string _issuer = string.Empty;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string audienceId = data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) ? data.Properties.Dictionary[AudiencePropertyKey] : null;

            if (string.IsNullOrWhiteSpace(audienceId)) throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");

            Audience audience = AudiencesStore.FindAudience(audienceId);

            string symmetricKeyAsBase64 = audience.Base64Secret;

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;
            BCSV2LoginFactory loginFactory = new BCSV2LoginFactory();
            UserDetails userDetails = loginFactory.GetUserDetails(audience.Name);
            data.Identity.AddClaim(new Claim(ClaimTypes.Role, "Role"));
            foreach (var role in userDetails.Roles)
            {
                data.Identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            }
            data.Identity.AddClaim(new Claim("UserId", userDetails.UserId.ToString()));
            data.Identity.AddClaim(new Claim("Application", ConfigurationManager.AppSettings["Application"].ToString()));

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);
            TokenDetails tokenDetails = new TokenDetails
            {
                UserId = userDetails.UserId,
                Token = jwt.ToString(),
                UtcIssuedOn = issued.Value.UtcDateTime,
                UtcExpiresOn = expires.Value.UtcDateTime
            };
            loginFactory.AddToken(tokenDetails);
            bool result = AudiencesStore.AudiencesList.TryRemove(audienceId, out audience);
            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }

    }
}