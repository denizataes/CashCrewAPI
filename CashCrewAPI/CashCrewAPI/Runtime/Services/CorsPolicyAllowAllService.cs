using System;
using IdentityServer4.Services;

namespace CashCrewAPI.Runtime.Services
{
    public class CorsPolicyAllowAllService : ICorsPolicyService
    {
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            return Task.FromResult(true);
        }
    }
}

