using System;
using Entities.Models;

namespace Repositories.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        //Task<PagedList<User>> GetAllBooksAsync(BookParameters bookParameters,
        //bool trackChanges);
        Task<IEnumerable<User>> GetAllUserAsync(bool trackChanges);
        Task<User> GetUserByIdAsync(int id, bool trackChanges);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);

        //Task<IEnumerable<User>> GetAllBooksWithDetailsAsync(bool trackChanges);

    }
}

