using System;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        private readonly IVacationService _vacationService;
        private readonly IPaymentService _paymentService;
        private readonly IDebtService _debtService;

        public ServiceManager(IUserService userService,
            ILoginService loginService,
            IVacationService vacationService,
            IPaymentService paymentService,
            IDebtService debtService
            )
        {
            _userService = userService;
            _loginService = loginService;
            _vacationService = vacationService;
            _paymentService = paymentService;
            _debtService = debtService;
        }

        public IUserService UserService => _userService;

        public ILoginService LoginService => _loginService;

        public IVacationService VacationService => _vacationService;

        public IPaymentService PaymentService => _paymentService;

        public IDebtService DebtService => _debtService;
    }
}

