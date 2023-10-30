using System;
namespace Services.Contracts
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        ILoginService LoginService { get; }
    }
}

