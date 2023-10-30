using System;
using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        //Task<PagedList<User>> GetAllBooksAsync(BookParameters bookParameters,
        //bool trackChanges);
        Task<PagedList<User>> GetAllUserAsync(UserParameters userParameters, bool trackChanges);
        Task<User> GetUserByIdAsync(int id, bool trackChanges);
        Task<User> GetUserByUsernameAsync(string username, bool trackChanges);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);

        //Task<IEnumerable<User>> GetAllBooksWithDetailsAsync(bool trackChanges);

    }
}

