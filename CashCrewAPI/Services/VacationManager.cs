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

            entity.Password = Encryption.EncryptPassword(entity.Password, entity.Title);
            try
            {
                _manager.Vacation.CreateVacation(entity);
                await _manager.SaveAsync();

                var vacationId = entity.ID;

                var associationModel = new VacationUserAssociation();
                associationModel.UserID = entity.CreatedUserID;
                associationModel.VacationID = vacationId;

                _manager.VacationUserAssociation.CreateVacationUserAssociationAsync(associationModel);
                await _manager.SaveAsync();
            }
            catch (Exception e)
            {
                
            }
            return _mapper.Map<VacationDto>(entity);
        }

        public async Task<List<VacationDto>> GetVacationsByUserIdAsync(int id)
        {
            var entity = await _manager.Vacation.GetVacationsByUserIdAsync(id);
            return _mapper.Map<List<VacationDto>>(entity);
        }

        public async Task<ResultModel<bool>> JoinVacationAsync(JoinVacationDto joinVacationDto)
        {
            try
            {
                var vacation = await _manager.Vacation.GetVacationById(joinVacationDto.VacationID);

                if (vacation != null)
                {
                    vacation.Password = Encryption.Decrypt(vacation.Password, vacation.Title);

                    if (vacation.Password.Equals(joinVacationDto.Password))
                    {
                        var associationExists = await _manager.VacationUserAssociation
                            .CheckIfUserAlreadyRegistered(joinVacationDto.VacationID, joinVacationDto.UserID);

                        if (associationExists)
                        {
                            return new ResultModel<bool>(false, "Bu tatile zaten kayıtlısınız.");
                        }

                        var entity = new VacationUserAssociation
                        {
                            UserID = joinVacationDto.UserID,
                            VacationID = joinVacationDto.VacationID
                        };

                        await _manager.VacationUserAssociation.CreateVacationUserAssociationAsync(entity);
                        await _manager.SaveAsync();

                        return new ResultModel<bool>(true, "Başarılı bir şekilde tatile katıldınız.");
                    }

                    return new ResultModel<bool>(false, "Tatil şifresi yanlış girildi.");
                }

                return new ResultModel<bool>(false, "Tatil bulunamadı.");
            }
            catch (Exception e)
            {
                return new ResultModel<bool>(false, $"Hata oluştu: {e.InnerException?.Message}");
            }
        }

    }
}

