using System;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using Services.Util;

namespace Services
{
    public class VacationManager : IVacationService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public VacationManager(IRepositoryManager manager,
            ILoggerService logger,
            IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<VacationDto> CreateVacationAsync(VacationDto vacationDto)
        {
            var entity = _mapper.Map<Vacation>(vacationDto);

            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            if (_manager.Vacation.GetVacationByTitleAndDescAsync(entity.Title,entity.Description,false) is not null)
                throw new VacationAlreadyExistException(entity.Title, entity.Description);

            entity.Password = Encryption.EncryptPassword(entity.Description, entity.Title);
            try
            {
                _manager.Vacation.CreateVacation(entity);
                await _manager.SaveAsync();
            }
            catch(Exception e)
            {
                
            }
            return _mapper.Map<VacationDto>(entity);
        }

        public async Task<List<VacationDto>> GetVacationsByUserIdAsync(int id)
        {
            var entity = await _manager.Vacation.GetVacationsByUserIdAsync(id);
            return _mapper.Map<List<VacationDto>>(entity);
        }
    }
}

