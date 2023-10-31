using System;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;

namespace Repositories.EFCore
{
    public sealed class VacationRepository : RepositoryBase<Vacation>, IVacationRepository
    {
        public VacationRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateVacation(Vacation vacation) => Create(vacation);

        public async Task<Vacation> GetVacationByTitleAndDescAsync(string title, string description, bool trackChanges) =>
         await FindByCondition(b => b.Description.Equals(description) && b.Title.Equals(title), trackChanges)
             .SingleOrDefaultAsync();

        public async Task<List<Vacation>> GetVacationsByUserIdAsync(int userId) =>
       await FindByCondition(b => b.CreatedUserID.Equals(userId), false)
           .ToListAsync();


    }
}

