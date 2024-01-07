using System;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface IDebtRepository: IRepositoryBase<Debt>
    {
        Task CreateDeptAsync(Debt debt);
        Task UpdateDeptAsync(Debt debt);
        Task DeleteDeptAsync(Debt debt);
        Task<List<Debt>> GetDeptsByVacationIDAsync(int ID);
    }
}

