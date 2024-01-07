using System;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;


namespace Repositories.EFCore
{
    public sealed class DebtRepository : RepositoryBase<Debt>, IDebtRepository
    {
        public DebtRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateDeptAsync(Debt debt) => Create(debt);

        public async Task<List<Debt>> GetDeptsByVacationIDAsync(int ID) =>
            await FindByCondition(b => b.VacationID.Equals(ID), false)
           .ToListAsync();

        public async Task UpdateDeptAsync(Debt debt) => Update(debt);

        public async Task DeleteDeptAsync(Debt debt) => Delete(debt);

    }
}

