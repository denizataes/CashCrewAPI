using System;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;

        public ServiceManager(IUserService userService,
            ILoginService loginService)
        {
            _userService = userService;
            _loginService = loginService;
            
        }

        public IUserService UserService => _userService;

        public ILoginService LoginService => _loginService;
    }
}

