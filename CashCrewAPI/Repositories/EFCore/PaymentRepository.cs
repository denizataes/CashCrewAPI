using System;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;


namespace Repositories.EFCore
{
    public sealed class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
    {
        public PaymentRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreatePaymentAsync(Payment payment) => Create(payment);

        public async Task<List<Payment>> GetAllPaymentsByVacationIDAsync(int ID) =>
            await FindByCondition(b => b.VacationID.Equals(ID), false)
            .ToListAsync();
    }
}

