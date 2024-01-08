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
	[Route("api/debt")]
    public class DebtController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public DebtController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetDeptsByVacationID")]
        [Authorize]
        public async Task<List<DebtReadDto>> GetDeptsByVacationIDAsync([FromQuery] int ID)
        {
            var debts = await _manager.DebtService.GetDeptsByVacationIDAsync(ID);
            return debts;
        }

        [HttpPost("PayDebt")]
        [Authorize]
        public async Task<ResultModel<bool>> PayDebtAsync([FromBody] PayDebtDto payDebtDto)
        {
            try
            {
                await _manager.DebtService.PayDebtAsync(payDebtDto);
                var payments = await _manager.PaymentService.GetAllPaymentsByVacationIDAsync(payDebtDto.VacationID);
                await _manager.DebtService.ControlAndSaveDebtAsync(payDebtDto.VacationID, payments);
                return new ResultModel<bool>(true, "Borç ödemesi başarılı.");
            }
            catch (Exception ex)
            {
                // Hata durumu için 500 Internal Server Error döndür
                return new ResultModel<bool>(false, "Borç ödemesi sırasında hata oluştu. Lütfen daha sonra tekrar deneyin.");
            }
        }

    }
}

