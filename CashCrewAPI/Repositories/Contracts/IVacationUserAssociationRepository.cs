using System;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface IVacationUserAssociationRepository : IRepositoryBase<VacationUserAssociation>
    {
        void CreateVacationUserAssociationAsync(VacationUserAssociation vacation);
    }
}

