using System;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUser(bool trackChanges);
        UserDto GetUserById(int id, bool trackChanges);
        UserDto CreateUser(UserDtoForInsertion user);
        void UpdateUser(int id, UserDtoForUpdate user, bool trackChanges);
        void DeleteUser(int id);
    }
}

