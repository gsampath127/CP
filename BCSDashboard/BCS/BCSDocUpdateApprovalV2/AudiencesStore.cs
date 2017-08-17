using BCSDocUpdateApprovalV2.Entities;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using BCS.ObjectModel.Factories;

namespace BCSDocUpdateApprovalV2
{
    public static class AudiencesStore
    {

        public static ConcurrentDictionary<string, Audience> AudiencesList = new ConcurrentDictionary<string, Audience>();

        static AudiencesStore()
        {
            AudiencesList.TryAdd("099153c2625149bc8ecb3e85e03f0022",
                                new Audience
                                {
                                    ClientId = "099153c2625149bc8ecb3e85e03f0022",
                                    Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                                    Name = "ResourceServer.Api 1"
                                });
        }

        public static Audience AddAudience(string username, string password)
        {
            BCSV2LoginFactory loginFactory = new BCSV2LoginFactory();
            Tuple<bool,string> loginData = loginFactory.AuthenticateUser(username,password);
            if (loginData.Item1)// validate Credentials with Database
            {
                var clientId = Guid.NewGuid().ToString("N");

                var key = new byte[32];
                RNGCryptoServiceProvider.Create().GetBytes(key);
                var base64Secret = TextEncodings.Base64Url.Encode(key);

                Audience newAudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = username, Success = true };
                AudiencesList.TryAdd(clientId, newAudience);
                return newAudience;
            }
            else
            {
                return new Audience { Name = username, Success = false,Message = loginData.Item2 };
            }

        }


        public static Audience FindAudience(string clientId)
        {
            Audience audience = null;
            if (AudiencesList.TryGetValue(clientId, out audience))
            {
                return audience;
            }
            return null;
        }

    }
}