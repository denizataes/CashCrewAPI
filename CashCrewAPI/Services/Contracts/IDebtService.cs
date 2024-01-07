using System;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IDebtService
    {
        Task<List<DebtReadDto>> GetDeptsByVacationIDAsync(int ID);
        Task CreateDeptAsync(Debt debt);
        Task UpdateDeptAsync(Debt debt);
        Task DeleteDeptAsync(Debt debt);
        Task ControlAndSaveDebtAsync(int vacationID, List<PaymentReadDto> payments);
    }
}
