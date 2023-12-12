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

        public void CreateVacationUserAssociationAsync(VacationUserAssociation vacation) => Create(vacation);

    }
}

