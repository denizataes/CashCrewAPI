using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.DataTransferObjects;
using Presentation.ActionFilters;
using Entities.RequestFeatures;
using System.Text.Json;
using Entities.Models;

namespace Presentation.Controller
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
	[Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public PaymentController(IServiceManager manager)
        {
            _manager = manager;
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("CreatePayment")]
        [Authorize]
        public async Task<IActionResult> CreatePaymentAsync([FromBody] PaymentWriteDto PaymentWriteDto)
        {
            var paymentResult = await _manager.PaymentService.CreatePaymentAsync(PaymentWriteDto);
            if (paymentResult.Success)
            {
                var payments = await _manager.PaymentService.GetAllPaymentsByVacationIDAsync(PaymentWriteDto.VacationID);
                await _manager.DebtService.ControlAndSaveDebtAsync(PaymentWriteDto.VacationID, payments);
            }
            return StatusCode(201, paymentResult);
        }

        [HttpGet("GetAllPaymentsByVacationID")]
        [Authorize]
        public async Task<List<PaymentReadDto>> GetAllPaymentsByVacationIDAsync([FromQuery] int ID)
        {
            var payments = await _manager.PaymentService.GetAllPaymentsByVacationIDAsync(ID);
            return payments;
        }

        [HttpGet("GetTotalPriceByVacationID")]
        [Authorize]
        public async Task<decimal> GetTotalPriceByVacationIDAsync([FromQuery] int ID)
        {
            var totalDept = await _manager.PaymentService.GetTotalPriceByVacationIDAsync(ID);
            return totalDept;
        }

    }
}

