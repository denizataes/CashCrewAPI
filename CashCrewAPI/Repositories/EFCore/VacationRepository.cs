using System;
using Entities.Exceptions;
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

        public async Task<Vacation> GetVacationById(int id)
        => await FindByCondition(b => b.ID.Equals(id), false)
                        .FirstOrDefaultAsync();

        public Vacation GetVacationByTitleAndDescAsync(string title, string description, bool trackChanges) =>
              FindByCondition(b => b.Description.Equals(description) && b.Title.Equals(title), trackChanges)
             .SingleOrDefault();

        public async Task<List<Vacation>> GetVacationsByUserIdAsync(int userId) =>
       await FindByCondition(b => b.CreatedUserID.Equals(userId), false)
           .ToListAsync();


    }
}

