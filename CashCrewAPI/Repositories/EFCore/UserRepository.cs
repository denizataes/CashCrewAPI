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

        public IEnumerable<User> GetAllUser(bool trackChanges) => FindAll(false);

        public User GetUserByIdAsync(int id, bool trackChanges) => FindByCondition(b => b.ID.Equals(id), trackChanges)
            .SingleOrDefault();

        public void UpdateUser(User user) => Update(user);
    }
}

