using System;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUserAsync(bool trackChanges);
        Task<UserDto> GetUserByIdAsync(int id, bool trackChanges);
        Task<UserDto> CreateUserAsync(UserDtoForInsertion user);
        Task UpdateUserAsync(int id, UserDtoForUpdate user, bool trackChanges);
        Task DeleteUserAsync(int id);
    }
}

