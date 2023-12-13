using System;
using System.Linq.Expressions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;

namespace Repositories.EFCore
{
    
    public sealed class VacationUserAssociationRepository : RepositoryBase<VacationUserAssociation>, IVacationUserAssociationRepository
    {
        public VacationUserAssociationRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<bool> CheckIfUserAlreadyRegistered(int vacationID, int userID)
        {
            var result = await FindByCondition(
                            b => b.VacationID.Equals(vacationID) && b.UserID.Equals(userID), false
                         )
                         .ToListAsync();

            return result.Any(); 
        }


        public async Task CreateVacationUserAssociationAsync(VacationUserAssociation vacation) => Create(vacation);

    }
}

