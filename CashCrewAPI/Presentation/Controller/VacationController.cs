using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.DataTransferObjects;
using Presentation.ActionFilters;
using Entities.RequestFeatures;
using System.Text.Json;

namespace Presentation.Controller
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
	[Route("api/vacation")]
    public class VacationController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public VacationController(IServiceManager manager)
        {
            _manager = manager;
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("CreateVacation")]
        [Authorize]
        public async Task<IActionResult> CreateVacationAsync([FromBody] VacationDto vacationDto)
        {
            var vacation = await _manager.VacationService.CreateVacationAsync(vacationDto);
            return StatusCode(201, vacation); // CreatedAtRoute()
        }

        [HttpGet("GetVacationsByUserId")]
        [Authorize]
        public async Task<List<VacationDto>> GetVacationsByUserIdAsync([FromQuery(Name = "userId")] int userId)
        {
            return await _manager.VacationService.GetVacationsByUserIdAsync(userId);
        }




    }
}

