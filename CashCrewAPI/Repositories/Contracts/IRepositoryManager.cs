using System;
namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        ILoginRepository Login { get; }
        IVacationRepository Vacation { get; }
        Task SaveAsync();
    }
}

