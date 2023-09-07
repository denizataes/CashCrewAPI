using System;
using Entities.Models;

namespace Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUser(bool trackChanges);
        User GetUserById(int id, bool trackChanges);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}

