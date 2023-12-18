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
            CreateMap<UserDtoForInsertion, User>();


            CreateMap<Vacation, VacationDto>();
            CreateMap<VacationDto, Vacation>();


            CreateMap<PaymentDto, Payment>();
            CreateMap<Payment, PaymentDto>();


            CreateMap<PaymentParticipant, PaymentParticipantDto>();
            CreateMap<PaymentParticipantDto, PaymentParticipant>();


        }
    }
}
