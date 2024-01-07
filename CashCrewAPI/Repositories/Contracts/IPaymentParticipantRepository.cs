using System;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface IPaymentParticipantRepository : IRepositoryBase<PaymentParticipant>
    {
        Task CreatePaymentParticipantAsync(PaymentParticipant paymentParticipant);
        Task<List<PaymentParticipant>> GetPaymentParticipantsByPaymentID(int paymentID);
    }
}

