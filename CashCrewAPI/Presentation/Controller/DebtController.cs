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
    }
}

