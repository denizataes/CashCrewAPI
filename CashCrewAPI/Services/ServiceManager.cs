using System;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        private readonly IVacationService _vacationService;

        public ServiceManager(IUserService userService,
            ILoginService loginService,
            IVacationService vacationService)
        {
            _userService = userService;
            _loginService = loginService;
            _vacationService = vacationService;
        }

        public IUserService UserService => _userService;

        public ILoginService LoginService => _loginService;

        public IVacationService VacationService => _vacationService;
    }
}

