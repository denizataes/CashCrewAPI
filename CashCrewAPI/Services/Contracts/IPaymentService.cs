﻿using System;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IPaymentService
    {
        Task<ResultModel<bool>> CreatePaymentAsync(PaymentDto paymentDto);
    }
}

