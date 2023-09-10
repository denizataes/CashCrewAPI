using System;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUser(bool trackChanges);
        User GetUserById(int id, bool trackChanges);
        void CreateUser(User user);
        void UpdateUser(int id, UserDtoForUpdate user, bool trackChanges);
        void DeleteUser(User user);
    }
}

