using System;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;


namespace Repositories.EFCore
{
    public sealed class PaymentParticipantRepository : RepositoryBase<PaymentParticipant>, IPaymentParticipantRepository
    {
        public PaymentParticipantRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreatePaymentParticipantAsync(PaymentParticipant paymentParticipant) => Create(paymentParticipant);

        public async Task<List<PaymentParticipant>> GetPaymentParticipantsByPaymentID(int paymentID) =>
             await FindByCondition(b => b.PaymentID.Equals(paymentID), false)
            .ToListAsync();
    }
}

