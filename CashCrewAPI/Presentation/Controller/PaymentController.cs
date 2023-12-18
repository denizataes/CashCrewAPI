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
        public async Task<IActionResult> CreatePaymentAsync([FromBody] PaymentDto paymentDto)
        {
            var paymentResult = await _manager.PaymentService.CreatePaymentAsync(paymentDto);
            return StatusCode(201, paymentResult);
        }

    }
}

