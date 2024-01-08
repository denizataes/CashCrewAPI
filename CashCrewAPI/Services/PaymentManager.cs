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
    public class PaymentManager : IPaymentService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public PaymentManager(IRepositoryManager manager,
            ILoggerService logger,
            IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResultModel<bool>> CreatePaymentAsync(PaymentWriteDto PaymentWriteDto)
        {
            var entity = _mapper.Map<Payment>(PaymentWriteDto);

            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            try
            {

                await _manager.Payment.CreatePaymentAsync(entity);
                var participantToRemove = entity.Participants.FirstOrDefault(p => p.ParticipantUserID.Equals(entity.PaidUserID));
                if (participantToRemove != null)
                {
                    entity.Participants.Remove(participantToRemove);
                }

                await _manager.SaveAsync();

            }
            catch (Exception e)
            {
                return new ResultModel<bool>(false, "Hata oluştu. Hata Mesajı: " + e.InnerException);
            }
            
            return new ResultModel<bool>(true, "Başarıyla oluşturuldu.");

            }

        public async Task<List<PaymentReadDto>> GetAllPaymentsByVacationIDAsync(int ID)
        {
            var payments = await _manager.Payment.GetAllPaymentsByVacationIDAsync(ID);
            List<PaymentReadDto> returnList = new List<PaymentReadDto>();
            foreach(var payment in payments)
            {
                var paymentParticipants = await _manager.PaymentParticipant.GetPaymentParticipantsByPaymentID(payment.ID);
                PaymentReadDto obj = new PaymentReadDto();
                obj.ID = payment.ID;
                obj.PaidDateTime = payment.PaidDateTime;
                obj.PaidUser = _mapper.Map<UserInfoDto>(await _manager.User.GetUserByIdAsync(payment.PaidUserID, false));
                obj.Price = payment.Price;
                obj.ProductDescription = payment.ProductDescription;
                obj.ProductName = payment.ProductName;
                obj.VacationID = payment.VacationID;
                obj.IsDebt = payment.IsDebt;
                if (paymentParticipants != null)
                {
                    foreach (var paymentParticipant in paymentParticipants)
                    {
                        var user = _mapper.Map<UserInfoDto>(await _manager.User.GetUserByIdAsync(paymentParticipant.ParticipantUserID, false));
                        if (user is not null)
                        {
                            PaymentParticipantReadDto pprd = new PaymentParticipantReadDto();
                            pprd.ParticipantUser = user;
                            if (obj.Participants == null)
                                obj.Participants = new List<PaymentParticipantReadDto>();

                            obj.Participants.Add(pprd);
                        }
                    }
                }
                returnList.Add(obj);
            }
            return returnList;
        }

        public async Task<decimal> GetTotalPriceByVacationIDAsync(int ID)
        {
            var payments = await _manager.Payment.GetAllPaymentsByVacationIDAsync(ID);
            return payments.Where(t => t.IsDebt == false).Sum(payment => payment.Price);
        }
    }
}

