﻿using System;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;

namespace Repositories.EFCore
{
    public sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateUser(User user) => Create(user);

        public void DeleteUser(User user) => Delete(user);

        public async Task<PagedList<User>> GetAllUserAsync(UserParameters userParameters,
            bool trackChanges)
        {
            var user = await FindAll(trackChanges)
            .Search(userParameters.SearchTerm)
            .Sort(userParameters.OrderBy)
            .ToListAsync();

            return PagedList<User>
                .ToPagedList(user,
                userParameters.PageNumber,
                userParameters.PageSize);
        }

        public async Task<User> GetUserByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.ID.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<User> GetUserByUsernameAsync(string username, bool trackChanges) =>
    await FindByCondition(b => b.Username.Equals(username), trackChanges)
    .SingleOrDefaultAsync();

        public void UpdateUser(User user) => Update(user);
    }
}

