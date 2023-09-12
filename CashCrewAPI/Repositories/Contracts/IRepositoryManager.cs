using System;
namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        Task SaveAsync();
    }
}

