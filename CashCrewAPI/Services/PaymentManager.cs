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

        public async Task<ResultModel<bool>> CreatePaymentAsync(PaymentDto paymentDto)
        {
            var entity = _mapper.Map<Payment>(paymentDto);

            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                _manager.Payment.CreatePaymentAsync(entity);
                await _manager.SaveAsync(); 

                foreach (var participantDto in paymentDto.Participants)
                {
                    var paymentParticipant = new PaymentParticipant
                    {
                        PaymentID = entity.ID,
                        ParticipantUserID = participantDto.ParticipantUserID
                    };

                    await _manager.PaymentParticipant.CreatePaymentParticipantAsync(paymentParticipant);
                }
            }
            catch (Exception e)
            {
                return new ResultModel<bool>(false, "Hata oluştu. Hata Mesajı: " + e.InnerException);
            }
            
            return new ResultModel<bool>(true, "Başarıyla oluşturuldu.");

            }
    }
}

