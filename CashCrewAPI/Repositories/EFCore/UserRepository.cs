using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateUser(User user) => Create(user);

        public void DeleteUser(User user) => Delete(user);

        public async Task<IEnumerable<User>> GetAllUserAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .ToListAsync();

        public async Task<User> GetUserByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.ID.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void UpdateUser(User user) => Update(user);
    }
}

