using System;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface IVacationUserAssociationRepository : IRepositoryBase<VacationUserAssociation>
    {
        Task CreateVacationUserAssociationAsync(VacationUserAssociation vacation);
        Task<bool> CheckIfUserAlreadyRegistered(int vacationID, int userID);
    }
}

