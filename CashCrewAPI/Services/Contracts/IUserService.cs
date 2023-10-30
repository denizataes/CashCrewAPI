using System;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<(IEnumerable<UserDto> users, MetaData metaData)> GetAllUserAsync(UserParameters userParameters, bool trackChanges);
        Task<UserDto> GetUserByIdAsync(int id, bool trackChanges);
        Task<UserDto> GetUserByUsernameAsync(string username, bool trackChanges);
        Task<UserDto> CreateUserAsync(UserDtoForInsertion user);
        Task UpdateUserAsync(int id, UserDtoForUpdate user, bool trackChanges);
        Task DeleteUserAsync(int id);
        Task<bool> ValidateCredentialsAsync(string username, string password);
    }
}

