using System;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface IPaymentRepository : IRepositoryBase<Payment>
    {
        Task CreatePaymentAsync(Payment payment);
    }
}

