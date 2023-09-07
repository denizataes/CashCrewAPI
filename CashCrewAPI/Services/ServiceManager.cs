using System;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUserService _userService;

        public ServiceManager(IUserService userService)
        {
            _userService = userService;
        }

        public IUserService UserService => _userService;

    }
}

