using CashCrewAPI.Runtime.Auth.Config;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Linq;
using System.Threading.Tasks;

namespace CashCrewAPI.Runtime.Auth.Store
{
    public class AuthClientStore : IClientStore
    {
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var clients = ClientConfig.GetClients();
            return Task.FromResult(clients.FirstOrDefault(w => w.ClientId == clientId));
        }
    }
}
