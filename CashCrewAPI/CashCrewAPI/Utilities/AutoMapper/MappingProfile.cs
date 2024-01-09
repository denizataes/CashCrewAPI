using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDtoForUpdate, User>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserInfoDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserDtoForInsertion, User>();



            CreateMap<Vacation, VacationDto>();
            CreateMap<VacationDto, Vacation>();


            CreateMap<PaymentWriteDto, Payment>();
            CreateMap<Payment, PaymentWriteDto>();

            CreateMap<PaymentReadDto, Payment>();
            CreateMap<Payment, PaymentReadDto>();


            CreateMap<PaymentParticipant, PaymentParticipantWriteDto>();
            CreateMap<PaymentParticipantWriteDto, PaymentParticipant>();

            CreateMap<Debt, DebtDto>();
            CreateMap<DebtDto, Debt>();

            CreateMap<VacationUserAssociation, VacationUserAssociationDto>();
            CreateMap<VacationUserAssociationDto, VacationUserAssociation>();


        }
    }
}
