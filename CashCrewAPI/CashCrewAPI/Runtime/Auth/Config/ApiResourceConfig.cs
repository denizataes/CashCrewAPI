using System;
using IdentityServer4.Models;

namespace CashCrewAPI.Runtime.Auth.Config
{
    public class ApiResourceConfig
    {
        public static IEnumerable<ApiResource> GetApiResources(string secretForIntrospectionEndpoint)
        {
            var result = new List<ApiResource>();

            var resource = new ApiResource("auth.api", "Auth API", new List<string>
                {
                    IdentityModel.JwtClaimTypes.Id,
                    IdentityModel.JwtClaimTypes.GivenName,
                    IdentityModel.JwtClaimTypes.Email,
                    IdentityModel.JwtClaimTypes.Locale
                });

            resource.ApiSecrets = new List<Secret>
            {
                new Secret(secretForIntrospectionEndpoint.Sha256())
            };

            result.Add(resource);

            return result;
        }
    }
}
