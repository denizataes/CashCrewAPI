using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace CashCrewAPI.Runtime.Auth.Config
{
    public class ClientConfig
    {
        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {

                // resource owner password grant client
                new Client
                {
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "offline_access","auth.api" },
                    AllowOfflineAccess = true,
                    ClientId = "ResourceOwnerPasswordClient",
                    ClientSecrets = { new Secret("ResourceOwnerPasswordSecret".Sha256()) },
                    AccessTokenLifetime = Convert.ToInt32(new TimeSpan(30, 0, 0, 0).TotalSeconds.ToString())
                },
            };
        }
    }
}

