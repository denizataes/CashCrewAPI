using System;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IVacationService
    {
        Task<VacationDto> CreateVacationAsync(VacationDto vacationDto);
        Task<List<VacationDto>> GetVacationsByUserIdAsync(int id);
        Task<ResultModel<bool>> JoinVacationAsync(JoinVacationDto joinVacationDto);
        Task<(IEnumerable<VacationDto> vacations, MetaData metaData)> GetAllVacationAsync(SearchParameters vacationParameters, bool trackChanges);

    }
}

